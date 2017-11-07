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
    public class EduManager
    {
        private static EduManager instance;

        //private Hashtable gVars, lVars;
        private List<EduEvent> events;//所有的按钮
        private DataManager manager;
        //public EduEvent currentEvent;

        public static EduManager GetInstance()
        {
            if (instance == null) instance = new EduManager();
            return instance;
        }

        private EduManager() { }

        public void Init(List<EduEvent> events, DataManager manager)
        {
            //this.gVars = gVars;
            //this.lVars = lVars;
            this.manager = manager;
            this.events = events;
        }

        public static List<EduEvent> GetStaticEduEvents()
        {
            List<EduEvent> events = new List<EduEvent>();
            string path = Constants.DEBUG ? Constants.EDU_DEBUG_PATH : Constants.EDU_PATH;
            foreach (TextAsset text in Resources.LoadAll<TextAsset>(path))
            {
                JsonData jsondata = JsonMapper.ToObject(text.text);
                foreach(JsonData data in jsondata)
                {
                    EduEvent ee = new EduEvent(data);
                    //Debug.Log("读取：" + ee.name);
                    events.Add(ee);
                }
            }
            return events;
        }

    }
}
