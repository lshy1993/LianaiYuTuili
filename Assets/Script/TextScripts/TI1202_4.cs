using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TI1202_4 : TextScript
    {
        public TI1202_4(Hashtable gVars, GameObject root, PanelSwitch ps):base(gVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            pieces = new List<Piece>()
            {
                //*调查->田径场
                f.t("【李云萧】", "足球队员们休息用的长椅，上面好像有份东西……"),
                f.t("【李云萧】", "“足球积分表”？"),
                f.t("【李云萧】", "上面写有红蓝队队员的名字，还有一些看不懂的数字。"),
                f.t("【李云萧】", "大概是有人在统计每个队员的成绩吧。",() => pieces.Count),
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
