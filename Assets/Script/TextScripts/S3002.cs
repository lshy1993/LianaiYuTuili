using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class S3002 : TextScript
    {
        public S3002(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps):base(gVars, lVars, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——背景 商场——
                f.t("【李云萧】", "（时隔多日，又来到了这个伤心地）"),
                f.t("【李云萧】", "（算了算了，今天就好好休息一下吧）"),
                f.t("【李云萧】", "（嗯？前面这是怎么了？）"),
                //——CG 欧阳趴在蛋糕店外玻璃上——
                f.t("【李云萧】", "哇……这样也太羞耻了吧……",() => pieces.Count),
                //——背景 消失——
            };
        }

        public override GameNode NextNode()
        {
            //return base.NextNode();
            Finish();
            //return nodeFactory.GetEduNode("");
            return nodeFactory.GetMapNode();
        }

    }
}
