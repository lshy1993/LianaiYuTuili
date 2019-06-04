using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class S13_02B : TextScript
    {
        public S13_02B(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧","（那个神秘的女生……）"),
                f.t("李云萧","（刚才，那个女生对喵星人说……）"),
                f.t("？？？","喵星人，这里交给你了，我还有事要先离开一下。"),
                f.t("喵星人","知道了！交给我吧！"),
                f.t("李云萧","（难道说！）"),
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
