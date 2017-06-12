using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo08_x : TextScript
    {
        public demo08_x(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "你现在说的话，充满了……矛、矛盾……"),
                f.t("戚海超", "矛盾？那是什么东西？"),
                f.t("李云萧", "唔……好像，是我搞错了……"),
                f.HPChange(-1),
                f.t("李云萧", "[66ccff]（糟糕，大家对我的印象变差了……）[-]"),
                f.t("李云萧", "[66ccff]（重新再考虑一次。）[-]")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ02");
        }

    }
}
