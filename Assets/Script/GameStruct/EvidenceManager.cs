using LitJson;
using Assets.Script.GameStruct.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public class EvidenceManager
    {
        private static EvidenceManager instance;
        private static readonly string EVIDENCE_PATH = "Text/EvidenceConfig/";
        private static readonly string EVIDENCE_DEBUG_PATH = "Text/EvidenceDebug/";

        
        private Hashtable gVars, lVars;
        private Dictionary<string, Evidence> evidenceDic;

        public static EvidenceManager GetInstance()
        {
            if (instance == null) instance = new EvidenceManager();
            return instance;
        }

        public static Dictionary<string, Evidence> GetStaticEvidenceDic()
        {
            Dictionary<string, Evidence> dic = new Dictionary<string, Evidence>();
            string path = Constants.DEBUG ? EVIDENCE_DEBUG_PATH : EVIDENCE_PATH;
            Debug.Log("读取证据列表");
            foreach (TextAsset text in Resources.LoadAll<TextAsset>(path))
            {
                JsonData jsondata = JsonMapper.ToObject(text.text);
                foreach (JsonData jd in jsondata)
                {
                    Evidence ee = new Evidence(jd);
                    //Debug.Log("读取：" + ee.name);
                    dic.Add(ee.name, ee);
                }
            }
            return dic;
        }
    }
}
