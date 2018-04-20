using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1202_r : TextScript
    {
        public TZ1202_r(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "（证词到这里结束了……）"),
                //——立绘 戚海——
                f.t("【苏梦忆】", "这下完了，两个人都看到喵星人拿起试卷了。"),
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "的确很棘手，不过，刚才的话里有些让我在意的地方。"),

                f.t("【苏梦忆】", "对不起，我、我没有看出来。"),

                //——立绘 戚海——
                f.t("【李云萧】", "不过，这意味着什么呢？"),
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
