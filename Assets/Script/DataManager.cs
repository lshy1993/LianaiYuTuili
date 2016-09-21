using Assets.Script.GameStruct;
using Assets.Script.GameStruct.EventSystem;
using Assets.Script.GameStruct.Model;
//using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using System.IO;

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
        private EventManager eventManager;
        private DetectManager detectManager;
        private EnquireManager enquireManager;
        private ReasoningManager reasoningManager;
        private EduManager eduManager;
        private GirlManager girlManager;


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
            SetInTurnVar("文字记录", new Queue<BacklogText>());
        }

        private void InitGame()
        {
            datapool.WriteGameVar("回合", 0);
            SetGameVar("玩家", new Player());
        }

        private void InitSystem()
        {

        }

        private void InitSaving()
        {
            Dictionary<int, SavingInfo> list = new Dictionary<int, SavingInfo>();
            string filename = "datasv.sav";
            string savepath = LoadSaveTool.SAVE_PATH + "/" + filename;
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

        public void RefreshSavePic()
        {
            //读取存档的图片
            Dictionary<int, SavingInfo> list = (Dictionary<int, SavingInfo>)datapool.GetSystemVar("存档信息");
            Dictionary<string, byte[]> savepic = new Dictionary<string, byte[]>();
            foreach (KeyValuePair<int, SavingInfo> kv in list)
            {
                string picpath = LoadSaveTool.SAVE_PATH + "/data" + kv.Key + ".png";
                FileStream fs = new FileStream(picpath, FileMode.Open, FileAccess.Read);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, (int)fs.Length);
                fs.Close();
                savepic.Add(kv.Value.picPath, bytes);
            }
            datapool.WriteSystemVar("存档缩略图", savepic);
        }

        private void InitMultiplay()
        {

            List<bool> musicTable = new List<bool>();
            List<bool> cgTable = new List<bool>();
            List<bool> endingTable = new List<bool>();
            List<bool> caseTable = new List<bool>();

            string filename = "datamp.sav";
            string savepath = LoadSaveTool.SAVE_PATH + "/" + filename;

            if (!LoadSaveTool.IsFileExists(savepath))
            {
                //生成默认的二周目数据表写入文件
                //Hashtable SysSave = new Hashtable();
                Dictionary<string, List<bool>> SysSave = new Dictionary<string, List<bool>>(); 
                //临时测试用
                musicTable.Add(true);
                cgTable.Add(true);
                endingTable.Add(true);
                caseTable.Add(true);

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
                //存在则读取系统数据
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
        }
        
        #region 静态数据
        private void InitStatic()
        {
            InitEvents();
            InitDetects();
            InitCharacters();
            InitGirls();
            InitEnquire();
            InitReasoning();
            InitEdu();
            InitEvidence();
        }

        private void InitEvidence()
        {
            Dictionary<string, Evidence> evidenceDic = EvidenceManager.GetStaticEvidenceDic();
            datapool.WriteStaticVar("证据列表", evidenceDic);
            List<Evidence> holdEvidence = new List<Evidence>();
            foreach(KeyValuePair<string,Evidence> kv in evidenceDic)
            {
                holdEvidence.Add(kv.Value);
            }
            datapool.WriteGameVar("持有证据", holdEvidence);
        }

        private void InitEdu()
        {
            List<EduEvent> events = EduManager.GetStaticEduEvents();
            datapool.WriteStaticVar("养成按钮", events);

            eduManager = EduManager.GetInstance();
            eduManager.Init(events, this);
        }

        private void InitDetects()
        {
            Dictionary<string, DetectEvent> events = DetectManager.GetStaticDetectEvents();
            datapool.WriteStaticVar("侦探事件表", events);

            detectManager = DetectManager.GetInstance();
            detectManager.Init(events, this);
        }

        private void InitEnquire()
        {
            Dictionary<string, EnquireEvent> events = EnquireManager.GetStaticEnquireEvents();
            datapool.WriteStaticVar("询问总表", events);

            enquireManager = EnquireManager.GetInstance();
            enquireManager.Init(events, this);
        }

        private void InitCharacters()
        {
            Dictionary<string, Character> characters = CharacterManager.GetStaticCharacters();
            datapool.WriteStaticVar("人物", characters);

            CharacterManager.GetInstance().characterTable = characters;
        }

        private void InitReasoning()
        {
            Dictionary<string, ReasoningEvent> events = ReasoningManager.GetStaticEnquireEvents();
            datapool.WriteStaticVar("自我推理总表", events);

            reasoningManager = ReasoningManager.GetInstance();
            reasoningManager.Init(events, datapool.GetGameVarTable(), datapool.GetInTurnVarTable());
        }

        private void InitGirls()
        {
            Dictionary<string, Girls> girls = GirlManager.GetStaticGirls();
            datapool.WriteStaticVar("女主角资料表", girls);
        }

        private void InitEvents()
        {
            Dictionary<string, MapEvent> events = EventManager.GetStaticEvent();
            datapool.WriteStaticVar("事件表", events);
            datapool.WriteGameVar("事件状态", EventManager.LoadEventState(events));
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

        public void MoveOneTurn()
        {
            int t = (int)datapool.GetGameVar("回合");
            datapool.WriteGameVar("回合", t + 1);
            //随机当日的课表
            int morningSchedule = 0, afternoonSchedule = 0;
            while (morningSchedule == afternoonSchedule)
            {
                morningSchedule = UnityEngine.Random.Range(0, 4);
                afternoonSchedule = UnityEngine.Random.Range(0, 4);
            }
            SetInTurnVar("上午课程", morningSchedule);
            SetInTurnVar("下午课程", afternoonSchedule);
            //随机当日的加成系数
            int morningRate = UnityEngine.Random.Range(2, 3),
                afternoonRate = UnityEngine.Random.Range(2, 3);
            SetInTurnVar("上午指数", morningRate);
            SetInTurnVar("下午指数", afternoonRate);
        }

        public void AddHistory(BacklogText blt)
        {
            Queue<BacklogText> history = GetInTurnVar<Queue<BacklogText>>("文字记录");
            if (history.Count > 150)
            {
                history.Dequeue();
            }
            history.Enqueue(blt);
        }

        public void SetGameVar(string key, object value)
        {
            datapool.WriteGameVar(key, value);
        }

        public void SetInTurnVar(string key, object value)
        {
            datapool.WriteInTurnVar(key, value);
        }

        public T GetGameVar<T>(string key)
        {
            return (T)datapool.GetGameVar(key);
        }

        public bool ContainsGameVar(string key) { return datapool.GetGameVarTable().ContainsKey(key); }

        public bool ContainsInTurnVar(string key) { return datapool.GetInTurnVarTable().ContainsKey(key); }

        public T GetInTurnVar<T>(string key)
        {
            return (T)datapool.GetInTurnVar(key);
        }

        DataPool GetDataPool() { return datapool; }
        public Hashtable GetGameVars()
        {
            return datapool.GetGameVarTable();
        }

        public Hashtable GetInTurnVars()
        {
            return datapool.GetInTurnVarTable();
        }

        public void Save(int i)
        {
            //string toSave = LoadSaveTool.RijndaelEncrypt(DataToJsonString(), LoadSaveTool.GetKey());
            string toSave = DataToJsonString();
            string filename = "data" + i + ".sav";
            LoadSaveTool.CreateDirectory(LoadSaveTool.SAVE_PATH);
            LoadSaveTool.CreateFile(LoadSaveTool.SAVE_PATH + "/" + filename, toSave);
            //储存截图
            string picname = "data" + i + ".png";
            File.WriteAllBytes(LoadSaveTool.SAVE_PATH + "/" + picname, (byte[])datapool.GetSystemVar("缩略图"));
            //更新存档信息
            Dictionary<int, SavingInfo> savedic = (Dictionary<int, SavingInfo>)datapool.GetSystemVar("存档信息");
            SavingInfo info = new SavingInfo("Avg", DateTime.Now.ToString("yyyy/MM/dd\nHH:mm"), "存档了！", picname);
            if (savedic.ContainsKey(i))
            {
                savedic[i] = info;
            }
            else
            {
                savedic.Add(i, info);
            }
            //string sysSave = LoadSaveTool.RijndaelEncrypt(JsonMapper.Serialize(savedic), LoadSaveTool.GetKey());
            string sysSave = JsonConvert.SerializeObject(savedic);
            LoadSaveTool.CreateFile(LoadSaveTool.SAVE_PATH + "/datasv.sav", sysSave);
            RefreshSavePic();
        }

        private string DataToJsonString()
        {
            Hashtable toSave = new Hashtable();
            toSave.Add("GameVar", datapool.GetGameVarTable());
            toSave.Add("InTurnVar", datapool.GetInTurnVarTable());
            ((Hashtable)toSave["InTurnVar"]).Remove("文字记录");
            return JsonConvert.SerializeObject(toSave);
                //JsonMapper.Serialize(toSave);
        }

        public void Load(int i)
        {
            string filename = "data" + i + ".sav";
            StreamReader savefile = new StreamReader(LoadSaveTool.SAVE_PATH +"/" + filename);
            string toLoad = savefile.ReadToEnd();
            //toLoad = LoadSaveTool.RijndaelDecrypt(toLoad, LoadSaveTool.GetKey());
            LoadDataFromJson(toLoad);
        }

        private void LoadDataFromJson(string str)
        {
            Hashtable hst = JsonConvert.DeserializeObject<Hashtable>(str);
            Hashtable gVars = (Hashtable)hst["GameVar"];
            SetGameVar("回合", (int)gVars["回合"]);
            Player player = (Player)gVars["玩家"];
            SetGameVar("玩家", player);
            Dictionary<string, int> eventStatusDict = (Dictionary<string, int>)gVars["侦探事件位置状态"];
            Dictionary<string, int> placeDict = (Dictionary<string, int>)gVars["事件状态"];

            Hashtable lVars = (Hashtable)hst["InTurnVar"];
            SetInTurnVar("文字位置", (int)lVars["文字位置"]);
            SetInTurnVar("上午课程", (int)lVars["上午课程"]);
            SetInTurnVar("下午课程", (int)lVars["下午课程"]);
            SetInTurnVar("上午指数", (int)lVars["上午指数"]);
            SetInTurnVar("下午指数", (int)lVars["下午指数"]);

            List<int> pressedId = (List<int>)lVars["已威慑证词序号"];
            SetInTurnVar("已威慑证词序号", pressedId);
            List<string> knownInfo = (List<string>)lVars["侦探事件已知信息"];
            SetInTurnVar("侦探事件已知信息", knownInfo);


            //JsonData data = JsonMapper.ToObject(str);
            //datapool.Clear();

            //JsonData gVars = data["GameVar"];

            //SetGameVar("回合", (int)gVars[Regex.Escape("回合")]);
            //SetGameVar("玩家", new Player((string)gVars[Regex.Escape("玩家")]));
            //JsonData detectPlaceStatus = gVars[Regex.Escape("侦探事件位置状态")];
            //JsonData eventStatus = gVars[Regex.Escape("事件状态")];
            //Dictionary<string, int> eventStatusDict = new Dictionary<string, int>();
            //Dictionary<string, int> placeDict = new Dictionary<string, int>();

            //foreach (KeyValuePair<string, JsonData> kv in eventStatus)
            //{
            //    eventStatusDict.Add(kv.Key, (int)kv.Value);
            //}
            //SetGameVar("事件状态", eventStatusDict);

            //foreach (KeyValuePair<string, JsonData> kv in detectPlaceStatus)
            //{
            //    placeDict.Add(kv.Key, (int)kv.Value);
            //}

            //SetGameVar("侦探事件位置状态", placeDict);

            //JsonData lVars = data["InTurnVar"];

            //SetInTurnVar("文字位置", (int)lVars[Regex.Escape("文字位置")]);

            //SetInTurnVar("上午课程", (int)lVars[Regex.Escape("上午课程")]);
            //SetInTurnVar("下午课程", (int)lVars[Regex.Escape("下午课程")]);
            //SetInTurnVar("上午指数", (int)lVars[Regex.Escape("上午指数")]);
            //SetInTurnVar("下午指数", (int)lVars[Regex.Escape("下午指数")]);

            //List<int> pressedId = new List<int>();
            //foreach (JsonData j in lVars[Regex.Escape("已威慑证词序号")])
            //{
            //    pressedId.Add((int)j);
            //}

            //SetInTurnVar("已威慑证词序号", pressedId);

            //List<string> knownInfo = new List<string>();

            //foreach (JsonData j in lVars[Regex.Escape("侦探事件已知信息")])
            //{
            //    knownInfo.Add((string)j);
            //}
            //SetInTurnVar("侦探事件已知信息", knownInfo);
        }
    }
}
