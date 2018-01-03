using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo18_6 : TextScript
    {
        public demo18_6(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "你为什么这么肯定就是他？"),
                f.t("叶婷", "因为他被我抓了个现行！"),
                f.t("李云萧", "可能他只是单纯地拿起来看看而已……"),
                f.t("李云萧", "[66ccff]（怎么我也说这句话了……）[-]"),
                f.t("叶婷", "小偷也不会承认自己偷了的。"),
                f.t("李云萧", "唔……\n[66ccff]（没有证据……无法反驳……）[-]")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ03");
        }

    }
}
