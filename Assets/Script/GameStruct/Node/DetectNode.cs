using Assets.Script.GameStruct.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public class DetectNode : GameNode
    {
        private DetectUIManager uiManager;
        private DetectManager detectManager;
        private DetectEvent detectEvent;
        private DetectPlaceSection section;
        private GameNode next;
        private NodeFactory factory;

        public DetectNode(DataManager manager, GameObject root, PanelSwitch ps, DetectEvent detectEvent)
            : base(manager, root, ps)
        {
            Init(detectEvent);
        }

        public void Init(DetectEvent detectEvent)
        {
            detectManager = DetectManager.GetInstance();

            //关闭对话box
            root.transform.Find("Avg_Panel/DialogBox_Panel").gameObject.SetActive(false);

            //打开invest panel
            uiManager = root.transform.Find("Avg_Panel/Invest_Panel").GetComponent<DetectUIManager>();

            uiManager.transform.gameObject.SetActive(true);

            factory = NodeFactory.GetInstance();

            this.detectEvent = detectEvent;

            uiManager.SetDetectNode(this);

            section = detectEvent.sections.FirstOrDefault().Value;

            uiManager.LoadSection(section);

            uiManager.CheckEvent(detectEvent.id);

            uiManager.SwitchStatus(uiManager.status);
        }

        public override void Update()
        { }

        public void ChooseNext(string entry)
        {
            next = factory.FindTextScript(entry);

            end = true;
        }

        public void MoveTo(string place)
        {
            next = this;

            section = detectEvent.sections[place];

            uiManager.LoadSection(section);

            end = true;
        }

        public override GameNode NextNode()
        {
            return next;
        }

    }
}
