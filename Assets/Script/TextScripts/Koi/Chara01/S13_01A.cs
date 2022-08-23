using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class S13_01A : TextScript
    {
        public S13_01A(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "（是有段时间没有见到过她了，但是……）"),
                f.t("李云萧", "（我和她只有一面之缘，为什么要紧跟着呢。）"),
                f.t("李云萧", "你是学生会的，有分内之事要做，我就不去了。"),
                f.t("喵星人", "欸，真的不去吗？"),
                f.t("喵星人", "你不去的话，我也不去了。"),
                f.t("喵星人", "反正都是各个部长出席的说明会，我只要把东西交过去就可以了。"),
                f.t("李云萧", "给你，分成几堆了。"),
                f.t("喵星人", "多谢，我先去提交了。"),
                f.t("李云萧", "[66ccff]（目送着喵星人离开，我也应该去做自己的事情了。）[-]"),
                f.CloseDialog(),
                f.FadeoutAll()
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEndTurnNode();
        }

    }
}
