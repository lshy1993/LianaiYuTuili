using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo06_1 : TextScript
    {
        public demo06_1(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "你没有看错吗？"),
                f.t("叶婷", "当然，我不会记错的。"),
                f.t("叶婷", "我对我自己的视力还是有信心的。"),
                f.t("李云萧", "……")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ00");
        }

    }
}
