using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class test2 : TextScript
    {
        public test2(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps):base(gVars, lVars, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                f.t("测试","即将进入侦探模式"),
                f.t("测试","json编号detest1"),
            };
        }

        public override GameNode NextNode()
        {
            //return base.NextNode();
            Finish();
            return nodeFactory.GetEduNode("");
            //return nodeFactory.GetReasoningNode("Q001");
            //return nodeFactory.GetEnquireNode("Z1101");
            //return nodeFactory.GetDetectJudgeNode("detest1");
        }

    }
}
