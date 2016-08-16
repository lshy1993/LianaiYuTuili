using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1201_5 : TextScript
    {
        public TZ1201_5(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps):base(gVars, lVars, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "你为什么这么肯定就是他？"),
                //——立绘 叶枫婷——
                f.t("【叶枫婷】", "因为他被我抓了个现行！"),
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "可能他只是单纯地拿起来看看而已……（怎么我也说这句话了。）"),
                f.t("【叶枫婷】", "是小偷都不会承认自己偷了的。"),
                f.t("【叶枫婷】", "唔……（没有证据可以反驳。）",() => pieces.Count)
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
