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
        /// 进入考试模式
        /// </summary>
        /// <returns></returns>
        public TestNode GetTestNode()
        {
            return new TestNode(dm, root, ps);
        }

        /// <summary>
        /// 时间转场
        /// </summary>
        /// <returns></returns>
        public TimeSwitchNode GetSwitchNode(string time,string place,string exit)
        {
            return new TimeSwitchNode(dm, root, ps, time, place, exit);
        }

        /// <summary>
        /// 选择分歧
        /// </summary>
        /// <param name="dic">分歧项</param>
        /// <param name="cd">倒计时</param>
        /// <param name="cdexit">时间为零时出口</param>
        /// <returns></returns>
        public SelectNode GetSelectNode(Dictionary<string, string> dic, float cd = 0f, string cdexit="")
        {
            return new SelectNode(dm, root, ps, dic, cd, cdexit);
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
        /// 寻找脚本文件(自动重置文本位置)
        /// </summary>
        /// <param name="name">脚本名</param>
        /// <returns></returns>
        public TextScript FindTextScript(string name)
        {
            dm.gameData.currentTextPos = 0;
            dm.gameData.currentScript = name;
            dm.gameData.MODE = "Avg模式";
            return FindScript(name);
        }

        /// <summary>
        /// 寻找脚本文件(不重置文字位置)
        /// </summary>
        /// <param name="name">脚本名</param>
        /// <returns></returns>
        public TextScript FindTextScriptNoneInit(string name)
        {
            dm.gameData.currentScript = name;
            dm.gameData.MODE = "Avg模式";
            DataManager.GetInstance().tempData.isDiaboxRecover = true;
            return FindScript(name);
        }

        private TextScript FindScript(string name)
        {
            string classStr = SCRIPT_PATH + "." + name;
            Type t = Type.GetType(classStr);
            object[] args = new object[] { dm, root, ps };
            TextScript script = (TextScript)Activator.CreateInstance(t, args);
            return script;
        }

    }
}
