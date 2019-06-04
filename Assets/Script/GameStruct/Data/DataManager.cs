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
using Mono.Data.Sqlite;


namespace Assets.Script.GameStruct
{
    public class DataManager
    {
        private static DataManager instance = new DataManager();

        public static readonly DateTime START_DAY = new DateTime(2014, 8, 31);

        public static DataManager GetInstance()
        {
            //if (instance == null)
            //{
            //    Debug.Log("new datamanager");
            //    instance = new DataManager();
            //}
            return instance;
        }

        //各类数据管理器
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
        /// 锁定文字履历，不添加新文字 (菜单/询问)
        /// </summary>
        private bool blockBacklog = false;

        /// <summary>
        /// 禁用存读档(询问)
        /// </summary>
        private bool blockSaveLoad = false;

        public readonly string version = "测试用Demo 1.45";

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

        #region 游戏数据类初始化
        /// <summary>
        /// 游戏临时数据的清空
        /// </summary>
        private void InitTemp()
        {
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

        /// <summary>
        /// 游戏数据初始化 跟随存档
        /// </summary>
        public void InitGame()
        {
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
            //事件状态表的初始化
            gameData.eventStatus = EventManager.LoadEventState(staticData.eventTable);
            //选过的选项清空
            gameData.selectionSwitch = new List<string>();
            //游戏邮件系统的初始化
            gameData.charaMessages = new Dictionary<string, List<int>>();
            string str = staticData.mails[1].chara;
            gameData.charaMessages.Add(str, new List<int>());
            gameData.charaMessages[str].Add(1);
            //朋友圈系统的初始化
            gameData.momentList = new List<Moment>();
            gameData.momentList.Add(new Moment("匿名", "听说有新的留学生进来了……"));
            gameData.momentList.Add(new Moment("匿名", "快开学了，暑假作业我还没有做完。"));
            gameData.momentList.Add(new Moment("匿名", "学校的排练房是不是随时可以借用啊？"));
            gameData.momentList.Add(new Moment("匿名", "真的嘛？你从哪里得来的消息啊？"));
            gameData.momentList.Add(new Moment("匿名", "没做完的家伙，整个暑假在干什么呀。"));
            //随机课程
            RandomCourse();
        }

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
        #endregion
        
        #region 游戏设置 ConfigData 初始化
        private void InitSystem()
        {
            string filename = "config.sav";
            string savepath = SaveLoadTool.GetSavePath(filename);
            if (!SaveLoadTool.IsFileExists(savepath))
            {
                //若不存在则创建默认数据
                ResetSysConfig();
                string toSave = JsonConvert.SerializeObject(configData);
                SaveLoadTool.SaveFile(savepath, toSave);
            }
            else
            {
                try
                {
                    //读取config
                    string toLoad = SaveLoadTool.LoadFile(savepath);
                    configData = JsonConvert.DeserializeObject<ConfigData>(toLoad);
                }
                catch
                {
                    Debug.LogError("存档文件不符，已重置");
                    //出错则采用默认 并覆盖原文件
                    ResetSysConfig();
                    string toSave = JsonConvert.SerializeObject(configData);
                    SaveLoadTool.SaveFile(savepath, toSave);
                }
                
            }
        }

        public void ResetSysConfig()
        {
            //游戏的默认设置
            configData = new ConfigData();
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
            string savepath = SaveLoadTool.GetSavePath(filename);
            if (SaveLoadTool.IsFileExists(savepath))
            {
                //读取存档列表
                string toLoad = SaveLoadTool.LoadFile(savepath);
                list = JsonConvert.DeserializeObject<Dictionary<int, SavingInfo>>(toLoad);
            }
            tempData.saveInfo = list;
            RefreshSavePic();
        }

        /// <summary>
        /// 读取存档的图片并写入Data
        /// </summary>
        public void RefreshSavePic()
        {
            Dictionary<int, SavingInfo> list = tempData.saveInfo;

            Dictionary<string, byte[]> savepic = new Dictionary<string, byte[]>();
            foreach (KeyValuePair<int, SavingInfo> kv in list)
            {
                string picname = "data" + kv.Key + ".png";
                string picpath = SaveLoadTool.GetSavePath(picname);
                FileStream fs = new FileStream(picpath, FileMode.Open, FileAccess.Read);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, (int)fs.Length);
                fs.Close();
                savepic.Add(kv.Value.picPath, bytes);
            }
            tempData.WriteTempVar("存档缩略图", savepic);
        }
        #endregion

        #region 多周目数据初始化
        /// <summary>
        /// 初始化多周目数据
        /// </summary>
        private void InitMultiplay()
        {
            string filename = "datamp.sav";
            string savepath = SaveLoadTool.GetSavePath(filename);

            //判断是否含有datamp文件
            if (!SaveLoadTool.IsFileExists(savepath))
            {
                //若不存在 则生成默认数据表 
                DefaultMultiData();
                //并写入本地文件
                string toSave = JsonConvert.SerializeObject(multiData);
                SaveLoadTool.SaveFile(savepath, toSave);
            }
            else
            {
                try
                {
                    //若文件存在 则读取二周目数据
                    string toLoad = SaveLoadTool.LoadFile(savepath);
                    multiData = JsonConvert.DeserializeObject<MultiData>(toLoad);
                }
                catch
                {
                    Debug.LogError("存档文件不符，已重置");
                    //出差错则覆盖本地文件
                    DefaultMultiData();
                    string toSave = JsonConvert.SerializeObject(multiData);
                    SaveLoadTool.SaveFile(savepath, toSave);
                }
                
            }

        }

        /// <summary>
        /// 生成默认的多周目数据
        /// </summary>
        private void DefaultMultiData()
        {
            multiData = new MultiData();

            foreach (KeyValuePair<int, string> kv in staticData.cgInfo)
            {
                multiData.cgTable.Add(kv.Key, false);
            }

            #region 临时测试用 静态表
            multiData.cgTable[0] = true;
            multiData.cgTable[1] = true;
            multiData.cgTable[2] = true;
            multiData.cgTable[3] = true;
            multiData.endingTable[0] = true;
            multiData.endingTable[1] = true;
            multiData.endingTable[2] = false;
            multiData.endingTable[3] = false;
            //multiData.musicTable[0] = true;
            //multiData.musicTable[1] = true;
            #endregion
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
            InitNegotiate();
            InitExam();
            InitApp();
            //TODO:静态表格 例如cginfo 记录键值与文件全路径
            Dictionary<int, string> cgInfo = new Dictionary<int, string>();
            cgInfo.Add(0, "Background/sky_day");
            cgInfo.Add(1, "Background/classroom");
            cgInfo.Add(2, "Background/corridor");
            cgInfo.Add(3, "Background/gate");
            cgInfo.Add(4, "Background/sky_evening");
            staticData.cgInfo = cgInfo;
        }

        /// <summary>
        /// 初始化电子学生手册
        /// </summary>
        private void InitApp()
        {
            //女主角资料
            staticData.girls = StaticManager.GetStaticGirls();
            //旅游资讯
            staticData.tours = StaticManager.GetStaticTours();
            //帮助词条
            staticData.keywords = StaticManager.GetStaticKeywords();
            //校历
            staticData.routines = StaticManager.GetStaticRoutines();
            //邮件系统
            staticData.mails = StaticManager.GetStaticMails();
            //成就
            staticData.endingInfo = StaticManager.GetStaticEndings();
        }

        private void InitCharacters()
        {
            //人物表情？
            CharacterManager.GetInstance().characterTable = CharacterManager.GetStaticCharacters();
        }

        /// <summary>
        /// 初始化考试题库
        /// </summary>
        private void InitExam()
        {
            staticData.examList = GetStaticQuestiones();
            staticData.bgmTitleList = GetBGMTitle();
            staticData.selections = StaticManager.GetStaticSelections();
        }

        public static Dictionary<int, Question> GetStaticQuestiones()
        {
            Dictionary<int, Question> dic = new Dictionary<int, Question>();

            Sqlite sql = new Sqlite("liantui.db");
            SqliteDataReader dataReader = sql.SelectDataBase("ExamList");
            while (dataReader.Read())
            {
                //读取ID
                int id = dataReader.GetInt32(dataReader.GetOrdinal("ID"));
                Question qs = new Question();
                //读取Name
                int type = dataReader.GetInt32(dataReader.GetOrdinal("Type"));
                qs.isTorF = type == 1;
                int subject = dataReader.GetInt32(dataReader.GetOrdinal("Subject"));
                qs.subject = "a" + subject;
                int hard = dataReader.GetInt32(dataReader.GetOrdinal("Hardness"));
                qs.hard = hard;
                string title = dataReader.GetString(dataReader.GetOrdinal("Title"));
                qs.content = title;
                string select1 = dataReader.GetString(dataReader.GetOrdinal("Select1"));
                qs.choice.Add(select1);
                string select2 = dataReader.GetString(dataReader.GetOrdinal("Select2"));
                qs.choice.Add(select2);
                string select3 = dataReader.GetString(dataReader.GetOrdinal("Select3"));
                qs.choice.Add(select3);
                string select4 = dataReader.GetString(dataReader.GetOrdinal("Select4"));
                qs.choice.Add(select4);
                int answer = dataReader.GetInt32(dataReader.GetOrdinal("Answer"));
                qs.answer = answer;
                dic.Add(id, qs);
            }
            dataReader.Close();
            sql.CloseDataBase();

            //string path = Constants.DEBUG ? Constants.EXAM_DEBUG_PATH : Constants.EXAM_PATH;
            //TextAsset text = Resources.Load<TextAsset>(path + "exam");
            //DebugLog.Log("读取考试题库");
            //JsonData jsondata = JsonMapper.ToObject(text.text);
            //foreach (JsonData jd in jsondata)
            //{
            //    Question ee = new Question(jd);
            //    DebugLog.LogDone("读取：" + ee.UID);
            //    dic.Add(ee.UID, ee);
            //}

            return dic;
        }

        public Dictionary<string, string> GetBGMTitle()
        {
            Sqlite sql = new Sqlite("liantui.db");
            SqliteDataReader dataReader = sql.SelectDataBase("BgmList");
            Dictionary<string, string> bgmTitle = new Dictionary<string, string>();
            while (dataReader.Read())
            {
                //读取ID
                int id = dataReader.GetInt32(dataReader.GetOrdinal("ID"));
                //读取Name
                string filename = dataReader.GetString(dataReader.GetOrdinal("FileName"));
                string title = dataReader.GetString(dataReader.GetOrdinal("Title"));
                bgmTitle.Add(filename, title);
            }
            sql.CloseDataBase();
            return bgmTitle;
        }

        /// <summary>
        /// 初始化证据信息
        /// </summary>
        private void InitEvidence()
        {
            staticData.evidenceDic = StaticManager.GetStaticEvidenceDic();
        }

        /// <summary>
        /// 初始化【养成模式】数据
        /// </summary>
        private void InitEdu()
        {
            staticData.eduEvents = StaticManager.GetStaticEduEvents();
        }

        /// <summary>
        /// 初始化【侦探模式】数据
        /// </summary>
        private void InitDetects()
        {
            staticData.detectEvents = StaticManager.GetStaticDetectEvents();
            DetectManager.GetInstance().Init(this);
        }

        /// <summary>
        /// 初始化【询问模式】数据
        /// </summary>
        private void InitEnquire()
        {
            staticData.enquireEvents = StaticManager.GetStaticEnquireEvents();
            EnquireManager.GetInstance().Init(this);
        }

        /// <summary>
        /// 初始化【推理模式】数据
        /// </summary>
        private void InitReasoning()
        {
            staticData.reasonEvents = StaticManager.GetStaticReasoningEvents();
            ReasoningManager.GetInstance().Init(this);
        }

        /// <summary>
        /// 初始化【对峙模式】数据
        /// </summary>
        private void InitNegotiate()
        {
            staticData.negotiateEvents = StaticManager.GetStaticNegotiateEvents();
            staticData.negotiateTexts = StaticManager.GetStaticNegotiateList();
        }

        /// <summary>
        /// 初始化【事件】数据
        /// </summary>
        private void InitEvents()
        {
            staticData.eventTable = StaticManager.GetStaticEvent();

            //datapool.WriteGameVar("事件状态表", EventManager.LoadEventState(events));
            //gameData.eventStatus = EventManager.LoadEventState(events);

            EventManager.GetInstance().Init(this);
        }
        #endregion

        #region Block 函数
        /// <summary>
        /// 禁用右键功能
        /// </summary>
        public void BlockRightClick()
        {
            blockRightClick = true;
        }
        /// <summary>
        /// 禁用左键点击功能（含滚轮）
        /// </summary>
        public void BlockClick()
        {
            blockClick = true;
        }
        /// <summary>
        /// 禁用鼠标滚轮
        /// </summary>
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
        /// 禁用存读档
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
        /// <summary>
        /// 解锁鼠标滚轮
        /// </summary>
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
        
        #region public 方法
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
            int turn = gameData.gameTurn;
            return START_DAY.AddDays(turn);
        }

        public string GetTodayText()
        {
            return GetToday().ToString("MM月dd日");
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

        #endregion

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
            string toSave = DataToJsonString();
            string filename = "data" + i + ".sav";
            SaveLoadTool.SaveFile(SaveLoadTool.GetSavePath(filename), toSave);
            //储存截图
            string picname = "data" + i + ".png";
            byte[] picdata = (byte[])tempData.GetTempVar("缩略图");
            SaveLoadTool.CreatByteFile(SaveLoadTool.GetSavePath(picname), picdata);
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
            string sysSave = JsonConvert.SerializeObject(savedic);
            SaveLoadTool.SaveFile(SaveLoadTool.GetSavePath("datasv.sav"), sysSave);
            RefreshSavePic();
        }

        private string DataToJsonString()
        {
            //储存内容序列化
            SaveData sv = new SaveData();
            sv.gameData = gameData;
            sv.inturnData = inturnData;
            return JsonConvert.SerializeObject(sv);
        }

        public void Load(int i)
        {
            string filename = "data" + i + ".sav";
            string toLoad = SaveLoadTool.LoadFile(SaveLoadTool.GetSavePath(filename));
            LoadDataFromJson(toLoad);
        }

        private void LoadDataFromJson(string str)
        {
            SaveData sv = JsonConvert.DeserializeObject<SaveData>(str);
            gameData = sv.gameData;
            inturnData = sv.inturnData;
            EventManager.GetInstance().UpdateEvent();
            //Detect模式的复原
            if (gameData.MODE == "侦探模式")
            {
                DetectEvent de = staticData.detectEvents[inturnData.currentDetectEvent];
                tempData.currentDetectEvent = de;
            }
            //对临时变量重置？
            ClearHistory();
        }

        public void ChangeSave()
        {
            string sysSave = JsonConvert.SerializeObject(tempData.saveInfo);
            SaveLoadTool.SaveFile(SaveLoadTool.GetSavePath("datasv.sav"), sysSave);
            RefreshSavePic();
        }

        public void Delete(int i)
        {
            //删除存档
            string filename = "data" + i + ".sav";
            SaveLoadTool.DeleteFile(SaveLoadTool.GetSavePath(filename));
            //删除截图
            string picname = "data" + i + ".png";
            SaveLoadTool.DeleteFile(SaveLoadTool.GetSavePath(picname));
            //更新存档信息
            Dictionary<int, SavingInfo> savedic = tempData.saveInfo;

            if (savedic.ContainsKey(i))
            {
                savedic.Remove(i);
            }
            //写入系统存档
            string sysSave = JsonConvert.SerializeObject(savedic);
            SaveLoadTool.SaveFile(SaveLoadTool.GetSavePath("datasv.sav"), sysSave);
            RefreshSavePic();
        }
        #endregion

    }
}
