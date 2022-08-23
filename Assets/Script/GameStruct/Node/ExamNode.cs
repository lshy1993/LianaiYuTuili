using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public class ExamNode : GameNode
    {
        private ExamUIManager uiManager;

        private GameNode next;
        private NodeFactory factory;

        public ExamNode(DataManager manager, GameObject root, PanelSwitch ps):
            base(manager, root, ps)
        {
            ps.SwitchTo_VerifyIterative("Exam_Panel");
        }

        public override void Init()
        {
            uiManager = root.transform.Find("Exam_Panel").GetComponent<ExamUIManager>();
            uiManager.SetExamNode(this);
            //uiManager.SetExamEvent(allEvents);

            factory = NodeFactory.GetInstance();
        }

        public override void Update()
        { }

        public void ExamExit()
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
