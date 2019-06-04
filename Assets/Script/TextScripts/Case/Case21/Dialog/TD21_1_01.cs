using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TD21_1_01 : TextScript
    {
        public TD21_1_01(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //——关于被盗事件——
                f.OpenDialog(),
                f.t("李云萧","首先，详细说说这次事件吧。"),
                f.t("蔡助理","好的，事情是在下午的彩排结束后发生的。"),
                f.t("蔡助理","彩排结束后，我让她回休息室，之后我中途有事，离开了后台。"),
                f.t("蔡助理","等我回到后台的时候，就去她的休息室找她。"),
                f.t("蔡助理","那个时候，西门吹的东西就已经不见了。"),
                f.t("李云萧","你有亲眼看到吗？"),
                f.t("蔡助理","没有，是她自己发现东西不见了。"),
                f.t("李云萧","之后你有在附近找过吗？"),
                f.t("蔡助理","当然，我立刻让所有的工作人员帮忙寻找。"),
                f.t("蔡助理","但是很遗憾，没有人汇报发现了发卡一样的东西。"),
                f.t("李云萧","你找了哪些地方？"),
                f.t("蔡助理","主要就是后台休息时，服装室，还有舞台。"),
                f.t("李云萧","说起工作人员，有必要再详细了解一下。"),
                f.FadeoutAll()
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetDetectJudgeNode("T21_01");
        }

    }
}
