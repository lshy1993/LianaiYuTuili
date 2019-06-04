using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class S13_02A : TextScript
    {
        public S13_02A(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧","（那堆桌上的资料……）"),
                f.t("李云萧","（我记得我右手边的桌上放着我整理好的资料。）"),
                f.t("李云萧","（从我的座位开始，右手边的座位都是预备给新生用的。）"),
                f.t("李云萧","（那么，为什么要在那个座位上放我分好类的资料呢？）"),
                f.t("李云萧","（我记得只有学生会的成员才人手一份叠在一起的资料。）"),
                f.t("李云萧","（这么说来，那个位置一定坐了特殊的人……）"),
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
