using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo06_3 : TextScript
    {
        public demo06_3(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "只有1张吗？"),
                f.t("叶婷", "是的，只有一张。"),
                f.t("李云萧", "有没有可能是叠在一起了？"),
                f.t("叶婷", "后来我有检查过，并没有多的试卷。"),
                f.t("李云萧", "是这样啊……")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ00");
        }

    }
}
