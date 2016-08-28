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
        private static readonly string REASONING_PATH = "Text/ReasoningDebug/";
        private static readonly string REASONING_DEBUG_PATH = "Text/ReasoningDebug/";

        private Hashtable gVars, lVars;
        private Dictionary<string, ReasoningEvent> reasoningEvents;
        private EnquireEvent currentEvent;

        public static ReasoningManager GetInstance()
        {
            if (instance == null) instance = new ReasoningManager();
            return instance;
        }

        private ReasoningManager() { }

        public void Init(Dictionary<string, ReasoningEvent> reasoningEvents, Hashtable gVars, Hashtable lVars)
        {
            this.gVars = gVars;
            this.lVars = lVars;
            this.reasoningEvents = reasoningEvents;
        }

        public ReasoningEvent LoadEvent(string key)
        {
            if (!reasoningEvents.ContainsKey(key)) throw new Exception();

            return reasoningEvents[key];
        }

        public static Dictionary<string, EnquireEvent> GetStaticEnquireEvents()
        {
            Dictionary<string, EnquireEvent> events = new Dictionary<string, EnquireEvent>();
            string path = Constants.DEBUG ? REASONING_DEBUG_PATH : REASONING_PATH;
            foreach (TextAsset text in Resources.LoadAll<TextAsset>(path))
            {
                JsonData jsondata = JsonMapper.ToObject(text.text);

                foreach (JsonData jd in jsondata)
                {
                    EnquireEvent ee = new EnquireEvent(jd);
                    Debug.Log("读取：" + ee.id);
                    events.Add(ee.id, ee);
                }
            }
            return events;
        }
    }
}