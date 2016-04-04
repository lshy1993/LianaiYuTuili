using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1102_4 : TextScript
    {
        public TZ1102_4(Hashtable gVars, GameObject root, PanelSwitch ps):base(gVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            pieces = new List<Piece>()
            {
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "你为什么要这样干？"),
                //——背景 证人台侧——
                //——立绘 叶婷枫——
                f.t("【叶婷枫】", "毕竟那声音特别响，我忍不住好奇，不是人之常情吗？"),
                //——背景 检察官侧——
                //——立绘 检察官——
                f.t("【沈尘业】", "嗯，的确是这样，那后来你又看到了什么呢？",() => pieces.Count),
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
