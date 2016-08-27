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
    public class EnquireManager
    {
        private static EnquireManager instance;
        private static readonly string ENQUIRE_PATH = "Text/EnquireDebug/";
        private static readonly string ENQUIRE_DEBUG_PATH = "Text/EnquireDebug/";

        private Hashtable gVars, lVars;
        private Dictionary<string, EnquireEvent> enquireEvents;
        private EnquireEvent currentEvent;

        public static EnquireManager GetInstance()
        {
            if (instance == null) instance = new EnquireManager();
            return instance;
        }

        private EnquireManager() { }

        public void Init(Dictionary<string, EnquireEvent> enquireEvents, Hashtable gVars, Hashtable lVars)
        {
            this.gVars = gVars;
            this.lVars = lVars;
            this.enquireEvents = enquireEvents;
        }

        public EnquireEvent LoadEvent(string key)
        {
            if (!enquireEvents.ContainsKey(key)) throw new Exception();

            return enquireEvents[key];
        }

        public static Dictionary<string, EnquireEvent> GetStaticEnquireEvents()
        {
            Dictionary<string, EnquireEvent> events = new Dictionary<string, EnquireEvent>();
            string path = Constants.DEBUG ? ENQUIRE_DEBUG_PATH : ENQUIRE_PATH;
            foreach (TextAsset text in Resources.LoadAll<TextAsset>(path))
            {
                JsonData jsondata = JsonMapper.ToObject(text.text);

                foreach (JsonData jd in jsondata){
                    EnquireEvent ee = new EnquireEvent(jd);
                    Debug.Log("读取：" + ee.id);
                    events.Add(ee.id, ee);
                }
            }
            return events;
        }

    }
}
