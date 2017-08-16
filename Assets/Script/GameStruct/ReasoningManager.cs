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
    public class ReasoningManager
    {
        private static ReasoningManager instance;
        private static readonly string REASONING_PATH = "Text/ReasoningConfig/";
        private static readonly string REASONING_DEBUG_PATH = "Text/ReasoningDebug/";

        public List<string> eviNameList
        {
           get { return DataManager.GetInstance().GetInTurnVar<List<string>>("持有证据"); }
        }

        public Dictionary<string, Evidence> eviDic
        {
            get { return DataPool.GetInstance().GetStaticVar("证据列表") as Dictionary<string, Evidence>; }
        }

        private Dictionary<string, ReasoningEvent> reasoningEvents;
        private ReasoningEvent currentEvent;

        public static ReasoningManager GetInstance()
        {
            if (instance == null) instance = new ReasoningManager();
            return instance;
        }

        private ReasoningManager() { }

        public void Init(Dictionary<string, ReasoningEvent> reasoningEvents)
        {
            this.reasoningEvents = reasoningEvents;
        }

        public ReasoningEvent LoadEvent(string key)
        {
            //if (!reasoningEvents.ContainsKey(key)) throw new Exception();

            ReasoningEvent e = reasoningEvents[key];

            //if (lVars.ContainsKey("自我推理编号"))
            //{
            //    if (e.id != (string)lVars["自我推理编号"])
            //    {
            //        lVars["自我推理编号"] = e.id;
            //    }
            //}
            //else
            //{
            //    lVars.Add("自我推理编号", e.id);
            //}
            currentEvent = e;
            return currentEvent;
        }

        public static Dictionary<string, ReasoningEvent> GetStaticEnquireEvents()
        {
            Dictionary<string, ReasoningEvent> events = new Dictionary<string, ReasoningEvent>();
            string path = Constants.DEBUG ? REASONING_DEBUG_PATH : REASONING_PATH;
            Debug.Log("读取自我推理表");
            foreach (TextAsset text in Resources.LoadAll<TextAsset>(path))
            {
                JsonData jsondata = JsonMapper.ToObject(text.text);
                foreach (JsonData jd in jsondata)
                {
                    ReasoningEvent ee = new ReasoningEvent(jd);
                    Debug.Log("读取：" + ee.id);
                    events.Add(ee.id, ee);
                }
            }
            return events;
        }
    }
}