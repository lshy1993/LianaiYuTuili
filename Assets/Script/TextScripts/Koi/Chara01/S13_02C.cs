using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class S13_02C : TextScript
    {
        public S13_02C(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧","（座位的分隔方式……）"),
                f.t("李云萧","（如果以我的座位作为分界线的话，我的左手边应该都是学生会使用的……）"),
                f.t("李云萧","（从我的座位开始，我右手边也应该是前来听说明会的新生用的。）"),
                f.t("李云萧","（但是为社么要这么做呢，为什么有2个新生的座位在桌子的另一侧。）"),
                f.t("李云萧","（等一下，2个座位……）"),
                f.t("李云萧","喵星人，难道说，刚才那个女生是学生会的吗?")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.FindTextScript("S13_03");
        }

    }
}
