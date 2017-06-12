using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo02_4 : TextScript
    {
        public demo02_4(DataManager manager, GameObject root, PanelSwitch ps) : base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*调查->碎掉的玻璃窗
                f.OpenDialog(0),
                f.t("李云萧", "有一扇窗户破了，碎玻璃散了一地。"),
                f.t("李云萧", "也不知道是什么时候，是怎么碎掉的。")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetDetectJudgeNode("detest2");
        }

    }
}
