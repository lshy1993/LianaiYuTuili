using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo04 : TextScript
    {
        public demo04(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.FadeoutCharacterSprite(0,0),
                f.OpenDialog(),
                f.t("李云萧", "[66ccff]（能向喵星人询问的部分，已经差不多结束了……）[-]"),
                f.t("李云萧", "[66ccff]（接下来就是，这让我在意的现场了……）[-]"),
                f.t("", "[00ff00]终于可以进行【调查】了，调查就是寻找现场证据。[-]"),
                f.t("", "[00ff00]点击下方的调查按钮后，就可以看到现场可疑的部分。\n若要仔细查看，请点击该部分。[-]"),
                f.t("", "[00ff00]请快点找到关键的证据吧！[-]")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetDetectJudgeNode("detest2");
        }

    }
}
