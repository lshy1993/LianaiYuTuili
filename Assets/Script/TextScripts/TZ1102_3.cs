using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1102_3 : TextScript
    {
        public TZ1102_3(Hashtable gVars, GameObject root, PanelSwitch ps):base(gVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            pieces = new List<Piece>()
            {
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "不过你是怎么知道是玻璃破碎的声音呢？"),
                //——背景 证人台侧——
                //——立绘 叶枫婷——
                f.t("【叶枫婷】", "因为学校里经常有窗户被打碎的事情发生，我听得多了就知道。"),
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "那声音也有可能是其他东西的，比如镜子之类的？"),
                //——背景 证人台侧——
                //——立绘 叶枫婷——
                f.t("【叶枫婷】", "镜子也是玻璃做的，再说，当时我也想知道究竟是什么碎了，所以之后……",() => pieces.Count)
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
