using Assets.Script.GameStruct.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public class EnquireNode : GameNode
    {
        private EnquireManager enquireManager;
        private EnquireUIManager uiManager;
        //private  enquireEvent;
        private GameNode next;
        private NodeFactory factory;

        public EnquireNode(DataManager manager, GameObject root, PanelSwitch ps, string eventName)
            : base(manager, root, ps)
        {
            Init(eventName);
            ps.SwitchTo_VerifyIterative("Avg_Panel", uiManager.WheelStart);
        }

        public void Init(string eventName)
        {
            enquireManager = EnquireManager.GetInstance();
            //获取uimanager
            uiManager = root.transform.Find("Avg_Panel/Enquire_Panel").GetComponent<EnquireUIManager>();
            uiManager.transform.gameObject.SetActive(true);
            factory = NodeFactory.GetInstance();
            EnquireEvent enquireEvent = enquireManager.LoadEvent(eventName);
            List<string> visibleTestimony = enquireManager.LoadTestimony();
            uiManager.SetEnquireEvent(enquireEvent, visibleTestimony);
            uiManager.SetEnquireNode(this);
        }

        public override void Update()
        { }

        public void EnquireExit(string entry)
        {
            uiManager.gameObject.SetActive(false);
            next = factory.FindTextScript(entry);
            end = true;
        }

        public override GameNode NextNode()
        {
            return next;
        }
    }
}
