using Assets.Script.GameStruct.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public class TestNode : GameNode
    {
        private ExamUIManager uiManager;

        private GameNode next;
        private NodeFactory factory;

        public TestNode(DataManager manager, GameObject root, PanelSwitch ps):
            base(manager, root, ps)
        {
            ps.SwitchTo_VerifyIterative("Test_Panel");
        }

        public override void Init()
        {
            uiManager = root.transform.Find("Test_Panel").GetComponent<ExamUIManager>();


            factory = NodeFactory.GetInstance();
        }

        public override void Update()
        { }

        public void TestExit()
        {
            this.next = factory.GetEndTurnNode();
            base.end = true;
        }

        public override GameNode NextNode()
        {
            return next;
        }
    }
}
