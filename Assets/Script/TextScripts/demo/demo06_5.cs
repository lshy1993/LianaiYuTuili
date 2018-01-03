using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo06_5 : TextScript
    {
        public demo06_5(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "什么都没发生？"),
                f.t("叶婷", "当然是因为我及时阻止了他被！"),
                f.t("叶婷", "人赃俱获，还有什么可以说的！"),
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
