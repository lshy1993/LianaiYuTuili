using Assets.Script.GameStruct;
using Assets.Script.GameStruct.EventSystem;
using Assets.Script.GameStruct.Model;
//using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Assets.Script.GameStruct
{
    public class DataManager
    {
        private static DataManager instance;

        public static readonly DateTime START_DAY = new DateTime(2014, 8, 31);

        public static DataManager GetInstance()
        {
            if (instance == null) instance = new DataManager();
            return instance;
        }

        private DataPool datapool;
        //各类数据管理器
        private EventManager eventManager;
        private DetectManager detectManager;
        private EnquireManager enquireManager;
        private ReasoningManager reasoningManager;
        private EduManager eduManager;
        private AppManager appManager;

        //是否自动模式
        public bool isAuto;
        //正在进行图形特效
        public bool isEffecting = false;
        //禁用右键菜单：在询问时，地点转换？
        public bool blockRightClick = false;
        //禁用快进
        public bool blockClick = false;
        //禁用BackLog：在打开菜单，询问时
        public bool blockBacklog = false;      
        //禁用存读档：询问时
        public bool blockSaveLoad = false;

        private DataManager()
        {
            datapool = DataPool.GetInstance();
            Init();
        }

        public void Init()
        {
            InitStatic();
            InitSystem();
            InitSaving();
            InitMultiplay();
            InitGame();
            InitInTurn();
        }

        private void InitInTurn()
        {
            //单回合内数据 写入存档
            SetTempVar("文字记录", new Queue<BacklogText>());
            SetInTurnVar("持有证据", new List<string>());
            SetInTurnVar("侦探事件已知信息", new List<string>());
            SetInTurnVar("侦探模式", Constants.DETECT_STATUS.FREE);
            SetInTurnVar("当前侦探事件", "");
            SetInTurnVar("当前侦探位置", "");
            SetInTurnVar("侦探事件位置状态表", new Dictionary<string, bool>());
            SetInTurnVar("已威慑证词序号", new List<int>());
            SetInTurnVar("询问编号", "");
            SetInTurnVar("证词序号", 0);
            SetInTurnVar("当前血量", 5);
        }

        private void InitGame()
        {
            //游戏数据 跟随存档
            SetGameVar("回合", 0);
            SetGameVar("姓","李");
            SetGameVar("名", "云萧");
            SetGameVar("玩家", new Player());
            SetGameVar("精力总量", 200);
            RandomCourse();
        }

        #region 系统设置类初始化
        private void InitSystem()
        {
            Hashtable sysConfig = new Hashtable();
            string filename = "config.sav";
            string savepath = LoadSaveTool.GetSavePath(filename);
            if (!LoadSaveTool.IsFileExists(savepath))
            {
                //若不存在则创建默认数据
                ResetSysConfig();
                sysConfig = datapool.GetSystemTable();
                //string toSave = LoadSaveTool.RijndaelEncrypt(JsonConvert.SerializeObject(sysConfig), LoadSaveTool.GetKey());
                string toSave = JsonConvert.SerializeObject(sysConfig);
                LoadSaveTool.CreateDirectory(LoadSaveTool.SAVE_PATH);
                LoadSaveTool.CreateFile(savepath, toSave);
            }
            else
            {
                StreamReader savefile = new StreamReader(savepath);
                //string toLoad = LoadSaveTool.RijndaelDecrypt(savefile.ReadToEnd(), LoadSaveTool.GetKey());
                string toLoad = savefile.ReadToEnd();
                //string x = (string)JsonMapper.ToObject(toLoad);
                sysConfig = JsonConvert.DeserializeObject<Hashtable>(toLoad);
                LoadSysConfig(sysConfig);
            }
        }

        public void ResetSysConfig()
        {
            //系统默认设置
            datapool.WriteSystemVar("settingMode", Constants.Setting_Mode.Graphic);
            datapool.WriteSystemVar("fullScreen", false);
            datapool.WriteSystemVar("fadingSwitch", true);
            datapool.WriteSystemVar("animateSwitch", true);
            datapool.WriteSystemVar("avatarSwitch", true);
            datapool.WriteSystemVar("BGMTime", 3);
            datapool.WriteSystemVar("chapterTime", 3);
            datapool.WriteSystemVar("textSpeed", 60f);
            datapool.WriteSystemVar("waitTime", 1.5f);
            datapool.WriteSystemVar("diaboxAlpha", 75);
            datapool.WriteSystemVar("defaultCharaNum", 0);
            float[] charaVolume = { 1, 1, 1, 1, 1, 1 };
            datapool.WriteSystemVar("charaVoiceVolume", charaVolume);
            bool[] charaVoice = { true, true, true, true, true, true };
            datapool.WriteSystemVar("charaVoice", charaVoice);
        }

        private void LoadSysConfig(Hashtable hst)
        {
            foreach(string key in hst.Keys)
            {
                //Debug.Log(key+":" + hst[key].GetType() +":::"+ hst[key].ToString());
                if(key == "textSpeed")
                {
                    string json = hst[key].ToString();
                    datapool.WriteSystemVar(key, JsonConvert.DeserializeObject<float>(json));
                }
                else if (key == "waitTime")
                {
                    string json = hst[key].ToString();
                    datapool.WriteSystemVar(key, JsonConvert.DeserializeObject<float>(json));
                }
                else if (key == "charaVoiceVolume")
                {
                    string json = hst[key].ToString();
                    datapool.WriteSystemVar(key, JsonConvert.DeserializeObject<float[]>(json));
                }
                else if(key == "charaVoice")
                {
                    string json = hst[key].ToString();
                    datapool.WriteSystemVar(key, JsonConvert.DeserializeObject<bool[]>(json));
                }
                else if (hst[key].GetType() == typeof(Int64))
                {
                    datapool.WriteSystemVar(key, Convert.ToInt32(hst[key]));
                }
                else
                    datapool.WriteSystemVar(key, hst[key]);
            }
        }
        #endregion

        #region 存档类初始化
        /// <summary>
        /// 初始化存档
        /// </summary>
        private void InitSaving()
        {
            Dictionary<int, SavingInfo> list = new Dictionary<int, SavingInfo>();
            string filename = "datasv.sav";
            string savepath = LoadSaveTool.GetSavePath(filename);
            if (LoadSaveTool.IsFileExists(savepath))
            {
                //读取存档列表
                StreamReader savefile = new StreamReader(savepath);
                //string toLoad = LoadSaveTool.RijndaelDecrypt(savefile.ReadToEnd(), LoadSaveTool.GetKey());
                string toLoad = savefile.ReadToEnd();
                //string x = (string)JsonMapper.ToObject(toLoad);
                list = JsonConvert.DeserializeObject<Dictionary<int, SavingInfo>>(toLoad);
            }
            datapool.WriteSystemVar("存档信息", list);
            RefreshSavePic();
        }

        /// <summary>
        /// 读取存档的图片并写入Data
        /// </summary>
        public void RefreshSavePic()
        {
            Dictionary<int, SavingInfo> list = (Dictionary<int, SavingInfo>)datapool.GetSystemVar("存档信息");
            Dictionary<string, byte[]> savepic = new Dictionary<string, byte[]>();
            foreach (KeyValuePair<int, SavingInfo> kv in list)
            {
                string picname = "data" + kv.Key + ".png";
                string picpath = LoadSaveTool.GetSavePath(picname);
                FileStream fs = new FileStream(picpath, FileMode.Open, FileAccess.Read);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, (int)fs.Length);
                fs.Close();
                savepic.Add(kv.Value.picPath, bytes);
            }
            datapool.WriteSystemVar("存档缩略图", savepic);
        }
        #endregion

        #region 多周目数据初始化
        /// <summary>
        /// 初始化多周目数据
        /// </summary>
        private void InitMultiplay()
        {
            List<bool> musicTable = new List<bool>();
            List<bool> cgTable = new List<bool>();
            List<bool> endingTable = new List<bool>();
            List<bool> caseTable = new List<bool>();

            string filename = "datamp.sav";
            string savepath = LoadSaveTool.GetSavePath(filename);

            //若不存在 则生成默认数据表 并写入本地文件
            if (!LoadSaveTool.IsFileExists(savepath))
            {
                #region 临时测试用 静态表
                musicTable.Add(true);
                cgTable.Add(true);
                endingTable.Add(true);
                caseTable.Add(true);
                #endregion

                //Hashtable SysSave = new Hashtable();
                Dictionary<string, List<bool>> SysSave = new Dictionary<string, List<bool>>(); 
                SysSave.Add("MusicTable", musicTable);
                SysSave.Add("CGTable", cgTable);
                SysSave.Add("EndingTable", endingTable);
                SysSave.Add("CaseTable", caseTable);

                //string toSave = LoadSaveTool.RijndaelEncrypt(JsonConvert.SerializeObject(SysSave), LoadSaveTool.GetKey());
                string toSave = JsonConvert.SerializeObject(SysSave);
                LoadSaveTool.CreateDirectory(LoadSaveTool.SAVE_PATH);
                LoadSaveTool.CreateFile(savepath, toSave);
            }
            else
            {
                //存在 则读取二周目数据
                StreamReader savefile = new StreamReader(savepath);
                //string toLoad = LoadSaveTool.RijndaelDecrypt(savefile.ReadToEnd(), LoadSaveTool.GetKey());
                string toLoad = savefile.ReadToEnd();
                Dictionary<string, List<bool>> sysSave = JsonConvert.DeserializeObject<Dictionary<string, List<bool>>>(toLoad);
                musicTable = sysSave["MusicTable"];
                cgTable = sysSave["CGTable"];
                endingTable = sysSave["EndingTable"];
                caseTable = sysSave["CaseTable"];
            }
            datapool.WriteSystemVar("音乐表", musicTable);
            datapool.WriteSystemVar("画廊表", cgTable);
            datapool.WriteSystemVar("结局表", endingTable);
            datapool.WriteSystemVar("案件表", caseTable);


            //TODO:静态表格 例如cginfo
            Dictionary<int, string> cgInfo = new Dictionary<int, string>();
            cgInfo.Add(0, "Logo");
            cgInfo.Add(1, "classroom");
            datapool.WriteSystemVar("CG信息表", cgInfo);

        }
        #endregion

        #region 游戏静态数据初始化
        private void InitStatic()
        {
            InitEvents();
            InitDetects();
            InitCharacters();
            InitEnquire();
            InitReasoning();
            InitEdu();
            InitEvidence();
            InitApp();
        }

        /// <summary>
        /// 初始化电子学生手册
        /// </summary>
        private void InitApp()
        {
            Dictionary<string, Girl> girls = AppManager.GetStaticGirls();
            datapool.WriteStaticVar("女主角资料表", girls);
            Dictionary<string, Tour> tours = AppManager.GetStaticTours();
            datapool.WriteStaticVar("旅游资讯表", tours);
            Dictionary<string, Keyword> keywords = AppManager.GetStaticKeywords();
            datapool.WriteStaticVar("帮助词条表", keywords);
            Dictionary<int, Routine> routines = AppManager.GetStaticRoutines();
            datapool.WriteStaticVar("日程表", routines);
        }

        private void InitCharacters()
        {
            Dictionary<string, Character> characters = CharacterManager.GetStaticCharacters();
            datapool.WriteStaticVar("人物", characters);

            CharacterManager.GetInstance().characterTable = characters;
        }


        /// <summary>
        /// 初始化证据信息
        /// </summary>
        private void InitEvidence()
        {
            Dictionary<string, Evidence> evidenceDic = EvidenceManager.GetStaticEvidenceDic();
            datapool.WriteStaticVar("证据列表", evidenceDic);

            //List<Evidence> holdEvidence = new List<Evidence>();
            //datapool.WriteGameVar("持有证据", holdEvidence);

            // 测试后删除
            //foreach(KeyValuePair<string,Evidence> kv in evidenceDic)
            //{
            //    holdEvidence.Add(kv.Value);
            //}

        }

        /// <summary>
        /// 初始化【养成模式】数据
        /// </summary>
        private void InitEdu()
        {
            List<EduEvent> events = EduManager.GetStaticEduEvents();
            datapool.WriteStaticVar("养成按钮", events);

            eduManager = EduManager.GetInstance();
            eduManager.Init(events, this);
        }

        /// <summary>
        /// 初始化【侦探模式】数据
        /// </summary>
        private void InitDetects()
        {
            Dictionary<string, DetectEvent> events = DetectManager.GetStaticDetectEvents();
            datapool.WriteStaticVar("侦探事件表", events);

            detectManager = DetectManager.GetInstance();
            detectManager.Init(events, this);
        }

        /// <summary>
        /// 初始化【询问模式】数据
        /// </summary>
        private void InitEnquire()
        {
            Dictionary<string, EnquireEvent> events = EnquireManager.GetStaticEnquireEvents();
            datapool.WriteStaticVar("询问总表", events);

            enquireManager = EnquireManager.GetInstance();
            enquireManager.Init(events, this);
        }

        /// <summary>
        /// 初始化【询问模式】数据
        /// </summary>
        private void InitReasoning()
        {
            Dictionary<string, ReasoningEvent> events = ReasoningManager.GetStaticEnquireEvents();
            datapool.WriteStaticVar("自我推理总表", events);

            reasoningManager = ReasoningManager.GetInstance();
            reasoningManager.Init(events);
        }

        /// <summary>
        /// 初始化【事件】数据
        /// </summary>
        private void InitEvents()
        {
            Dictionary<string, MapEvent> events = EventManager.GetStaticEvent();
            datapool.WriteStaticVar("事件表", events);
            datapool.WriteGameVar("事件状态表", EventManager.LoadEventState(events));
            eventManager = EventManager.GetInstance();
            eventManager.Init(
                (Dictionary<string, MapEvent>)datapool.GetStaticVar("事件表"),
                this);
        }
        #endregion

        public void PrintEvents()
        {
            Debug.Log("事件表:");
            foreach (KeyValuePair<string, MapEvent> kv in (Dictionary<string, MapEvent>)datapool.GetStaticVar("事件表"))
            {
                Debug.Log(kv.Key);
                Debug.Log(kv.Value.ToString());
            }
        }

        /// <summary>
        /// 游戏执行下一回合
        /// </summary>
        public void MoveOneTurn()
        {
            int t = GetGameVar<int>("回合");
            SetGameVar("回合", t + 1);
            //清空InTurnVar
            InitInTurn();
            //随机当日的课表
            RandomCourse();
        }
        
        private void RandomCourse()
        {
            int morningSchedule = 0, afternoonSchedule = 0;
            while (morningSchedule == afternoonSchedule)
            {
                morningSchedule = UnityEngine.Random.Range(0, 4);
                afternoonSchedule = UnityEngine.Random.Range(0, 4);
            }
            SetGameVar("上午课程", morningSchedule);
            SetGameVar("下午课程", afternoonSchedule);
            //随机当日的加成系数
            int morningRate = IsHoliday() ? 1 : UnityEngine.Random.Range(2, 3);
            int afternoonRate = IsHoliday() ? 1 : UnityEngine.Random.Range(2, 3);
            SetGameVar("上午指数", morningRate);
            SetGameVar("下午指数", afternoonRate);
        }

        /// <summary>
        /// 判断当前回合时候是节假日
        /// </summary>
        public bool IsHoliday()
        {
            int turn = GetGameVar<int>("回合");
            DateTime date = START_DAY.AddDays(turn);
            int x = Convert.ToInt32(date.DayOfWeek);
            //TODO:对节假日的判断
            if (turn != 0 && (x == 6 || x == 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取当前回合对应日期
        /// </summary>
        public DateTime GetToday()
        {
            int turn = GetGameVar<int>("回合");
            return START_DAY.AddDays(turn);
        }


        /// <summary>
        /// 清空文字记录
        /// </summary>
        public void ClearHistory()
        {
            GetTempVar<Queue<BacklogText>>("文字记录").Clear();
        }

        /// <summary>
        /// 添加文字记录
        /// </summary>
        /// <param name="blt">文本记录块</param>
        public void AddHistory(BacklogText blt)
        {
            Queue<BacklogText> history = GetTempVar<Queue<BacklogText>>("文字记录");
            if (history.Count > 100)
            {
                history.Dequeue();
            }
            history.Enqueue(blt);
        }

        public Queue<BacklogText> GetHistory()
        {
            return GetTempVar<Queue<BacklogText>>("文字记录");
        }

        #region Get / Set 方法
        public void SetTempVar(string key, object value)
        {
            datapool.WriteTempVar(key, value);
        }

        public void SetGameVar(string key, object value)
        {
            datapool.WriteGameVar(key, value);
        }

        public void SetInTurnVar(string key, object value)
        {
            datapool.WriteInTurnVar(key, value);
        }

        public void SetSystemVar(string key, object value)
        {
            datapool.WriteSystemVar(key, value);
        }

        public T GetTempVar<T>(string key)
        {
            return (T)datapool.GetTempVar(key);
        }

        public T GetGameVar<T>(string key)
        {
            return (T)datapool.GetGameVar(key);
        }

        public T GetInTurnVar<T>(string key)
        {
            return (T)datapool.GetInTurnVar(key);
        }

        public T GetSystemVar<T>(string key)
        {
            return (T)datapool.GetSystemVar(key);
        }

        public bool ContainsGameVar(string key) { return datapool.GetGameVarTable().ContainsKey(key); }

        public bool ContainsInTurnVar(string key) { return datapool.GetInTurnVarTable().ContainsKey(key); }

        DataPool GetDataPool() { return datapool; }
        public Hashtable GetGameVars()
        {
            return datapool.GetGameVarTable();
        }

        public Hashtable GetInTurnVars()
        {
            return datapool.GetInTurnVarTable();
        }
        #endregion

        #region 存读档用
        public void Save(int i)
        {
            //string toSave = LoadSaveTool.RijndaelEncrypt(DataToJsonString(), LoadSaveTool.GetKey());
            string toSave = DataToJsonString();
            string filename = "data" + i + ".sav";
            LoadSaveTool.CreateDirectory(LoadSaveTool.SAVE_PATH);
            LoadSaveTool.CreateFile(LoadSaveTool.GetSavePath(filename), toSave);
            //储存截图
            string picname = "data" + i + ".png";
            byte[] picdata = (byte[])datapool.GetSystemVar("缩略图");
            LoadSaveTool.CreatByteFile(LoadSaveTool.GetSavePath(picname), picdata);
            //更新存档信息
            Dictionary<int, SavingInfo> savedic = (Dictionary<int, SavingInfo>)datapool.GetSystemVar("存档信息");
            //TODO: 获取状态
            string gamemode = GetGameVar<string>("MODE");
            string savetime = DateTime.Now.ToString("yyyy/MM/dd\nHH:mm");
            string customtext = "存档了！";
            SavingInfo info = new SavingInfo(gamemode, savetime, customtext, picname);
            if (savedic.ContainsKey(i))
            {
                savedic[i] = info;
            }
            else
            {
                savedic.Add(i, info);
            }
            //写入系统存档
            //string sysSave = LoadSaveTool.RijndaelEncrypt(JsonMapper.Serialize(savedic), LoadSaveTool.GetKey());
            string sysSave = JsonConvert.SerializeObject(savedic);
            LoadSaveTool.CreateFile(LoadSaveTool.GetSavePath("datasv.sav"), sysSave);
            RefreshSavePic();
        }

        private string DataToJsonString()
        {
            Hashtable toSave = new Hashtable();
            toSave.Add("GameVar", new Hashtable(datapool.GetGameVarTable()));
            toSave.Add("InTurnVar", new Hashtable(datapool.GetInTurnVarTable()));
            return JsonConvert.SerializeObject(toSave);
                //JsonMapper.Serialize(toSave);
        }

        public void Load(int i)
        {
            string filename = "data" + i + ".sav";
            StreamReader savefile = new StreamReader(LoadSaveTool.GetSavePath(filename));
            string toLoad = savefile.ReadToEnd();
            //toLoad = LoadSaveTool.RijndaelDecrypt(toLoad, LoadSaveTool.GetKey());
            LoadDataFromJson(toLoad);
        }

        private void LoadDataFromJson(string str)
        {
            string json;
            Hashtable hst = JsonConvert.DeserializeObject<Hashtable>(str);
            json = hst["GameVar"].ToString();
            JObject gVars = JObject.Parse(json);
            //游戏数据GameVar
            json = gVars.Property("回合").Value.ToString();
            int currentTurn = JsonConvert.DeserializeObject<int>(json);
            json = gVars.Property("玩家").Value.ToString();
            Player player = JsonConvert.DeserializeObject<Player>(json);
            string currentEventName = gVars.Property("当前事件名").Value.ToString();
            json = gVars.Property("事件状态表").Value.ToString();
            Dictionary<string, int> eventStatusDict = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
            string xing = gVars.Property("姓").Value.ToString();
            string ming = gVars.Property("名").Value.ToString();
            json = gVars.Property("精力总量").Value.ToString();
            int allMP = JsonConvert.DeserializeObject<int>(json);
            json = gVars.Property("上午课程").Value.ToString();
            int morningSchedule = JsonConvert.DeserializeObject<int>(json);
            json = gVars.Property("下午课程").Value.ToString();
            int afternoonSchedule = JsonConvert.DeserializeObject<int>(json);
            json = gVars.Property("上午指数").Value.ToString();
            int morningRate = JsonConvert.DeserializeObject<int>(json);
            json = gVars.Property("下午指数").Value.ToString();
            int afternoonRate = JsonConvert.DeserializeObject<int>(json);
            string modeName = gVars.Property("MODE").Value.ToString();
            string textName = gVars.Property("当前脚本名").Value.ToString();
            json = gVars.Property("文字位置").Value.ToString();
            int currentTextId = JsonConvert.DeserializeObject<int>(json);

            if(modeName == "Avg模式" || modeName == "侦探模式")
            {
                string bgsprite = gVars.Property("背景图片").Value.ToString();
                json = gVars.Property("立绘信息").Value.ToString();
                Dictionary<int, SpriteState> fgsprite = JsonConvert.DeserializeObject<Dictionary<int, SpriteState>>(json);
                SetGameVar("背景图片", bgsprite);
                SetGameVar("立绘信息", fgsprite);
            }

            SetGameVar("回合", currentTurn);
            SetGameVar("玩家", player);
            SetGameVar("事件状态表", eventStatusDict);
            SetGameVar("当前事件名", currentEventName);
            eventManager.UpdateEvent();

            SetGameVar("姓", xing);
            SetGameVar("名", ming);
            SetGameVar("精力总量", allMP);
            SetGameVar("上午课程", morningSchedule);
            SetGameVar("下午课程", afternoonSchedule);
            SetGameVar("上午指数", morningRate);
            SetGameVar("下午指数", afternoonRate);
            SetGameVar("当前脚本名", textName);
            SetGameVar("MODE", modeName);
            SetGameVar("文字位置", currentTextId);

            //单回合内数据
            json = hst["InTurnVar"].ToString();
            JObject IVars = JObject.Parse(json);

            //foreach (JProperty jp in IVars.Properties())
            //{
            //    Debug.Log(jp.Name);
            //}

            json = IVars.Property("持有证据").Value.ToString();
            List<string> evidenceHave = JsonConvert.DeserializeObject<List<string>>(json);
            json = IVars.Property("侦探模式").Value.ToString();
            Constants.DETECT_STATUS currentStatus = JsonConvert.DeserializeObject<Constants.DETECT_STATUS>(json);
            json = IVars.Property("侦探事件位置状态表").Value.ToString();
            Dictionary<string, bool> placeDict = JsonConvert.DeserializeObject<Dictionary<string, bool>>(json);
            json = IVars.Property("已威慑证词序号").Value.ToString();
            List<int> pressedId = JsonConvert.DeserializeObject<List<int>>(json);
            json = IVars.Property("侦探事件已知信息").Value.ToString();
            List<string> knownInfo = JsonConvert.DeserializeObject<List<string>>(json);
            string currentDetect = IVars.Property("当前侦探事件").Value.ToString();
            string currentDetectPlace = IVars.Property("当前侦探位置").Value.ToString();
            json = IVars.Property("当前血量").Value.ToString();
            int currentHP = JsonConvert.DeserializeObject<int>(json);
            string enquireId = IVars.Property("询问编号").Value.ToString();
            json = IVars.Property("证词序号").Value.ToString();
            int testimonyId = JsonConvert.DeserializeObject<int>(json);


            SetInTurnVar("持有证据", evidenceHave);
            SetInTurnVar("侦探模式", currentStatus);
            SetInTurnVar("侦探事件位置状态表", placeDict);
            SetInTurnVar("已威慑证词序号", pressedId);
            SetInTurnVar("侦探事件已知信息", knownInfo);
            SetInTurnVar("当前侦探事件",currentDetect);
            SetInTurnVar("当前侦探位置", currentDetectPlace);
            SetInTurnVar("询问编号", enquireId);
            SetInTurnVar("证词序号", testimonyId);
            SetInTurnVar("当前血量", currentHP);

        }

        public void Delete(int i)
        {
            //删除存档
            string filename = "data" + i + ".sav";
            LoadSaveTool.DeleteFile(filename);
            //删除截图
            string picname = "data" + i + ".png";
            LoadSaveTool.DeleteFile(picname);
            //更新存档信息
            Dictionary<int, SavingInfo> savedic = (Dictionary<int, SavingInfo>)datapool.GetSystemVar("存档信息");
            if (savedic.ContainsKey(i))
            {
                savedic.Remove(i);
            }
            //写入系统存档
            string sysSave = JsonConvert.SerializeObject(savedic);
            LoadSaveTool.CreateFile(LoadSaveTool.GetSavePath("datasv.sav"), sysSave);
            RefreshSavePic();
        }
        #endregion

    }
}
