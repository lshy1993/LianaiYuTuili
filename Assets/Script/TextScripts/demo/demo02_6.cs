using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo02_6 : TextScript
    {
        public demo02_6(DataManager manager, GameObject root, PanelSwitch ps) : base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*调查->地上的试卷
                f.OpenDialog(0),
                f.t("李云萧", "几张写有答案的试卷，应该是其他班级的。"),
                f.t("李云萧", "看题目应该就是昨天刚考完的试卷……嗯？"),
                f.t("李云萧", "好像有些碎玻璃，难道是窗户碎的时候，掉在上面的？"),
                f.GetEvidence("00002")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetDetectJudgeNode("detest2");
        }

    }
}
