using Assets.Script.GameStruct.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public class ReasoningNode : GameNode
    {
        private ReasoningManager reasoningManager;
        private ReasoningUIManager uiManager;
        private ReasoningEvent reasoningEvent;
        private NodeFactory factory;
        private GameNode next;
        
        public ReasoningNode(DataManager manager, GameObject root, PanelSwitch ps, string eventName)
            : base(manager, root, ps)
        {
            Init(eventName);
            ps.SwitchTo_VerifyIterative_WithOpenCallback("Reasoning_Panel", uiManager.OpenSelection);
            //ps.SwitchTo_VerifyIterative("");
        }

        public void Init(string eventName)
        {
            reasoningManager = ReasoningManager.GetInstance();
            //获取uimanager
            uiManager = root.transform.Find("Avg_Panel/Reasoning_Panel").GetComponent<ReasoningUIManager>();

            uiManager.transform.gameObject.SetActive(true);

            factory = NodeFactory.GetInstance();

            this.reasoningEvent = reasoningManager.LoadEvent(eventName); ;

            uiManager.SetReasoningEvent(reasoningEvent);
            uiManager.SetReasoningNode(this);
        }

        public override void Update()
        { }

        public void ReasoningExit(string entry)
        {
            next = factory.FindTextScript(entry);
            end = true;
        }

        public override GameNode NextNode()
        {
            return next;
        }
    }
}
