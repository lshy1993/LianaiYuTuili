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
    public class AppManager
    {
        private static AppManager instance;
        private static readonly string APP_PATH = "Text/AppConfig/";
        private static readonly string APP_DEBUG_PATH = "Text/AppDebug/";

        public static AppManager GetInstance()
        {
            if (instance == null) instance = new AppManager();
            return instance;
        }

        public static Dictionary<string, Girl> GetStaticGirls()
        {
            Dictionary<string, Girl> infos = new Dictionary<string, Girl>();
            string path = Constants.DEBUG ? APP_DEBUG_PATH : APP_PATH;
            TextAsset text = Resources.Load<TextAsset>(path + "girls");
            Debug.Log("读取女孩信息表");
            JsonData jsondata = JsonMapper.ToObject(text.text);
            foreach (JsonData da in jsondata)
            {
                Girl ee = new Girl(da);
                infos.Add(ee.name, ee);
                //Debug.Log("读取：" + ee.name);
            }
            return infos;
        }

        public static Dictionary<string, Tour> GetStaticTours()
        {
            Dictionary<string, Tour> infos = new Dictionary<string, Tour>();
            string path = Constants.DEBUG ? APP_DEBUG_PATH : APP_PATH;
            TextAsset text = Resources.Load<TextAsset>(path + "tourguides");
            Debug.Log("读取地点信息表");
            JsonData jsondata = JsonMapper.ToObject(text.text);
            foreach (JsonData da in jsondata)
            {
                Tour ee = new Tour(da);
                infos.Add(ee.name, ee);
                //Debug.Log("读取：" + ee.name);
            }
            return infos;
        }

        public static Dictionary<string, Keyword> GetStaticKeywords()
        {
            Dictionary<string, Keyword> infos = new Dictionary<string, Keyword>();
            string path = Constants.DEBUG ? APP_DEBUG_PATH : APP_PATH;
            TextAsset text = Resources.Load<TextAsset>(path + "keywords");
            Debug.Log("读取帮助词条表");
            JsonData jsondata = JsonMapper.ToObject(text.text);
            foreach (JsonData da in jsondata)
            {
                Keyword ee = new Keyword(da);
                infos.Add(ee.name, ee);
                //Debug.Log("读取：" + ee.name);
            }
            return infos;
        }

        public static Dictionary<int, Routine> GetStaticRoutines()
        {
            Dictionary<int, Routine> infos = new Dictionary<int, Routine>();
            string path = Constants.DEBUG ? APP_DEBUG_PATH : APP_PATH;
            TextAsset text = Resources.Load<TextAsset>(path + "routines");
            Debug.Log("读取日程表");
            JsonData jsondata = JsonMapper.ToObject(text.text);
            foreach (JsonData da in jsondata)
            {
                Routine ee = new Routine(da);
                infos.Add(ee.round, ee);
                //Debug.Log("读取：" + ee.name);
            }
            return infos;
        }
    }
}
