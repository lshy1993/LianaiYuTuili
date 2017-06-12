using Assets.Script.GameStruct.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public class DetectJudgeNode : GameNode
    {
        private DetectManager detectManager;
        private DetectEvent detectEvent;
        private NodeFactory factory;
        private AvgPanelSwitch avgps;

        public DetectJudgeNode(DataManager manager, GameObject root, PanelSwitch ps, string eventName, AvgPanelSwitch avgps)
            : base(manager, root, ps)
        {
            detectManager = DetectManager.GetInstance();
            detectEvent = detectManager.LoadEvent(eventName);

            factory = NodeFactory.GetInstance();

            if (detectManager.IsCurrentEventFinished()) { Update(); }
            else
            {
                ps.SwitchTo_VerifyIterative("Invest_Panel", Update);
            }
            
        }

        public override void Update() { end = true; }

        public override GameNode NextNode()
        {
            if (detectManager.IsCurrentEventFinished())
            {
                //已经完成当前的所有调查->进入出口脚本
                root.transform.Find("Avg_Panel/CharaGraph_Panel").gameObject.SetActive(true);
                return factory.FindTextScript(detectEvent.eventExit);
            }
            else
            {
                //回到侦探模式
                return factory.GetDetectNode(detectEvent);
            }

        }
    }
}
