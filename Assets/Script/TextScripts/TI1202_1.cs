using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TI1202_1 : TextScript
    {
        public TI1202_1(Hashtable gVars, GameObject root, PanelSwitch ps):base(gVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            pieces = new List<Piece>()
            {
                //*调查->足球草地
                f.t("【李云萧】", "这是真的草地么？"),
                f.t("【苏梦忆】", "当然是真的！不是人造的假草。"),
                f.t("【李云萧】", "估计一到秋天就会枯了吧。"),
                f.t("【苏梦忆】", "你没听说过“春风吹又生”吗？"),
                f.t("【李云萧】", "我当然听过……",() => pieces.Count)
                /*
                这里要跳回【现场调查】
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
