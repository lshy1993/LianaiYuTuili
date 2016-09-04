using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class test6 : TextScript
    {
        public test6(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps):base(gVars, lVars, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                f.t("选项","选项正确"),
                f.t("自我推理","进入证据选择",() => pieces.Count),
            };
        }

        public override GameNode NextNode()
        {
            //return base.NextNode();
            Finish();
            return nodeFactory.GetReasoningNode("Q002");
        }

    }
}
