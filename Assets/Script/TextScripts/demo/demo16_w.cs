using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo16_w : TextScript
    {
        public demo16_w(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(0),
                f.t("李云萧", "从教学楼到操场要多久呢？"),
                f.t("李云萧", "那么反过来，到教室的时间也应该一样。"),
                f.t("李云萧", "不行，这件证据不能说明问题……"),
                f.HPChange(-1),
                f.t("李云萧", "再重新考虑一下！")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetReasoningNode("Qdemo03");
        }

    }
}
