using System;
using System.Collections;
using Assets.Script.TextScripts;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.GameStruct.Node;
using Assets.Script.GameStruct.Model;

namespace Assets.Script.GameStruct
{
    public class NodeFactory
    {
        private static NodeFactory instance;
        public static NodeFactory GetInstance()
        {
            if (instance == null) { instance = new NodeFactory(); }
            return instance;

        }
        //private Hashtable gVars;
        //private Hashtable lVars;
        private DataManager manager;
        private GameObject root;
        private PanelSwitch ps;
        private AvgPanelSwitch avgPanelSwitch;
        private static readonly string SCRIPT_PATH = "Assets.Script.TextScripts";
        private NodeFactory() { }

        public void Init(DataManager manager, GameObject root, PanelSwitch ps)
        {
            //this.gVars = gVars;
            //this.lVars = lVars;
            this.manager = manager;
            this.root = root;
            this.ps = ps;
            //TextScript.SetAvgPanelSwitch(avgPanelSwitch);
        }

        public MapNode GetMapNode()
        {
            return new MapNode(manager, root, ps);
        }

        public EduNode GetEduNode(string type)
        {
            return new EduNode(manager, root, ps, type);
        }

        public EndTurnNode GetEndTurnNode()
        {
            return new EndTurnNode(manager, root, ps);
        }
        public GameNode GetDetectJudgeNode(string eventName)
        {
            return new DetectJudgeNode(manager, root, ps, eventName, avgPanelSwitch);
        }

        public GameNode GetDetectNode(DetectEvent detectEvent)
        {
            return new DetectNode(manager, root, ps, detectEvent);
        }

        public GameNode GetEnquireNode(string eventName)
        {
            return new EnquireNode(manager, root, ps, eventName);
        }

        public GameNode GetReasoningNode(string eventName)
        {
            return new ReasoningNode(manager, root, ps, eventName);
        }

        public TextScript FindTextScript(string name)
        {
            Debug.Log("获取文件脚本:" + name);
            string classStr = SCRIPT_PATH + "." + name;
            TextScript script = null;
            Type t = Type.GetType(classStr);
            object[] args = new object[] { manager, root, ps };
            script = (TextScript)Activator.CreateInstance(t, args);

            //try
            //{
            //}
            //catch(Exception e)
            //{
            //    Debug.LogError("文本脚本文件转换错误，请检查名称");
            //    Debug.LogError(e.InnerException.Message);
            //}
            return script;
        }
    }
}
