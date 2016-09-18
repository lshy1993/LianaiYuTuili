using Assets.Script.GameStruct;
using Assets.Script.GameStruct.EventSystem;
using Assets.Script.GameStruct.Model;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
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

        public static readonly DateTime START_DAY = new DateTime();

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
            InitGame();
            InitInTurn();
        }

        private void InitInTurn()
        {
            //throw new NotImplementedException();
            SetInTurnVar("文字记录", new List<BacklogText>());
        }

        private void InitGame()
        {
            InitTurn();
            SetGameVar("玩家", new Player());
        }

        private void InitSystem()
        {
        }

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

        /// <summary>
        /// 初始化回合
        /// </summary>
        private void InitTurn()
        {
            datapool.WriteGameVar("回合", 0);
            //datapool.WriteGameVar("日期", new DateTime(2014, 8, 31));
        }


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
            int morningSchedule = 0, afternoonSchedule = 0;

            while (morningSchedule == afternoonSchedule)
            {
                morningSchedule = UnityEngine.Random.Range(0, 4);
                afternoonSchedule = UnityEngine.Random.Range(0, 4);
            }

            SetInTurnVar("上午课程", morningSchedule);
            SetInTurnVar("下午课程", afternoonSchedule);
            int morningRate = UnityEngine.Random.Range(1, 3),
                afternoonRate = UnityEngine.Random.Range(1, 3);
            SetInTurnVar("上午指数", morningRate);
            SetInTurnVar("下午指数", afternoonRate);
        }

        public void AddHistory(BacklogText blt)
        {
            List<BacklogText> history = GetInTurnVar<List<BacklogText>>("文字记录");
            Debug.Log("history == null?" + history == null);
            history.Add(blt);
            //blt.Add(new BacklogText(name, dialog));
            //DataPool.GetInstance().WriteGameVar("文字记录", blt);

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
            string toSave = LoadSaveTool.RijndaelEncrypt(DataToJsonString(), LoadSaveTool.GetKey());
            string filename = "savedata" + i + ".sav";

            LoadSaveTool.CreateDirectory(LoadSaveTool.SAVE_PATH);
            LoadSaveTool.CreateFile(LoadSaveTool.SAVE_PATH + "/" + filename, toSave);
        }

        private string DataToJsonString()
        {
            Hashtable toSave = new Hashtable();
            toSave.Add("GameVar", datapool.GetGameVarTable());
            toSave.Add("InTurnVar", datapool.GetInTurnVarTable());
            return JsonMapper.Serialize(toSave);
        }

        public void Load(int i)
        {
            string filename = "savedata" + i + ".sav";

            StreamReader savefile = new StreamReader(LoadSaveTool.SAVE_PATH +"/" + filename);
            string toLoad = savefile.ReadToEnd();
            LoadDataFromJson(toLoad);
        }

        private void LoadDataFromJson(string str)
        {
            JsonData data = JsonMapper.ToObject(str);
            datapool.Clear();

            JsonData gVars = data["GameVar"];

            SetGameVar("回合", (int)gVars[Regex.Escape("回合")]);
            SetGameVar("玩家", new Player((string)gVars[Regex.Escape("玩家")]));
            JsonData detectPlaceStatus = gVars[Regex.Escape("侦探事件位置状态")];
            JsonData eventStatus = gVars[Regex.Escape("事件状态")];
            Dictionary<string, int> eventStatusDict = new Dictionary<string, int>();
            Dictionary<string, int> placeDict = new Dictionary<string, int>();

            foreach (KeyValuePair<string, JsonData> kv in eventStatus)
            {
                eventStatusDict.Add(kv.Key, (int)kv.Value);
            }
            SetGameVar("事件状态", eventStatusDict);

            foreach (KeyValuePair<string, JsonData> kv in detectPlaceStatus)
            {
                placeDict.Add(kv.Key, (int)kv.Value);
            }

            SetGameVar("侦探事件位置状态", placeDict);





            JsonData lVars = data["InTurnVar"];

            SetInTurnVar("文字位置", (int)lVars[Regex.Escape("文字位置")]);

            SetInTurnVar("上午课程", (int)lVars[Regex.Escape("上午课程")]);
            SetInTurnVar("下午课程", (int)lVars[Regex.Escape("下午课程")]);
            SetInTurnVar("上午指数", (int)lVars[Regex.Escape("上午指数")]);
            SetInTurnVar("下午指数", (int)lVars[Regex.Escape("下午指数")]);

            List<int> pressedId = new List<int>();
            foreach (JsonData j in lVars[Regex.Escape("已威慑证词序号")])
            {
                pressedId.Add((int)j);
            }

            SetInTurnVar("已威慑证词序号", pressedId);

            List<string> knownInfo = new List<string>();

            foreach (JsonData j in lVars[Regex.Escape("侦探事件已知信息")])
            {
                knownInfo.Add((string)j);
            }
            SetInTurnVar("侦探事件已知信息", knownInfo);
        }
    }
}
