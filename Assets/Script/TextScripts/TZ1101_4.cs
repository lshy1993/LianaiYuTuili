using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1101_4 : TextScript
    {
        public TZ1101_4(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps):base(gVars, lVars, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "办公室的钥匙除了老师手中的，只有一把吗？"),
                //——背景 证人台侧——
                //——立绘 张傲——
                f.t("【张傲】", "学校的保卫室有一把，加上被告人拿着的，总共两把备用钥匙。"),
                f.t("【张傲】", "但是，保卫室里的备用钥匙并没有被借出。"),
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "没有被借出么……"),
                //——背景 证人台侧——
                //——立绘 张傲——
                f.t("【张傲】", "所以……",() => pieces.Count)
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
