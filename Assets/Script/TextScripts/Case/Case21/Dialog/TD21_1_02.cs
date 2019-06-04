using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TD21_1_02 : TextScript
    {
        public TD21_1_02(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //——关于双人组合——"),
                f.OpenDialog(),
                f.t("李云萧","她们组成是叫“吹雪”的组合是吧？"),
                f.t("蔡助理","是的。"),
                f.t("李云萧","这个组合是什么性质的一个？"),
                f.t("蔡助理","最早，她们两个是分别在网上唱歌的，以翻唱为主。"),
                f.t("蔡助理","因为我见她们人气很高，所以就让他们成为我们旗下。"),
                f.t("李云萧","这么说，这两人并不是一开始就认识？"),
                f.t("蔡助理","是的，具体来说，是她们来我们这里培训的时候认识的。"),
                f.t("李云萧","这个“吹雪”二字，分别取自她们两个的名？"),
                f.t("蔡助理","是的，原本是想取些英文名的。"),
                f.t("蔡助理","巧的是，两人都是复姓单名，所以就组合了一下。"),
                f.t("李云萧","原来如此。"),
                f.t("蔡助理","没想到一两年下来，就已经这么出色了。"),
                f.t("李云萧","平时你的工作是？"),
                f.t("蔡助理","负责她们两位的演出工作。"),
                f.t("蔡助理","联系场地联系售票，然后接各种商业演出活动。"),
                f.t("蔡助理","除此以外，她们平时要接受大量的训练，文化课也不能落下。"),
                f.t("李云萧","[66ccff]（偶像也不轻松啊……）[-]"),
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
