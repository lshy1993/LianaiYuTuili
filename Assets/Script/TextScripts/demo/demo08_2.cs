using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo08_2 : TextScript
    {
        public demo08_2(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "你看到窗户被打碎了？"),
                f.t("戚海超", "是的。"),
                f.t("李云萧", "那是被什么东西打碎的呢？"),
                f.t("戚海超", "这个，是个足球。"),
                f.t("李云萧", "足球？"),
                f.t("戚海超", "对，我没有看错。"),
                f.t("李云萧", "[66ccff]（看来这是个重要线索……）[-]"),
                f.t("李云萧", "请你加入证词里吧。"),
                f.t("戚海超", "好的。")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ02");
        }

    }
}
