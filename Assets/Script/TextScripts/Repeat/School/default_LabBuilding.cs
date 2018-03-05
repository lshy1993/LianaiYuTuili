using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_LabBuilding : TextScript
    {
        public default_LabBuilding(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                //——背景：实验楼——
                f.OpenDialog(),
                f.t("【李云萧】", "实验楼，这里的每一间教室都是用于实验教学。"),
                f.t("【李云萧】", "不过，这里的仪器都是从哪里来的呢？"),
                f.t("【李云萧】", "……"),
                f.t("【李云萧】", "…………"),
                f.t("【李云萧】", "赶紧跑路吧。"),
                f.t("【李云萧】", "什么事情也没有发生……"),
                f.FadeoutAll()
                //——背景 消失——
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEndTurnNode();
        }

    }
}
