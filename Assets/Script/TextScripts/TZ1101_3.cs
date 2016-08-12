using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1101_3 : TextScript
    {
        public TZ1101_3(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps):base(gVars, lVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            pieces = new List<Piece>()
            {
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "可是窗玻璃为什么碎了？"),
                f.t("【李云萧】", "这难道不是有人尝试从外窗进入的证据吗？"),
                //——背景 证人台侧——
                //——立绘 张傲——
                f.t("【张傲】", "啊！这是因为……"),
                //——背景 法庭-检察方侧——
                //——立绘 检察官——
                f.t("【沈尘业】", "中午在办公室外发生了点意外，碰碎了窗户，但那与本事件无关。"),
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "是么……"),
                //——背景 证人台侧——
                //——立绘 张傲——
                f.t("【张傲】", "可以肯定的是，并没有人从碎掉的窗户这里进入。"),
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "……",() => pieces.Count),
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
