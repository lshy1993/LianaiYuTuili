using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1202_3 : TextScript
    {
        public TZ1202_3(Hashtable gVars, GameObject root, PanelSwitch ps):base(gVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            pieces = new List<Piece>()
            {
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "窗外的足球？"),
                //——立绘 戚海——
                f.t("【戚海】", "大概是操场上的人在踢足球，然后不小心撞到窗户了。"),
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "（这里可是4楼啊，这得多大的力道。）"),
                f.t("【李云萧】", "那么之后呢？"),
                //——立绘 戚海——
                f.t("【戚海】", "之后……",() => pieces.Count),
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
