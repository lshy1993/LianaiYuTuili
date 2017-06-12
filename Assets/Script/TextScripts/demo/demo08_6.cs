using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo08_6 : TextScript
    {
        public demo08_6(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "你确定没有看错？"),
                f.t("戚海超", "没有，那时候，他就拿着打开的密封袋。"),
                f.t("李云萧", "唔……"),
                f.t("戚海超", "所以，叶婷说的没有错。")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ02");
        }

    }
}
