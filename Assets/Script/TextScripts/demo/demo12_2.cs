using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo12_2 : TextScript
    {
        public demo12_2(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*调查->球门
                f.OpenDialog(0),
                f.t("李云萧", "已经开始掉漆的球门，虽然没有挂网。"),
                f.t("苏梦忆", "没有球网的话，怎么知道有没有进球？"),
                f.t("李云萧", "那不是很简单？如果球从两个柱子之间进去，就算作是进球。"),
                f.t("李云萧", "从柱子外飞出去的话，就算作出界。"),
                f.t("苏梦忆", "可如果球速非常快的话，会分不清的吧？"),
                f.t("李云萧", "好像……还真是这样，那样的话还是挂个网吧。")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetDetectJudgeNode("detest3");
        }

    }
}
