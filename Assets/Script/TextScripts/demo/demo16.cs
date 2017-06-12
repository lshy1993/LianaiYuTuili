using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo16 : TextScript
    {
        public demo16(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(0),
                f.t("李云萧", "从教学楼赶到操场需要5分钟，回来的时候也需要这么久。"),
                f.t("李云萧", "戚海超目击事件的真实时间，至少也是12点05分之后的事了。"),
                f.t("李云萧", "原来是这样！")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetReasoningNode("Qdemo03", "END");
            //return nodeFactory.FindTextScript("demo17");
        }

    }
}
