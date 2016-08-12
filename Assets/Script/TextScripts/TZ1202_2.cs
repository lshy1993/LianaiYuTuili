using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1202_2 : TextScript
    {
        public TZ1202_2(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps):base(gVars, lVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            pieces = new List<Piece>()
            {
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "你看到窗户被打碎了？"),
                //——立绘 戚海——
                f.t("【戚海】", "是的。"),
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "知道是什么东西打碎的吗？"),
                //——立绘 戚海——
                f.t("【戚海】", "这个……是个足球。"),
                f.t("【李云萧】", "足球？"),
                f.t("【戚海】", "对，我没有看错。"),
                f.t("【李云萧】", "（看来这是个重要线索……）"),
                f.t("【李云萧】", "请你加入证词里吧。"),
                f.t("【戚海】", "好的……",() => pieces.Count),
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
