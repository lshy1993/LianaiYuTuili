using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo13 : TextScript
    {
        public demo13(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(0),
                f.t("李云萧", "资料调查得差不多了。"),
                f.t("苏梦忆", "有结果了吗？"),
                f.t("李云萧", "嗯，好像有点头绪了……"),
                f.t("李云萧", "[66ccff]（先整理下至今发生的事情吧……）[-]"),
                f.t("李云萧", "[66ccff]（根据叶婷的说法，上午11点50分前后听到声音。）[-]"),
                f.t("李云萧", "[66ccff]（这个声音是因为窗户被足球踢中打碎了。）[-]"),
                f.t("李云萧", "[66ccff]（之后她便用钥匙打开了门，发现喵星人在里面。）[-]"),
                f.t("李云萧", "[66ccff]（那么，这段回忆有哪里是矛盾的？）[-]")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetReasoningNode("Qdemo01", "NEW");
        }

    }
}
