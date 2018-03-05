using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_AdminBuilding : TextScript
    {
        public default_AdminBuilding(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                //——背景：行政楼——
                f.OpenDialog(),
                f.t("【李云萧】", "哇，不管是哪个学校的行政楼都很恐怖。"),
                f.t("【李云萧】", "几乎是学生们最不愿意来的地方。"),
                f.t("【李云萧】", "前面那个……该不会是教导主任吧！"),
                f.t("【李云萧】", "赶紧跑！"),
                f.t("【李云萧】", "……"),
                f.t("【李云萧】", "…………"),
                f.t("【李云萧】", "应、应该……安、安全了吧？"),
                f.t("【李云萧】", "……"),
                f.t("【李云萧】", "太吓人了，还是赶紧回去吧。"),
                f.t("【李云萧】", "什么事也没发生……"),
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
