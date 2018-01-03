using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo20_4 : TextScript
    {
        public demo20_4(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(0),
                f.t("李云萧", "从你起身到开门用了多久？"),
                f.t("叶婷", "大概十几秒钟吧。"),
                f.t("叶婷", "然后我就遇到了气喘吁吁的威海超。"),
                f.t("李云萧", "[66ccff]（十几秒吗？）[-]"),
                f.t("叶婷", "因为教室的前门出去就是办公室，\n从座位上起来用不了很长时间。"),
                f.t("李云萧", "……"),
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ04");
        }

    }
}
