using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo06_2 : TextScript
    {
        public demo06_2(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "奇怪的声音？"),
                f.t("叶婷", "我想应该是玻璃被打碎的声音吧，很清脆的声响。"),
                f.t("叶婷", "而且你看，办公室的窗户不是破了吗？"),
                f.t("李云萧", "不过，你又是怎么知道具体时间是11点50分的？"),
                f.t("叶婷", "我开门的时候，正好看了眼里面的挂钟。"),
                f.t("李云萧", "挂钟？"),
                f.t("叶婷", "就挂在办公室外窗那面墙上的。"),
                f.t("李云萧", "[66ccff]（的确，进门之后可以看到挂钟）[-]"),
                f.t("李云萧", "那么之后？")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ01");
        }

    }
}
