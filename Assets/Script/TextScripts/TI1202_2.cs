using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TI1202_2 : TextScript
    {
        public TI1202_2(Hashtable gVars, GameObject root, PanelSwitch ps):base(gVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            pieces = new List<Piece>()
            {
                //*调查->球门
                f.t("【李云萧】", "已经开始掉漆的球门，虽然没有挂网。"),
                f.t("【苏梦忆】", "没有球网的话，怎么知道有没有进球？"),
                f.t("【李云萧】", "这简单，球从两个柱子间飞进去，就算进了。"),
                f.t("【李云萧】", "从外面飞出去的话，就算作出界。"),
                f.t("【苏梦忆】", "可是如果球速很快，会分不清的吧？"),
                f.t("【李云萧】", "好像……还真是这样，那还是挂个网好了。",() => pieces.Count)
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
