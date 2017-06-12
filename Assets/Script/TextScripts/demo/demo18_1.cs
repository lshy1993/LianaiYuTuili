using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo18_1 : TextScript
    {
        public demo18_1(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "你的教室在？"),
                f.t("叶婷", "距离办公室最近的一间，出门左拐就是办公室。"),
                f.t("叶婷", "这样近的话，我们就能随时注意办公的动静了。"),
                f.t("李云萧", "[66ccff]（这种没什么用的好处……）[-]")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ03");
        }

    }
}
