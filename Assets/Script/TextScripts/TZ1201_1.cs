using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1201_1 : TextScript
    {
        public TZ1201_1(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps):base(gVars, lVars, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "你的教室在？"),
                //——立绘 叶枫婷——
                f.t("【叶枫婷】", "距离办公室最近的一间，出门左拐就是办公室。"),
                //——立绘 叶枫婷——
                f.t("【叶枫婷】", "这样近的话有个好处，我们就能随时注意办公的动静了。"),
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "（这种没什么用的好处……）",() => pieces.Count)
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
