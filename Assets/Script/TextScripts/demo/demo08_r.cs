using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo08_r : TextScript
    {
        public demo08_r(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "[66ccff]（证词到这里结束了……）[-]"),
                f.t("苏梦忆", "这下完了，两个人都看到喵星人拿了试卷……"),
                f.t("李云萧", "别急，他的话里有些让我在意的地方。"),
                f.t("苏梦忆", "对不起，我、我没有看出来。"),
                f.t("李云萧", "不过，这意味着什么呢？")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ02");
        }

    }
}
