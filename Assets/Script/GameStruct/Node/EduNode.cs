using Assets.Script.GameStruct.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public class EduNode : GameNode
    {
        private EduUIManager uiManager;

        private List<EduEvent> allEvents;
        private GameNode next;
        private NodeFactory factory;

        public EduNode(DataManager manager, GameObject root, PanelSwitch ps):
            base(manager, root, ps)
        {
            ps.SwitchTo_VerifyIterative("Edu_Panel");
        }

        public override void Init()
        {
            allEvents = DataPool.GetInstance().GetStaticVar("养成按钮") as List<EduEvent>;

            uiManager = root.transform.Find("Edu_Panel").GetComponent<EduUIManager>();
            uiManager.SetEduNode(this);
            uiManager.SetEduEvent(allEvents);

            factory = NodeFactory.GetInstance();
        }

        public override void Update()
        { }

        public void EduExit()
        {
            this.next = factory.GetEndTurnNode();
            base.end = true;
        }

        public void ReturnMap()
        {
            this.next = factory.GetMapNode();
            base.end = true;
        }

        public override GameNode NextNode()
        {
            return next;
        }

    }
}
