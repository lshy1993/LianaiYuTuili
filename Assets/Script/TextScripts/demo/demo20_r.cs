using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo20_r : TextScript
    {
        public demo20_r(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(0),
                f.t("苏梦忆", "好像……没什么问题……"),
                f.t("李云萧", "如果她说的是真的，就和某人的[ff6600]证言[-]产生了矛盾。"),
                f.t("苏梦忆", "这里面有矛盾吗？"),
                f.t("李云萧", "[66ccff]（总之，要先解决这个明显的矛盾。）[-]")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ04");
        }

    }
}
