using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo14_3 : TextScript
    {
        public demo14_3(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*调查->办公桌
                f.OpenDialog(0),
                f.t("李云萧", "叶婷目击到的情形有问题！是假的！"),
                f.t("李云萧", "不对、不对！"),
                f.HPChange(-1),
                f.t("李云萧", "戚海超同样看到了喵星人，她的说法没有问题。"),
                f.t("李云萧", "再重新考虑下……")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetReasoningNode("Qdemo01");
        }

    }
}
