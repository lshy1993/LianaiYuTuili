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
        private static readonly string ENQUIRE_PATH = "Text/EnquireConfig/";
        private static readonly string ENQUIRE_DEBUG_PATH = "Text/EnquireDebug/";

        private Hashtable gVars, lVars;
        private Dictionary<string, EnquireEvent> enquireEvents;
        public EnquireEvent currentEvent;

        public List<string> visibleTestimony;//可见证词
        public List<int> pressedId;
        public int currentId;//当前的证词编号

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
            this.visibleTestimony = new List<string>();
            this.pressedId = new List<int>();
        }

        public EnquireEvent LoadEvent(string key)
        {
            //if (!enquireEvents.ContainsKey(key)) throw new Exception();

            EnquireEvent e = enquireEvents[key];

            if (lVars.ContainsKey("询问编号"))
            {
                if (e.id != (string)lVars["询问编号"])
                {
                    lVars["询问编号"] = e.id;
                    if (lVars.ContainsKey("证词序号")) lVars["证词序号"] = 0;
                    else lVars.Add("证词序号", 0);
                    if (lVars.ContainsKey("已威慑证词序号"))
                    {
                        lVars["已威慑证词序号"] = new List<int>();
                    }
                    else
                    {
                        lVars.Add("已威慑证词序号", new List<int>());
                    }
                }
            }
            else
            {
                lVars.Add("询问编号", e.id);
                if (lVars.ContainsKey("证词序号")) lVars["证词序号"] = 0;
                else lVars.Add("证词序号", 0);
                if (lVars.ContainsKey("已威慑证词序号"))
                {
                    lVars["已威慑证词序号"] = new List<int>();
                }
                else
                {
                    lVars.Add("已威慑证词序号", new List<int>());
                }

            }
            currentEvent = e;
            currentId = (int)lVars["证词序号"];
            pressedId = (List<int>)lVars["已威慑证词序号"];
            SetTestimony();
            return currentEvent;
        }

        private void SetTestimony()
        {
            //用于威慑后 重新计算可见证词
            visibleTestimony.Clear();
            for (int i = 0; i < currentEvent.testimony.Count; i++)
            {
                Debug.Log("证词编号：" + currentEvent.id);
                Debug.Log(currentEvent.testimony[i].text);
                Debug.Log("PressedID:" + pressedId);
                if (CheckVisible(pressedId, currentEvent.testimony[i].condition))
                {
                    visibleTestimony.Add(currentEvent.testimony[i].text);
                }
            }
        }

        private bool CheckVisible(List<int> have, List<int> need)
        {
            if (need == null || need.Count == 0)
            {
                return true;
            }
            else
            {
                return (have.Intersect(need)).Count() == need.Count();
            }
        }

        public static Dictionary<string, EnquireEvent> GetStaticEnquireEvents()
        {
            Dictionary<string, EnquireEvent> events = new Dictionary<string, EnquireEvent>();
            string path = Constants.DEBUG ? ENQUIRE_DEBUG_PATH : ENQUIRE_PATH;
            foreach (TextAsset text in Resources.LoadAll<TextAsset>(path))
            {
                JsonData jsondata = JsonMapper.ToObject(text.text);

                foreach (JsonData jd in jsondata)
                {
                    EnquireEvent ee = new EnquireEvent(jd);
                    //Debug.Log("读取：" + ee.id);
                    events.Add(ee.id, ee);
                }
            }
            return events;
        }
    }
}
