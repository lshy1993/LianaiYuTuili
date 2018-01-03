using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo18_r : TextScript
    {
        public demo18_r(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "[66ccff]（她的理由就到这里了……）[-]"),
                f.t("苏梦忆", "李云萧，能证明喵星人的清白吗？"),
                f.t("李云萧", "现在的话，我想应该可以……"),
                f.t("李云萧", "[66ccff]（刚才的那句话，有着很明显的矛盾，出手吧。）[-]")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ03");
        }

    }
}
