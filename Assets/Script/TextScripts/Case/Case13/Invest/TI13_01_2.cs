using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TI13_01_2 : TextScript
    {
        public TI13_01_2(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*调查->窗帘
                f.t("李云萧", "深绿色的遮光窗帘，我们的教室有这种东西吗？"),
                f.t("苏梦忆", "有啊，只是颜色不同而已，效果上都一样的。"),
                f.t("李云萧", "这种窗帘，拉上后，整个教室都会变暗许多。"),
                f.t("李云萧", "现在窗帘是打开的状态，昨天没有用过吗？"),
                //证据-打开的窗帘
                f.t("李云萧", "估计没有什么用处吧……"),
                /*
                这里要跳回现场调查
                */
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.FindTextScript("T11002");
        }

    }
}
