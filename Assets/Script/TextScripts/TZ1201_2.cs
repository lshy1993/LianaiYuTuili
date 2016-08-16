using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1201_2 : TextScript
    {
        public TZ1201_2(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps):base(gVars, lVars, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "奇怪的声音？"),
                //——立绘 叶枫婷——
                f.t("【叶枫婷】", "我想应该是玻璃被打碎的声音吧，很清脆的声响。"),
                //——立绘 叶枫婷——
                f.t("【叶枫婷】", "而且你看，办公室的窗户不是破了吗？所以我是听到的。"),
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "不过，你又是怎么知道是11点50分的？"),
                f.t("【叶枫婷】", "我开门的时候，正好看了眼里面的挂钟。"),
                f.t("【李云萧】", "挂钟？"),
                f.t("【叶枫婷】", "就挂在办公室外窗那面墙上的。"),
                f.t("【李云萧】", "（的确是有挂钟，这里没有问题……）"),
                f.t("【李云萧】", "那么之后？",() => pieces.Count)
                /*
                这里要跳转【继续询问】
                */
            };
        }

        public override GameNode NextNode()
        {
            //return base.NextNode();
            Finish();
            return nodeFactory.FindTextScript("T11002");
            //return nodeFactory.GetMapNode();
        }

    }
}
