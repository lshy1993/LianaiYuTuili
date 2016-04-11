using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class T12008 : TextScript
    {
        public T12008(Hashtable gVars, GameObject root, PanelSwitch ps):base(gVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            pieces = new List<Piece>()
            {
                //——背景 操场——
                f.t("【李云萧】", "（资料调查得差不多了……）"),
                f.t("【苏梦忆】", "怎么样，有结果了吗？"),
                f.t("【李云萧】", "嗯，我好像有点头绪了……"),
                f.t("【李云萧】", "（首先，整理下至今发生的事情吧……）",() => pieces.Count)
                /*
                这里跳转【自我推理】
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
