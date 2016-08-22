using Assets.Script.GameStruct.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public class DetectJudgeNode : GameNode
    {
        private DetectManager detectManager;
        private DetectEvent detectEvent;
        private NodeFactory factory;
        public DetectJudgeNode(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps, string eventName)
            : base(gVars, lVars, root, ps)
        {
            detectManager = DetectManager.GetInstance();
            LoadEvent(eventName);
        }

        public void LoadEvent(string eventName)
        {
            detectManager.LoadEvent(eventName);
            detectEvent = detectManager.GetCurrentEvent();
        }


        public override void Update() { end = true; }

        public override GameNode NextNode()
        {
            Debug.Log(detectEvent.sections.FirstOrDefault());
            if (detectManager.IsCurrentEventFinished())
            {
                return factory.FindTextScript(detectEvent.eventExit);
            }
            else if (!detectManager.IsEntered(detectEvent.sections.FirstOrDefault().Value.place))
            {
                return factory.FindTextScript(detectEvent.sections.FirstOrDefault().Value.entry);
            }
            else
            {
                return factory.GetDetectNode(detectEvent);
            }

        }
    }
}
