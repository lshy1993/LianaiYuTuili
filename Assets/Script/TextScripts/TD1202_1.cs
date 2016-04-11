using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TD1202_1 : TextScript
    {
        public TD1202_1(Hashtable gVars, GameObject root, PanelSwitch ps):base(gVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            pieces = new List<Piece>()
            {
                //*询问->关于足球比赛
                //——背景 办公室——
                f.t("【李云萧】", "你们是刚踢完一场比赛吗？"),
                f.t("【项茂】", "……"),
                f.t("【李云萧】", "……哎"),
                f.t("【苏梦忆】", "你们刚结束一场比赛吗？"),
                f.t("【项茂】", "是的，刚刚结束不久，我刚刚还进了一个球。"),
                f.t("【苏梦忆】", "能告诉我比赛的情况吗？比如，什么时候开始的？"),
                f.t("【项茂】", "没问题，比赛是足球社队内部的比赛。"),
                f.t("【项茂】", "比赛是从上午第四节的体育课开始的，一场进行90分钟。"),
                f.t("【项茂】", "这回除了提早开始上课，我们只能下课后继续比赛了。"),
                f.t("【李云萧】", "提早上课？"),
                f.t("【项茂】", "一节课，满打满算只有45分钟，根本不够踢。"),
                f.t("【项茂】", "所以，利用课间休息的几分钟，就能早点开始了。"),
                f.t("【李云萧】", "（这一点要再详细地问下。）"),
                f.t("【苏梦忆】", "还有吗？"),
                f.t("【项茂】", "嗯，我想想……有了！"),
                f.t("【项茂】", "比赛到一半的时候，发生了点意外。"),
                f.t("【李云萧】", "（意外？这个也得问问他。）",() => pieces.Count),
                /*
                这里要跳转【对话】
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
