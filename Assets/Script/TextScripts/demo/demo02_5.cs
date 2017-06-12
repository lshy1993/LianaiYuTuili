using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo02_5 : TextScript
    {
        public demo02_5(DataManager manager, GameObject root, PanelSwitch ps) : base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*调查->碎玻璃
                f.OpenDialog(0),
                f.t("李云萧", "掉在地上的碎玻璃，好像上面有点红色的血迹。"),
                f.t("李云萧", "是有什么人被玻璃划破了吗？"),
                f.GetEvidence("碎玻璃")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetDetectJudgeNode("detest2");
        }

    }
}
