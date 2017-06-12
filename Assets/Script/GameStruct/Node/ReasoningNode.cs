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

        private bool isnew, isend;

        public ReasoningNode(DataManager manager, GameObject root, PanelSwitch ps, string eventName, string status)
            : base(manager, root, ps)
        {
            isnew = status == "NEW";
            isend = status == "END";
            Init(eventName);
            ps.SwitchTo_VerifyIterative("Avg_Panel", uiManager.OpenSelection);
        }

        public void Init(string eventName)
        {
            reasoningManager = ReasoningManager.GetInstance();
            //获取uimanager
            uiManager = root.transform.Find("Avg_Panel/Reasoning_Panel").GetComponent<ReasoningUIManager>();
            uiManager.transform.gameObject.SetActive(true);

            factory = NodeFactory.GetInstance();
            uiManager.SetIsNew(isnew);
            uiManager.SetIsEnd(isend);

            reasoningEvent = reasoningManager.LoadEvent(eventName);
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
