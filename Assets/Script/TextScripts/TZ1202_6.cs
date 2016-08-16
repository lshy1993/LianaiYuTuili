using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1202_6 : TextScript
    {
        public TZ1202_6(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps):base(gVars, lVars, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "你确定没有看错？"),
                //——立绘 戚海——
                f.t("【戚海】", "应该没有，他那时候就拿着打开的密封袋。"),
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "唔……（喵星人啊，你干嘛要去拿那个袋子啊……）"),
                //——立绘 戚海——
                f.t("【戚海】", "所以，叶枫婷说的没有错。",() => pieces.Count),
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
