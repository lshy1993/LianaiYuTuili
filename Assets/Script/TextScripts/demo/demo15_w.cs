using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo15_w : TextScript
    {
        public demo15_w(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(0),
                f.t("李云萧", "这个证据能指出他去办公室的理由吗？"),
                f.t("李云萧", "唔……想不明白……"),
                f.HPChange(-1),
                f.t("李云萧", "不行，得再想一遍……")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetReasoningNode("Qdemo02");
        }

    }
}
