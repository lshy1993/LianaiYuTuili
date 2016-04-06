using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1102_r : TextScript
    {
        public TZ1102_r(Hashtable gVars, GameObject root, PanelSwitch ps):base(gVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            pieces = new List<Piece>()
            {
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【苏梦忆】", "李云萧，怎么办？"),
                f.t("【李云萧】", "该怎么办，苏梦忆？"),
                f.t("【苏梦忆】", "你别问我呀……"),
                f.t("【苏梦忆】", "总之，要证明他并没有看到那一幕。"),
                f.t("【李云萧】", "（刚才的证词里，一定存在着矛盾……）"),
                f.t("【李云萧】", "（边看调查记录，边仔细查看证词。）",() => pieces.Count)
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
