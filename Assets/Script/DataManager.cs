using Assets.Script.GameStruct;
using Assets.Script.GameStruct.EventSystem;
using Assets.Script.GameStruct.Model;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public class DataManager
    {
        private static DataManager instance;

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
            datapool.WriteGameVar("文字记录", new List<BacklogText>());
            datapool.WriteGameVar("上午课程", "");
            datapool.WriteGameVar("下午课程", "");
            datapool.WriteGameVar("上午指数", 1f);
            datapool.WriteGameVar("下午指数", 1f);
        }

        private void InitGame()
        {
            InitTurn();
            datapool.WriteGameVar("玩家", Player.GetInstance());
        }

        private void InitSystem()
        {
            //throw new NotImplementedException();
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
            eduManager.Init(events, datapool.GetGameVarTable(), datapool.GetInTurnVarTable());
        }

        private void InitDetects()
        {
            Dictionary<string, DetectEvent> events = DetectManager.GetStaticDetectEvents();
            datapool.WriteStaticVar("侦探事件表", events);

            detectManager = DetectManager.GetInstance();
            detectManager.Init(events, datapool.GetGameVarTable(), datapool.GetInTurnVarTable());

        }

        private void InitEnquire()
        {
            //Debug.Log("InitEnquire");
            Dictionary<string, EnquireEvent> events = EnquireManager.GetStaticEnquireEvents();
            datapool.WriteStaticVar("询问总表", events);

            enquireManager = EnquireManager.GetInstance();
            enquireManager.Init(events, datapool.GetGameVarTable(), datapool.GetInTurnVarTable());
        }

        private void InitCharacters()
        {
            //throw new NotImplementedException();
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
            //eventManager.Init(
            //    (Dictionary<string, MapEvent>)datapool.GetStaticVar("事件表"),
            //    datapool.GetGameVarTable());
            eventManager.Init(events, datapool.GetGameVarTable());
        }

        /// <summary>
        /// 初始化回合
        /// </summary>
        private void InitTurn()
        {
            datapool.WriteGameVar("回合", 0);
            datapool.WriteGameVar("日期", new DateTime(2014, 8, 31));
        }


        public void printEvents()
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
            DateTime day = (DateTime)datapool.GetGameVar("日期");
            datapool.WriteGameVar("回合", t + 1);
            datapool.WriteGameVar("日期", day.AddDays(1));

            Debug.Log("当前回合结束，从" + t + "到" + (t + 1));

            int forenoon = 0, afternoon = 0;
            while (forenoon == afternoon)
            {
                forenoon = UnityEngine.Random.Range(0, 4);
                afternoon = UnityEngine.Random.Range(0, 4);
            }
            datapool.WriteGameVar("上午课程", forenoon);
            datapool.WriteGameVar("下午课程", afternoon);
            int foreindex = UnityEngine.Random.Range(1, 3);
            int afterindex = UnityEngine.Random.Range(1, 3);
            //int afterindex = 1 + float.Parse(UnityEngine.Random.Range(0f, 1f).ToString("0.0"));
            datapool.WriteGameVar("上午指数", foreindex);
            datapool.WriteGameVar("下午指数", afterindex);

        }

        public Hashtable GetGameVars()
        {
            return datapool.GetGameVarTable();
        }

        public Hashtable GetInTurnVars()
        {
            return datapool.GetInTurnVarTable();
        }
    }
}
