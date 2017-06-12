using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo12_1 : TextScript
    {
        public demo12_1(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*调查->足球草地
                f.OpenDialog(0),
                f.t("李云萧", "这上面长的好像是真的草，不是人工草坪。"),
                f.t("李云萧", "但如果是真草的话，一到秋天就会枯萎变黄的吧。")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetDetectJudgeNode("detest3");
        }

    }
}
