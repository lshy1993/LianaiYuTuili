using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo08_3 : TextScript
    {
        public demo08_3(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "窗外的足球？"),
                f.t("戚海超", "大概是操场上的人在踢足球，然后不小心撞到窗户了。"),
                f.t("李云萧", "[66ccff]（这里可是4楼啊，这得多大的力道。）[-]"),
                f.t("李云萧", "那么之后呢？"),
                f.t("戚海超", "之后……")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ02");
        }

    }
}
