using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.GameStruct.Model;

namespace Assets.Script.GameStruct
{
    public class NegotiateNode : GameNode
    {
        private NegotiateUIManager uiManager;
        private NegotiateEvent negotiateEvent;
        private GameNode next;
        private NodeFactory factory;

        public NegotiateNode(DataManager manager, GameObject root, PanelSwitch ps, string negotiateEvent)
            : base(manager, root, ps)
        {
            Init(negotiateEvent);
            ps.SwitchTo_VerifyIterative("Avg_Panel", uiManager.OpenUI);
        }

        public void Init(string negotiateName)
        {
            uiManager = root.transform.Find("Avg_Panel/Negotiate_Panel").GetComponent<NegotiateUIManager>();
            uiManager.transform.gameObject.SetActive(true);

            negotiateEvent = DataManager.GetInstance().staticData.negotiateEvents[negotiateName];

            uiManager.SetCurrentEvent(negotiateEvent);
            uiManager.SetCurrentNode(this);
            factory = NodeFactory.GetInstance();
        }

        public override void Update()
        { }

        public void NegotiateExit()
        {
            next = factory.FindTextScript(negotiateEvent.exit);
            end = true;
        }

        public override GameNode NextNode()
        {
            return next;
        }
    }
}
