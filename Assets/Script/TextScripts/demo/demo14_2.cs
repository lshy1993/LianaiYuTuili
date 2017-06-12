using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo14_2 : TextScript
    {
        public demo14_2(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*调查->办公桌
                f.OpenDialog(0),
                f.t("李云萧", "[66ccff]窗户破碎的原因不是被足球碰了，而是其他的原因？[-]"),
                f.t("李云萧", "[66ccff]不对！怎么可能……[-]"),
                f.HPChange(-1),
                f.t("李云萧", "[66ccff]足球社的项茂可以证明，是足球碰碎了窗户。[-]"),
                f.t("李云萧", "[66ccff]唔……再考虑一下……[-]")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetReasoningNode("Qdemo01");
        }

    }
}
