using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo06_x : TextScript
    {
        public demo06_x(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "你现在说的话，充满了……矛、矛盾……"),
                f.t("叶婷", "哪里？我怎么不知道？"),
                f.t("苏梦忆", "对不起，我也没看出来……"),
                f.t("李云萧", "唔……好像，是我搞错了……"),
                f.HPChange(-1),
                f.t("李云萧", "[66ccff]（糟糕，大家对我的印象变差了……）[-]"),
                f.t("李云萧", "[66ccff]（重新再考虑一次。）[-]")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            if (manager.inturnData.gameOver)
            {
                return nodeFactory.FindTextScript("demo_fail");
            }
            return nodeFactory.GetEnquireNode("demoZ00");
        }

    }
}
