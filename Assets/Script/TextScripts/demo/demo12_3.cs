using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo12_3 : TextScript
    {
        public demo12_3(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*调查->篮球场
                f.OpenDialog(0),
                f.t("李云萧", "没想到午休期间还有人在打球。"),
                f.t("李云萧", "虽然我也很喜欢打球，至少中午不会。")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetDetectJudgeNode("detest3");
        }

    }
}
