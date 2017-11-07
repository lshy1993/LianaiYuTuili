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

        private DataManager manager;
        private Dictionary<string, EnquireEvent> enquireEvents;
        private EnquireEvent currentEvent;
        //可见证词
        private List<string> visibleTestimony;

        public List<int> pressedId
        {
            set { manager.SetInTurnVar("已威慑证词序号", value); }
            get { return manager.GetInTurnVar<List<int>>("已威慑证词序号"); }
        }

        //当前的证词编号
        public int currentId
        {
            set { manager.SetInTurnVar("证词序号", value); }
            get { return manager.GetInTurnVar<int>("证词序号"); }
        }

        public string enquireId
        {
            set { manager.SetInTurnVar("询问编号", value); }
            get { return manager.GetInTurnVar<string>("询问编号"); }
        }

        public Dictionary<string,Evidence> eviDic
        {
            get { return DataPool.GetInstance().GetStaticVar("证据列表") as Dictionary<string, Evidence>; }
        }

        public List<string> eviNameList
        {
            get { return manager.GetInTurnVar<List<string>>("持有证据"); }
        }

        public static EnquireManager GetInstance()
        {
            if (instance == null) instance = new EnquireManager();
            return instance;
        }

        private EnquireManager() { }

        public void Init(Dictionary<string, EnquireEvent> enquireEvents, DataManager manager)
        {
            this.manager = manager;
            this.enquireEvents = enquireEvents;
            this.visibleTestimony = new List<string>();
            this.pressedId = new List<int>();
        }

        public EnquireEvent LoadEvent(string key)
        {
            //if (!enquireEvents.ContainsKey(key)) throw new Exception();
            EnquireEvent e = enquireEvents[key];

            if (!manager.ContainsInTurnVar("询问编号") || e.id != enquireId)
            {
                // 需要刷新的情况
                enquireId = e.id;
                pressedId = new List<int>();
                currentId = 0;
            }
            currentEvent = e;

            SetTestimony();
            return currentEvent;
        }

        public List<string> LoadTestimony()
        {
            return visibleTestimony;
        }

        private void SetTestimony()
        {
            //用于威慑后 重新计算可见证词
            visibleTestimony.Clear();
            for (int i = 0; i < currentEvent.testimony.Count; i++)
            {
                //Debug.Log("证词编号：" + currentEvent.id);
                //Debug.Log(currentEvent.testimony[i].text);
                //Debug.Log("PressedID:" + pressedId);
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
            else if(have ==null || have.Count == 0)
            {
                return false;
            }
            else
            {
                return (have.Intersect(need)).Count() == need.Count();
            }
        }

        public static Dictionary<string, EnquireEvent> GetStaticEnquireEvents()
        {
            Dictionary<string, EnquireEvent> events = new Dictionary<string, EnquireEvent>();
            string path = Constants.DEBUG ? Constants.ENQUIRE_DEBUG_PATH : Constants.ENQUIRE_PATH;
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
