using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo18_2 : TextScript
    {
        public demo18_2(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "奇怪的声音？"),
                f.t("叶婷", "我想应该是玻璃被打碎的声音吧，很清脆的声响。"),
                f.t("叶婷", "而且你看，办公室的窗户不是破了吗？"),
                f.t("李云萧", "嗯，结果上说没错。")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ03");
        }

    }
}
