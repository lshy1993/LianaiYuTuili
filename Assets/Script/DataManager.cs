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
            //InitGirls();
        }

        private void InitGirls()
        {
            //throw new NotImplementedException();
        }

        private void InitEvents()
        {
            Dictionary<string, MapEvent> events = EventManager.GetStaticEvent();
            datapool.WriteStaticVar("事件表", events);
            datapool.WriteGameVar("事件状态", EventManager.LoadEventState(events));
            eventManager = EventManager.GetInstance();
            eventManager.Init(
                (Dictionary<string, MapEvent>)datapool.GetStaticVar("事件表"),
                datapool.GetGameVarTable());
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
/*            Debug.Log("MoveOneTurn")*/;
            int t = (int)datapool.GetGameVar("回合");
            //Debug.Log("回合:" + t);
            DateTime day = (DateTime)datapool.GetGameVar("日期");
            //Debug.Log(day.AddDays(1).ToString());
            datapool.WriteGameVar("回合", t + 1);
            datapool.WriteGameVar("日期", day.AddDays(1));
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
