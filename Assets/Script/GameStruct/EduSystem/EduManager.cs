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
        private static readonly string EDU_PATH = "Text/EduConfig/";
        private static readonly string EDU_DEBUG_PATH = "Text/EduDebug/";

        private Hashtable gVars, lVars;
        private List<EduEvent> events;//所有的按钮
        //public EduEvent currentEvent;

        public static EduManager GetInstance()
        {
            if (instance == null) instance = new EduManager();
            return instance;
        }

        private EduManager() { }

        public void Init(List<EduEvent> events, Hashtable gVars, Hashtable lVars)
        {
            this.gVars = gVars;
            this.lVars = lVars;
            this.events = events;
        }

        public static List<EduEvent> GetStaticEduEvents()
        {
            List<EduEvent> events = new List<EduEvent>();
            string path = Constants.DEBUG ? EDU_DEBUG_PATH : EDU_PATH;
            foreach (TextAsset text in Resources.LoadAll<TextAsset>(path))
            {
                JsonData jsondata = JsonMapper.ToObject(text.text);
                EduEvent ee = new EduEvent(jsondata);
                //Debug.Log("读取：" + ee.name);
                events.Add(ee);
            }
            return events;
        }

    }
}
