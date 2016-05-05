using System;
using System.Collections;
using Assets.Script.TextScripts;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public class NodeFactory
    {
        private static NodeFactory instance;
        public static NodeFactory GetInstance()
        {
            if(instance == null) { instance = new NodeFactory(); }
            return instance;

        }
        private Hashtable gVars;
        private GameObject root;
        private PanelSwitch ps;
        private static readonly string SCRIPT_PATH = "Assets.Script.TextScripts";
        private NodeFactory() { }      

        public void Init(Hashtable gVars, GameObject root, PanelSwitch ps)
        {
            this.gVars = gVars;
            this.root = root;
            this.ps = ps;
        }

        public MapNode GetMapNode()
        {
            Debug.Log("创建MapNode");
            return new MapNode(gVars, root, ps);
        }

    
        public EduNode GetEduNode(string type)
        {
            return new EduNode(gVars, root, ps, type);
        }


        public TextScript FindTextScript(string name)
        {
            string classStr = SCRIPT_PATH + "." + name;
            TextScript script = null;
            try
            {
                Type t = Type.GetType(classStr);
                Debug.Log("t == null?" + (t == null));
                object[] args = new object[] { gVars, root, ps };
                script = (TextScript)Activator.CreateInstance(t, args);
                Debug.Log("转换成功：" + classStr);

            }
            catch(Exception e)
            {
                Debug.LogError("文本脚本文件转换错误，请检查名称");
                Debug.LogError(e.Message);
            }
            return script;
        }
    }
}
