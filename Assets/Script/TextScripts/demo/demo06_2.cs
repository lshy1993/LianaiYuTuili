using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo06_2 : TextScript
    {
        public demo06_2(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "偷看是指？"),
                f.t("叶婷", "我躲在办公室的窗外，一直观察着。"),
                f.t("李云萧", "[66ccff]（这样做不太好吧……）[-]"),
                f.t("李云萧", "你看到了什么？"),
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ00");
        }

    }
}
