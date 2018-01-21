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
        
        //移动操作
        public void MoveTo(string place)
        {
            //判断是否 首次进入该场景
            if (!string.IsNullOrEmpty(detectEvent.sections[place].entry) && !detectManager.IsEntered(place))
            {
                //结束当前NODE 进入 文本NODE
                ChooseNext(detectEvent.sections[place].entry);
            }
            else
            {
                //普通的移动
                //重新对UI设置新数据
                uiManager.SetDetectNode(this, detectEvent.sections, place, detectEvent.id);
            }
            //修改数据
            detectManager.EnterPlace(place);
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
            string str = string.Empty;
            str += "ID: " + detectEvent.id;
            str += " Exit: " + detectEvent.eventExit;
            return str;
        }
    }
}
