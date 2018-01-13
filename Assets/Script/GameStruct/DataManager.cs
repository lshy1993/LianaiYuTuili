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

        //各类数据管理器
        private EventManager eventManager;
        private DetectManager detectManager;
        private EnquireManager enquireManager;
        private ReasoningManager reasoningManager;
        private EduManager eduManager;
        private AppManager appManager;

        public GameData gameData = new GameData();
        public InTurnData inturnData = new InTurnData();
        public StaticData staticData = new StaticData();
        public ConfigData configData = new ConfigData();
        public MultiData multiData = new MultiData();
        public TempData tempData = new TempData();

        /// <summary>
        /// 是否自动模式
        /// </summary>
        public bool isAuto = false;

        /// <summary>
        /// 是否正在进行特效
        /// </summary>
        public bool isEffecting = false;

        /// <summary>
        /// 禁用右键菜单(询问/地点转换)
        /// </summary>
        private bool blockRightClick = false;

        /// <summary>
        /// 禁用滚轮
        /// </summary>
        private bool blockWheel = false;

        /// <summary>
        /// 禁用快进
        /// </summary>
        private bool blockClick = false;

        /// <summary>
        /// 禁用文字履历(菜单/询问)
        /// </summary>
        private bool blockBacklog = false;

        /// <summary>
        /// 禁用存读档(询问)
        /// </summary>
        private bool blockSaveLoad = false;

        private DataManager()
        {
            Init();
        }

        public void Init()
        {
            InitTemp();
            InitStatic();
            InitSystem();
            InitMultiplay();
            InitSaving();
            InitGame();
            InitInTurn();
        }

        /// <summary>
        /// 游戏临时数据的清空
        /// </summary>
        private void InitTemp()
        {
            //SetTempVar("文字记录", new Queue<BacklogText>());
            tempData.backLog = new Queue<BacklogText>();
        }

        /// <summary>
        /// 初始化推理模式数据
        /// </summary>
        public void InitInTurn()
        {
            //推理回合数据 写入存档
            inturnData.holdEvidences = new List<string>();
            inturnData.detectKnown = new List<string>();
            inturnData.detectMode = Constants.DETECT_STATUS.FREE;
            inturnData.currentDetectEvent = string.Empty;
            inturnData.currentDetectPos = string.Empty;
            inturnData.detectEventTable = new Dictionary<string, bool>();
            inturnData.pressedTestimony = new List<int>();
            inturnData.currentEnquire = string.Empty;
            inturnData.currentTestimonyNum = 0;
            inturnData.currentHP = 5;
        }

        public void InitGame()
        {
            //游戏数据初始化 跟随存档
            gameData.gameTurn = 0;
            gameData.heroXing = "李";
            gameData.heroMing = "云萧";
            gameData.player = new Player();
            gameData.All_MP = 200;
            gameData.MODE = string.Empty;
            gameData.currentEvent = string.Empty;
            gameData.currentScript = string.Empty;
            gameData.currentTextPos = 0;
            gameData.bgSprite = string.Empty;
            gameData.fgSprites = new Dictionary<int, SpriteState>();
            //事件状态表的重置
            gameData.eventStatus = EventManager.LoadEventState(staticData.eventTable);
            RandomCourse();
        }

        #region 游戏设置 ConfigData 初始化
        private void InitSystem()
        {
            string filename = "config.sav";
            string savepath = LoadSaveTool.GetSavePath(filename);
            if (!LoadSaveTool.IsFileExists(savepath))
            {
                //若不存在则创建默认数据
                ResetSysConfig();
                //string toSave = LoadSaveTool.RijndaelEncrypt(JsonConvert.SerializeObject(sysConfig), LoadSaveTool.GetKey());
                string toSave = JsonConvert.SerializeObject(configData);

                LoadSaveTool.CreateDirectory(LoadSaveTool.SAVE_PATH);
                LoadSaveTool.CreateFile(savepath, toSave);
            }
            else
            {
                StreamReader savefile = new StreamReader(savepath);
                //string toLoad = LoadSaveTool.RijndaelDecrypt(savefile.ReadToEnd(), LoadSaveTool.GetKey());
                string toLoad = savefile.ReadToEnd();
                configData = JsonConvert.DeserializeObject<ConfigData>(toLoad);
                //LoadSysConfig(sysConfig);
            }
        }

        public void ResetSysConfig()
        {
            //游戏的默认设置
            configData.settingMode = Constants.Setting_Mode.Graphic;
            configData.fullScreen = false;
            configData.fadingSwitch = true;
            configData.animateSwitch = true;
            configData.avatarSwitch = true;
            configData.BGMTime = 3;
            configData.chapterTime = 3;
            configData.textSpeed = 60f;
            configData.waitTime = 1.5f;
            configData.diaboxAlpha = 75;
            configData.defaultCharaNum = 0;
            configData.charaVoiceVolume = new float[] { 1, 1, 1, 1, 1, 1 };
            configData.charaVoice = new bool[] { true, true, true, true, true, true };
        }

        /* 废弃旧版代码
        private void LoadSysConfig(Hashtable hst)
        {
            foreach(string key in hst.Keys)
            {
                //Debug.Log(key+":" + hst[key].GetType() +":::"+ hst[key].ToString());
                if(key == "textSpeed")
                {
                    string json = hst[key].ToString();
                    var value = JsonConvert.DeserializeObject<float>(json);
                    configData.GetType().GetField(key).SetValue(configData, value);
                    //datapool.WriteSystemVar(key, JsonConvert.DeserializeObject<float>(json));
                }
                else if (key == "waitTime")
                {
                    string json = hst[key].ToString();
                    var value = JsonConvert.DeserializeObject<float>(json);
                    configData.GetType().GetField(key).SetValue(configData, value);
                    //datapool.WriteSystemVar(key, JsonConvert.DeserializeObject<float>(json));
                }
                else if (key == "charaVoiceVolume")
                {
                    string json = hst[key].ToString();
                    var value = JsonConvert.DeserializeObject<float[]>(json);
                    configData.GetType().GetField(key).SetValue(configData, value);
                    //datapool.WriteSystemVar(key, JsonConvert.DeserializeObject<float[]>(json));
                }
                else if(key == "charaVoice")
                {
                    string json = hst[key].ToString();
                    var value = JsonConvert.DeserializeObject<bool[]>(json);
                    configData.GetType().GetField(key).SetValue(configData, value);
                    //datapool.WriteSystemVar(key, JsonConvert.DeserializeObject<bool[]>(json));
                }
                else if (hst[key].GetType() == typeof(Int64))
                {
                    var value = Convert.ToInt32(hst[key]);
                    configData.GetType().GetField(key).SetValue(configData, value);
                    //datapool.WriteSystemVar(key, Convert.ToInt32(hst[key]));
                }
                else
                {
                    var value = hst[key];
                    configData.GetType().GetField(key).SetValue(configData, value);
                    //datapool.WriteSystemVar(key, hst[key]);
                }

            }
        }
        */

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
                list = JsonConvert.DeserializeObject<Dictionary<int, SavingInfo>>(toLoad);
            }
            //datapool.WriteSystemVar("存档信息", list);
            tempData.saveInfo = list;
            RefreshSavePic();
        }

        /// <summary>
        /// 读取存档的图片并写入Data
        /// </summary>
        public void RefreshSavePic()
        {
            //Dictionary<int, SavingInfo> list = (Dictionary<int, SavingInfo>)datapool.GetSystemVar("存档信息");
            Dictionary<int, SavingInfo> list = tempData.saveInfo;

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
            tempData.WriteTempVar("存档缩略图", savepic);
            //datapool.WriteSystemVar("存档缩略图", savepic);
        }
        #endregion

        #region 多周目数据初始化
        /// <summary>
        /// 初始化多周目数据
        /// </summary>
        private void InitMultiplay()
        {
            string filename = "datamp.sav";
            string savepath = LoadSaveTool.GetSavePath(filename);

            //判断是否含有datamp文件
            if (!LoadSaveTool.IsFileExists(savepath))
            {
                //若不存在 则生成默认数据表 
                multiData = new MultiData();
                #region 临时测试用 静态表
                multiData.musicTable.Add(true);
                multiData.cgTable.Add(true);
                multiData.endingTable.Add(true);
                multiData.caseTable.Add(true);
                #endregion

                //并写入本地文件
                //string toSave = LoadSaveTool.RijndaelEncrypt(JsonConvert.SerializeObject(SysSave), LoadSaveTool.GetKey());
                string toSave = JsonConvert.SerializeObject(multiData);
                LoadSaveTool.CreateDirectory(LoadSaveTool.SAVE_PATH);
                LoadSaveTool.CreateFile(savepath, toSave);
            }
            else
            {
                //若文件存在 则读取二周目数据
                StreamReader savefile = new StreamReader(savepath);
                //string toLoad = LoadSaveTool.RijndaelDecrypt(savefile.ReadToEnd(), LoadSaveTool.GetKey());
                string toLoad = savefile.ReadToEnd();
                multiData = JsonConvert.DeserializeObject<MultiData>(toLoad);
            }

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
            //TODO:静态表格 例如cginfo 记录文件名与键值
            Dictionary<int, string> cgInfo = new Dictionary<int, string>();
            cgInfo.Add(0, "Logo");
            cgInfo.Add(1, "classroom");
            staticData.cgInfo = cgInfo;
        }

        /// <summary>
        /// 初始化电子学生手册
        /// </summary>
        private void InitApp()
        {
            Dictionary<string, Girl> girls = AppManager.GetStaticGirls();
            //datapool.WriteStaticVar("女主角资料表", girls);
            staticData.girls = girls;
            Dictionary<string, Tour> tours = AppManager.GetStaticTours();
            //datapool.WriteStaticVar("旅游资讯表", tours);
            staticData.tours = tours;
            Dictionary<string, Keyword> keywords = AppManager.GetStaticKeywords();
            //datapool.WriteStaticVar("帮助词条表", keywords);
            staticData.keywords = keywords;
            Dictionary<int, Routine> routines = AppManager.GetStaticRoutines();
            //datapool.WriteStaticVar("日程表", routines);
            staticData.routines = routines;
        }

        private void InitCharacters()
        {
            Dictionary<string, Character> characters = CharacterManager.GetStaticCharacters();
            //datapool.WriteStaticVar("人物", characters);
            staticData.characters = characters;
            CharacterManager.GetInstance().characterTable = characters;
        }


        /// <summary>
        /// 初始化证据信息
        /// </summary>
        private void InitEvidence()
        {
            Dictionary<string, Evidence> evidenceDic = EvidenceManager.GetStaticEvidenceDic();
            //datapool.WriteStaticVar("证据列表", evidenceDic);
            staticData.evidenceDic = evidenceDic;
        }

        /// <summary>
        /// 初始化【养成模式】数据
        /// </summary>
        private void InitEdu()
        {
            List<EduEvent> eduEvents = EduManager.GetStaticEduEvents();
            //datapool.WriteStaticVar("养成按钮", eduEvents);
            staticData.eduEvents = eduEvents;
            eduManager = EduManager.GetInstance();
            eduManager.Init(eduEvents, this);
        }

        /// <summary>
        /// 初始化【侦探模式】数据
        /// </summary>
        private void InitDetects()
        {
            Dictionary<string, DetectEvent> detectEvents = DetectManager.GetStaticDetectEvents();
            //datapool.WriteStaticVar("侦探事件表", detectEvents);
            staticData.detectEvents = detectEvents;
            detectManager = DetectManager.GetInstance();
            detectManager.Init(detectEvents, this);
        }

        /// <summary>
        /// 初始化【询问模式】数据
        /// </summary>
        private void InitEnquire()
        {
            Dictionary<string, EnquireEvent> enquireEvents = EnquireManager.GetStaticEnquireEvents();
            //datapool.WriteStaticVar("询问总表", enquireEvents);
            staticData.enquireEvents = enquireEvents;
            enquireManager = EnquireManager.GetInstance();
            enquireManager.Init(enquireEvents, this);
        }

        /// <summary>
        /// 初始化【推理模式】数据
        /// </summary>
        private void InitReasoning()
        {
            Dictionary<string, ReasoningEvent> reasonEvents = ReasoningManager.GetStaticEnquireEvents();
            //datapool.WriteStaticVar("自我推理总表", reasonEvents);
            staticData.reasonEvents = reasonEvents;
            reasoningManager = ReasoningManager.GetInstance();
            reasoningManager.Init(reasonEvents);
        }

        /// <summary>
        /// 初始化【事件】数据
        /// </summary>
        private void InitEvents()
        {
            Dictionary<string, MapEvent> events = EventManager.GetStaticEvent();
            //datapool.WriteStaticVar("事件表", events);
            staticData.eventTable = events;

            //datapool.WriteGameVar("事件状态表", EventManager.LoadEventState(events));
            //gameData.eventStatus = EventManager.LoadEventState(events);

            eventManager = EventManager.GetInstance();
            /*
            eventManager.Init(
                (Dictionary<string, MapEvent>)datapool.GetStaticVar("事件表"),
                this);
                */
            eventManager.Init(staticData.eventTable, this);
        }
        #endregion


        private void RandomCourse()
        {
            //随机当日课程
            int morningSchedule = 0, afternoonSchedule = 0;
            while (morningSchedule == afternoonSchedule)
            {
                morningSchedule = UnityEngine.Random.Range(0, 4);
                afternoonSchedule = UnityEngine.Random.Range(0, 4);
            }
            gameData.morningSchedule = morningSchedule;
            gameData.afternoonSchedule = afternoonSchedule;
            //随机当日的加成系数
            int morningRate = IsHoliday() ? 1 : UnityEngine.Random.Range(2, 3);
            int afternoonRate = IsHoliday() ? 1 : UnityEngine.Random.Range(2, 3);
            gameData.morningRate = morningRate;
            gameData.afternoonRate = afternoonRate;
        }

        #region Block 函数
        /// <summary>
        /// 锁定右键功能
        /// </summary>
        public void BlockRightClick()
        {
            blockRightClick = true;
        }
        /// <summary>
        /// 锁定左键点击
        /// </summary>
        public void BlockClick()
        {
            blockClick = true;
        }
        public void BlockWheel()
        {
            blockWheel = true;
        }
        /// <summary>
        /// 锁定文字履历（不添加新条目）
        /// </summary>
        public void BlockBacklog()
        {
            blockBacklog = true;
        }
        /// <summary>
        /// 锁定存读档
        /// </summary>
        public void BlockSaveLoad()
        {
            blockSaveLoad = true;
        }
        /// <summary>
        /// 解锁右键功能
        /// </summary>
        public void UnblockRightClick()
        {
            blockRightClick = false;
        }
        /// <summary>
        /// 解锁左键点击
        /// </summary>
        public void UnblockClick()
        {
            blockClick = false;
        }
        public void UnblockWheel()
        {
            blockWheel = false;
        }
        /// <summary>
        /// 解锁文字履历
        /// </summary>
        public void UnblockBacklog()
        {
            blockBacklog = false;
        }
        /// <summary>
        /// 解锁存读档
        /// </summary>
        public void UnblockSaveLoad()
        {
            blockSaveLoad = false;
        }

        public bool IsClickBlocked()
        {
            return blockClick;
        }
        public bool IsRightClickBlocked()
        {
            return blockRightClick;
        }
        public bool IsBacklogBlocked()
        {
            return blockBacklog;
        }
        public bool IsSaveLoadBlocked()
        {
            return blockSaveLoad;
        }
        public bool IsWheelBlocked()
        {
            return blockWheel;
        }
        #endregion

        /// <summary>
        /// 游戏执行下一回合
        /// </summary>
        public void MoveOneTurn()
        {
            gameData.gameTurn++;
            //清空InTurnVar
            InitInTurn();
            //随机当日的课表
            RandomCourse();
        }

        /// <summary>
        /// 判断当前回合时候是节假日
        /// </summary>
        public bool IsHoliday()
        {
            //int turn = GetGameVar<int>("回合");
            int turn = gameData.gameTurn;

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
            /* demo1.20 改动
            int turn = GetGameVar<int>("回合");
            */
            int turn = gameData.gameTurn;
            return START_DAY.AddDays(turn);
        }

        /// <summary>
        /// 清空文字记录
        /// </summary>
        public void ClearHistory()
        {
            tempData.backLog.Clear();
        }

        /// <summary>
        /// 添加文字记录
        /// </summary>
        /// <param name="blt">文本记录块</param>
        public void AddHistory(BacklogText blt)
        {
            Queue<BacklogText> history = tempData.backLog;
            if (history.Count > 100)
            {
                history.Dequeue();
            }
            history.Enqueue(blt);
        }

        public Queue<BacklogText> GetHistory()
        {
            return tempData.backLog;
        }

        #region Get / Set 方法
        public void SetTempVar(string key, object value)
        {
            tempData.WriteTempVar(key, value);
        }

        public T GetTempVar<T>(string key)
        {
            return (T)tempData.GetTempVar(key);
        }

        /* demo 1.22 去除
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
        public Hashtable GetSystemVars()
        {
            return datapool.GetSystemTable();
        }
        */
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
            byte[] picdata = (byte[])tempData.GetTempVar("缩略图");
            LoadSaveTool.CreatByteFile(LoadSaveTool.GetSavePath(picname), picdata);
            //更新存档信息
            Dictionary<int, SavingInfo> savedic = tempData.saveInfo;
            //TODO: 获取状态
            SavingInfo info = new SavingInfo();
            info.gameMode = gameData.MODE;
            info.saveTime = DateTime.Now.ToString("yyyy/MM/dd  HH:mm");
            info.saveText = "存档了！";
            info.currentText = tempData.currentText;
            info.picPath = picname;
            //SavingInfo info = new SavingInfo(gamemode, savetime, customtext, picname);
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
            /* demo 1.20 改动
            Hashtable toSave = new Hashtable();
            toSave.Add("GameVar", new Hashtable(datapool.GetGameVarTable()));
            toSave.Add("InTurnVar", new Hashtable(datapool.GetInTurnVarTable()));
            */
            SaveData sv = new SaveData();
            sv.gameData = gameData;
            sv.inturnData = inturnData;
            return JsonConvert.SerializeObject(sv);
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
            SaveData sv = JsonConvert.DeserializeObject<SaveData>(str);
            gameData = sv.gameData;
            inturnData = sv.inturnData;
            eventManager.UpdateEvent();
            //对临时变量重置？
            ClearHistory();
        }

        public void ChangeSave()
        {
            string sysSave = JsonConvert.SerializeObject(tempData.saveInfo);
            LoadSaveTool.CreateFile(LoadSaveTool.GetSavePath("datasv.sav"), sysSave);
            RefreshSavePic();
        }

        public void Delete(int i)
        {
            //删除存档
            string filename = "data" + i + ".sav";
            LoadSaveTool.DeleteFile(LoadSaveTool.GetSavePath(filename));
            //删除截图
            string picname = "data" + i + ".png";
            LoadSaveTool.DeleteFile(LoadSaveTool.GetSavePath(picname));
            //更新存档信息
            //Dictionary<int, SavingInfo> savedic = (Dictionary<int, SavingInfo>)datapool.GetSystemVar("存档信息");
            Dictionary<int, SavingInfo> savedic = tempData.saveInfo;

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
