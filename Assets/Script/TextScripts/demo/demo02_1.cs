using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo02_1 : TextScript
    {
        public demo02_1(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*调查->办公桌
                f.OpenDialog(0),
                f.t("李云萧", "老师们的办公桌，上面堆满了各种文件。")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetDetectJudgeNode("detest2");
        }

    }
}
