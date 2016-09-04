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
    public class GirlManager
    {
        private static GirlManager instance;
        private static readonly string GIRL_PATH = "Text/GirlConfig/";
        private static readonly string GIRL_DEBUG_PATH = "Text/GirlDebug/";

        private Hashtable gVars, lVars;
        private Dictionary<string, Girls> girlsInfo;

        public static GirlManager GetInstance()
        {
            if (instance == null) instance = new GirlManager();
            return instance;
        }

        private GirlManager() { }

        public void Init(Dictionary<string, Girls> girlsInfo, Hashtable gVars, Hashtable lVars)
        {
            this.gVars = gVars;
            this.lVars = lVars;
            this.girlsInfo = girlsInfo;
        }

        public static Dictionary<string, Girls> GetStaticGirls()
        {
            Dictionary<string, Girls> infos = new Dictionary<string, Girls>();
            string path = Constants.DEBUG ? GIRL_DEBUG_PATH : GIRL_PATH;
            foreach (TextAsset text in Resources.LoadAll<TextAsset>(path))
            {
                JsonData jsondata = JsonMapper.ToObject(text.text);
                foreach(JsonData da in jsondata)
                {
                    Girls ee = new Girls(da);
                    //Debug.Log("读取：" + ee.name);
                    infos.Add(ee.name, ee);
                }
            }
            return infos;
        }
    }
}
