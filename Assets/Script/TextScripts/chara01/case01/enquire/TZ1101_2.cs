using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1101_2 : TextScript
    {
        public TZ1101_2(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.OpenDialog(0),
                f.t("【李云萧】", "又或者，有没有可能老师忘了锁门呢？"),
                //——背景 证人台侧——
                //——立绘 张傲——
                f.t("【张傲】", "最后离开的几位老师，可以证明门是锁上的。"),
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "是、是么……"),
                /*
                这里要跳转【继续询问】
                */
            };
        }

        public override GameNode NextNode()
        {
            //return base.NextNode();
            Finish();
            return nodeFactory.GetEnquireNode("Z1101");
            //return nodeFactory.GetMapNode();
        }

    }
}
