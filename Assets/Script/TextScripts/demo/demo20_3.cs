using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo20_3 : TextScript
    {
        public demo20_3(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(0),
                f.t("李云萧", "离开教室到办公室用了多久？"),
                f.t("叶婷", "大概十几秒钟吧。"),
                f.t("李云萧", "十几秒？你确定吗？"),
                f.t("叶婷", "因为教室的前门出去就是办公室，\n从座位上起来用不了很长时间。"),
                f.t("李云萧", "的确，办公室的旁边就有间教室。")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ04");
        }

    }
}
