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
            uiManager = root.transform.Find("Avg_Panel/Invest_Panel").GetComponent<DetectUIManager>();

            factory = NodeFactory.GetInstance();
            this.detectEvent = detectEvent;

            //Debug.Log(detectManager.CurrentPlace());
            uiManager.SetDetectNode(this, detectEvent.sections, detectManager.CurrentPlace(), detectEvent.id);
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
            //移动操作
            if (!string.IsNullOrEmpty(detectEvent.sections[place].entry) && !detectManager.IsEntered(place))
            {
                //首次进入则触发
                detectManager.EnterPlace(place);
                ChooseNext(detectEvent.sections[place].entry);
            }
            else
            {
                //普通移动
                detectManager.EnterPlace(place);
                uiManager.SetDetectNode(this, detectEvent.sections, place, detectEvent.id);
                uiManager.ShowCharaContainer();
            }
            //section = detectEvent.sections[place];
            //uiManager.LoadSection(section);
            //uiManager.SwitchStatus(Constants.DETECT_STATUS.FREE);
        }

        public void SetKnown(string name)
        {
            detectManager.AddToKnown(name);
        }

        public override GameNode NextNode()
        {
            return next;
        }

        public override string ToString()
        {
            return detectEvent.id.ToString();
        }
    }
}
