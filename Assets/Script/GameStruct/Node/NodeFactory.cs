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
        private DataManager dm;
        private GameObject root;
        private PanelSwitch ps;
        private AvgPanelSwitch avgPanelSwitch;
        private static readonly string SCRIPT_PATH = "Assets.Script.TextScripts";
        private NodeFactory() { }

        public void Init(DataManager manager, GameObject root, PanelSwitch ps)
        {
            this.dm = manager;
            this.root = root;
            this.ps = ps;
        }

        /// <summary>
        /// 进入大地图模式
        /// </summary>
        /// <returns></returns>
        public MapNode GetMapNode()
        {
            //dm.SetGameVar("MODE", "大地图模式");
            dm.gameData.MODE = "大地图模式";
            return new MapNode(dm, root, ps);
        }

        /// <summary>
        /// 直接进入养成模式
        /// </summary>
        /// <returns></returns>
        public EduNode GetEduNode()
        {
            //dm.SetGameVar("MODE", "养成模式");
            dm.gameData.MODE = "养成模式";
            return new EduNode(dm, root, ps);
        }

        /// <summary>
        /// 结束回合
        /// </summary>
        /// <returns></returns>
        public EndTurnNode GetEndTurnNode()
        {
            return new EndTurnNode(dm, root, ps);
        }

        /// <summary>
        /// 进入侦探模式(判定)
        /// </summary>
        /// <param name="eventName">剧情名</param>
        /// <returns></returns>
        public GameNode GetDetectJudgeNode(string eventName)
        {
            return new DetectJudgeNode(dm, root, ps, eventName, avgPanelSwitch);
        }

        /// <summary>
        /// 进入侦探剧情
        /// </summary>
        /// <param name="detectEvent">剧情名</param>
        /// <returns></returns>
        public GameNode GetDetectNode(DetectEvent detectEvent)
        {
            //dm.SetGameVar("MODE", "侦探模式");
            dm.gameData.MODE = "侦探模式";
            return new DetectNode(dm, root, ps, detectEvent);
        }

        /// <summary>
        /// 进入询问模式
        /// </summary>
        /// <param name="eventName">询问编号</param>
        /// <returns></returns>
        public GameNode GetEnquireNode(string eventName)
        {
            return new EnquireNode(dm, root, ps, eventName);
        }

        /// <summary>
        /// 进入自我推理模式
        /// </summary>
        /// <param name="eventName">推理名</param>
        /// <param name="status">是否首次进入</param>
        /// <returns></returns>
        public GameNode GetReasoningNode(string eventName, string status = "")
        {
            return new ReasoningNode(dm, root, ps, eventName, status);
        }

        /// <summary>
        /// 进入对峙模式
        /// </summary>
        /// <param name="eventName">对峙名</param>
        /// <returns></returns>
        public GameNode GetNegotiateNode(string eventName)
        {
            return new NegotiateNode(dm, root, ps, eventName);
        }

        /// <summary>
        /// 结束游戏NODE
        /// </summary>
        /// <returns></returns>
        public GameNode GetFinNode()
        {
            return new FinNode(dm, root, ps);
        }

        /// <summary>
        /// 寻找下一脚本文件
        /// </summary>
        /// <param name="name">脚本名</param>
        /// <returns></returns>
        public TextScript FindTextScript(string name)
        {
            //dm.SetGameVar("文字位置", 0);
            dm.gameData.currentTextPos = 0;
            return FindTextScriptNoneInit(name);
        }

        /// <summary>
        /// 寻找下一脚本文件(无切换)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TextScript FindTextScriptNoneInit(string name)
        {
            /* demo1.20 改动
            manager.SetGameVar("当前脚本名", name);
            manager.SetGameVar("MODE", "Avg模式");
            */
            dm.gameData.currentScript = name;
            dm.gameData.MODE = "Avg模式";

            string classStr = SCRIPT_PATH + "." + name;
            Type t = Type.GetType(classStr);
            object[] args = new object[] { dm, root, ps };
            TextScript script = (TextScript)Activator.CreateInstance(t, args);
            return script;
        }

    }
}
