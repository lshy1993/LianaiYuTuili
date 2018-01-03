using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo18_3 : TextScript
    {
        public demo18_3(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "正好11点50分？"),
                f.t("叶婷", "是的，我没有记错。"),
                f.t("李云萧", "不过，你是怎么知道具体时间的？"),
                f.t("叶婷", "那时候，我正好看了眼办公室里的挂钟。"),
                f.t("李云萧", "挂钟？"),
                f.t("叶婷", "就挂在办公室外窗那面墙上的。"),
                f.t("李云萧", "[66ccff]（的确，进门之后可以看到挂钟）[-]"),
                f.t("李云萧", "那么之后？")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ03");
        }

    }
}
