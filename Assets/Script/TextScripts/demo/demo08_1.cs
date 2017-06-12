using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo08_1 : TextScript
    {
        public demo08_1(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "教学楼的另一侧？"),
                f.t("戚海超", "嗯，就是对面，我们学校的教学楼是凹字形的。"),
                f.t("李云萧", "你在那一侧干什么？"),
                f.t("戚海超", "刚刚运动完有点热，在那边吹风。"),
                f.t("李云萧", "吹风么……\n[66ccff]（真有闲心……）[-]")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ02");
        }

    }
}
