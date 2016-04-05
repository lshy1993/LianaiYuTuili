using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1103_2 : TextScript
    {
        public TZ1103_2(Hashtable gVars, GameObject root, PanelSwitch ps):base(gVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            pieces = new List<Piece>()
            {
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "挂钟？是什么样子的？"),
                //——背景 证人台侧——
                //——立绘 叶枫婷——
                f.t("【叶枫婷】", "就是普通的用电池的机械钟，只不过是挂在墙上的。"),
                f.t("【叶枫婷】", "那种挂钟，学校里每个办公室都有一个。"),
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "唔……",() => pieces.Count)
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
