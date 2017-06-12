using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo08_4 : TextScript
    {
        public demo08_4(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "绕过来？"),
                f.t("戚海超", "总不可能飞过来吧，要通过走廊过来。"),
                f.t("李云萧", "那么，你花了多久时间呢？"),
                f.t("戚海超", "因为我是跑过来的，也就半分钟不到吧。"),
                f.t("李云萧", "[66ccff]（的确，跑着过去十几秒就能到……）[-]"),
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
