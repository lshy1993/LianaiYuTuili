using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo20_4 : TextScript
    {
        public demo20_4(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(0),
                f.t("李云萧", "你确认了时间吗？"),
                f.t("叶婷", "没有。"),
                f.t("叶婷", "但是因为声音一响，我就起身了。"),
                f.t("叶婷", "所以我想应该是12点吧。"),
                f.t("李云萧", "……")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ04");
        }

    }
}
