using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1202_5 : TextScript
    {
        public TZ1202_5(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "你碰到了叶亭风？"),
                //——立绘 戚海——
                f.t("【戚海】", "没错，我走到前面一个教室的时候就看到她了。"),
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "那个时候，她在干什么呢？"),
                f.t("【戚海】", "当时就看到他掏出钥匙准备开办公室的门。"),
                f.t("【李云萧】", "办公室那时候是锁着的吗？"),
                f.t("【戚海】", "我想是的，不然她也不会用钥匙去开。"),
                //——立绘 戚海——
                f.t("【李云萧】", "嗯……",() => pieces.Count),
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
