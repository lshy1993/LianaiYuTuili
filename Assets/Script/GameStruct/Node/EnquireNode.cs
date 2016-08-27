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
        private EnquireEvent enquireEvent;
        private GameNode next;
        private NodeFactory factory;

        public EnquireNode(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps, string eventName)
            : base(gVars, lVars, root, ps)
        {
            Init(eventName);
            ps.SwitchTo_VerifyIterative("Enquire_Panel");
        }

        public void Init(string eventName)
        {
            //获取uimanager
            uiManager = root.transform.Find("Avg_Panel/Enquire_Panel").GetComponent<EnquireUIManager>();
            uiManager.transform.gameObject.SetActive(true);

            enquireManager = EnquireManager.GetInstance();

            factory = NodeFactory.GetInstance();

            this.enquireEvent = enquireManager.LoadEvent(eventName);;

            uiManager.SetEnquireEvent(enquireEvent);
            uiManager.SetEnquireNode(this);

        }

        public override void Update()
        { }

        public void EnquireExit(string entry)
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
