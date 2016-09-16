using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1202_1 : TextScript
    {
        public TZ1202_1(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "教学楼的另一侧？"),
                //——立绘 戚海——
                f.t("【戚海】", "嗯，就是对面，我们学校的教学楼是凹字形的。"),
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "你在那一侧干什么？"),
                //——立绘 戚海——
                f.t("【戚海】", "刚刚运动完有点热，在那边休息。"),
                f.t("【李云萧】", "单纯的休息么……",() => pieces.Count),
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
