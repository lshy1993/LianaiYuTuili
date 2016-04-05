using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1102_2 : TextScript
    {
        public TZ1102_2(Hashtable gVars, GameObject root, PanelSwitch ps):base(gVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            pieces = new List<Piece>()
            {
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "具体来说，是什么样的？"),
                //——背景 证人台侧——
                //——立绘 叶枫婷——
                f.t("【叶枫婷】", "就是“咣当——”这种声音。"),
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "就算你这么说……"),
                //——背景 证人台侧——
                //——立绘 叶枫婷——
                f.t("【叶枫婷】", "不过，我想那应该是……",() => pieces.Count)
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
