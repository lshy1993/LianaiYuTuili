using System;
using System.Collections;
using Assets.Script.TextScripts;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.GameStruct.Node;

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
        private Hashtable lVars;
        private GameObject root;
        private PanelSwitch ps;
        private static readonly string SCRIPT_PATH = "Assets.Script.TextScripts";
        private NodeFactory() { }      

        public void Init(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps)
        {
            this.gVars = gVars;
            this.lVars = lVars;
            this.root = root;
            this.ps = ps;
        }

        public MapNode GetMapNode()
        {
            return new MapNode(gVars, lVars, root, ps);
        }
    
        public EduNode GetEduNode(string type)
        {
            return new EduNode(gVars, lVars, root, ps, type);
        }

        public EndTurnNode GetEndTurnNode()
        {
            return new EndTurnNode(gVars, lVars, root, ps);
        }

        public TextScript FindTextScript(string name)
        {
            string classStr = SCRIPT_PATH + "." + name;
            TextScript script = null;
            try
            {
                Type t = Type.GetType(classStr);
                Debug.Log("t == null?" + (t == null));
                object[] args = new object[] { gVars, lVars, root, ps };
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
