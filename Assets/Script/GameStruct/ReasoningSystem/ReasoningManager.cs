using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;

namespace Assets.Script.GameStruct
{
    /// <summary>
    /// 自我推理数据 处理方法
    /// </summary>
    public class ReasoningManager
    {
        private static ReasoningManager instance = new ReasoningManager();
        private DataManager manager;

        /// <summary>
        /// 当前持有证据
        /// </summary>
        public List<string> eviNameList
        {
           get { return manager.inturnData.holdEvidences; }
        }

        /// <summary>
        /// 证据总表
        /// </summary>
        public Dictionary<string, Evidence> eviDic
        {
            get { return manager.staticData.evidenceDic; }
        }

        /// <summary>
        /// 自我推理总表
        /// </summary>
        private Dictionary<string, ReasoningEvent> reasoningEvents
        {
            get { return manager.staticData.reasonEvents; }
        }

        /// <summary>
        /// 当前所处自我推理事件
        /// </summary>
        private ReasoningEvent currentEvent;

        public static ReasoningManager GetInstance()
        {
            //if (instance == null) instance = new ReasoningManager();
            return instance;
        }

        private ReasoningManager() { }

        public void Init(DataManager manager)
        {
            this.manager = manager;
        }

        public ReasoningEvent LoadEvent(string key)
        {
            ReasoningEvent e = reasoningEvents[key];
            currentEvent = e;
            return currentEvent;
        }

    }
}