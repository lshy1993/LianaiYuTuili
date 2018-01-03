using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo02_7 : TextScript
    {
        public demo02_7(DataManager manager, GameObject root, PanelSwitch ps) : base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*调查->逃生图纸
                f.OpenDialog(0),
                f.t("李云萧", "逃生图，发生火灾的时候用的。"),
                f.t("李云萧", "上面印有教学区的平面图。"),
                f.GetEvidence("00003")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetDetectJudgeNode("detest2");
        }

    }
}
