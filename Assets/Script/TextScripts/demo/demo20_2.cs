using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo20_2 : TextScript
    {
        public demo20_2(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(0),
                f.t("李云萧", "过了一会？"),
                f.t("叶婷", "大概10分钟左右吧。"),
                f.t("李云萧", "确定听到了响声？"),
                f.t("叶婷", "这里离办公室很近，所以能听得清楚。"),
                f.t("叶婷", "因为是玻璃碎掉的声音，所以……"),
                f.t("李云萧", "所以？")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ04");
        }

    }
}
