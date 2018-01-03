using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo06_1r : TextScript
    {
        public demo06_1r(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "[66ccff]（她的理由就到这里了……）[-]"),
                f.t("苏梦忆", "李云萧，能证明喵星人的清白吗？"),
                f.t("李云萧", "现在是不可能的，发现的证据还不够……"),
                f.t("李云萧", "[66ccff]（但是，如果能把嫌疑人的范围扩大就好了。）[-]")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ01");
        }

    }
}
