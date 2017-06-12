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
        private DataManager manager;
        private GameObject root;
        private PanelSwitch ps;
        private AvgPanelSwitch avgPanelSwitch;
        private static readonly string SCRIPT_PATH = "Assets.Script.TextScripts";
        private NodeFactory() { }

        public void Init(DataManager manager, GameObject root, PanelSwitch ps)
        {
            this.manager = manager;
            this.root = root;
            this.ps = ps;
        }

        public MapNode GetMapNode()
        {
            manager.SetGameVar("MODE", "大地图模式");
            return new MapNode(manager, root, ps);
        }
    
        public EduNode GetEduNode()
        {
            manager.SetGameVar("MODE", "养成模式");
            return new EduNode(manager, root, ps);
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
            manager.SetGameVar("MODE", "侦探模式");
            return new DetectNode(manager, root, ps, detectEvent);
        }

        public GameNode GetEnquireNode(string eventName)
        {
            return new EnquireNode(manager, root, ps, eventName);
        }

        public GameNode GetReasoningNode(string eventName, string status = "")
        {
            return new ReasoningNode(manager, root, ps, eventName, status);
        }

        public GameNode GetNegotiateNode(string eventName)
        {
            return new NegotiateNode(manager, root, ps, eventName);
        }

        public GameNode GetFinNode()
        {
            return new FinNode(manager, root, ps);
        }

        public TextScript FindTextScript(string name)
        {
            manager.SetGameVar("文字位置", 0);
            return FindTextScriptNoneInit(name);
        }

        public TextScript FindTextScriptNoneInit(string name)
        {
            manager.SetGameVar("当前脚本名", name);
            manager.SetGameVar("MODE", "Avg模式");

            string classStr = SCRIPT_PATH + "." + name;
            Type t = Type.GetType(classStr);
            object[] args = new object[] { manager, root, ps };
            TextScript script = (TextScript)Activator.CreateInstance(t, args);
            return script;
        }

    }
}
