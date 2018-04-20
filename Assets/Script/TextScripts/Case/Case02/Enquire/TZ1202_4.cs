using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1202_4 : TextScript
    {
        public TZ1202_4(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "绕过来？"),
                //——立绘 戚海——
                f.t("【戚海】", "总不可能飞过来吧，要通过走廊过来。"),
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "那么，你花了多久时间呢？"),
                f.t("【戚海】", "因为我是跑过来的，也就半分钟不到吧。"),
                //——立绘 戚海——
                f.t("【李云萧】", "（的确，跑着过去十几秒就能到……）"),
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
