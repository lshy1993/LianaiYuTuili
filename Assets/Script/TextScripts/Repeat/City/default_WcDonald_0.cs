using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_WcDonald_0 : TextScript
    {
        public default_WcDonald_0(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                //——背景：WcDonald——
                f.OpenDialog(),
                f.t("【李云萧】", "不行，为了零用钱，我要忍住……"),
                f.t("【李云萧】", "……"),
                f.t("【李云萧】", "…………"),
                f.t("【李云萧】", "结果我也变成在这里做作业的人了……"),
                f.t("【李云萧】", "时间也不早了，趁着没有人赶紧离开这里吧。"),
                f.t("【李云萧】", "一天就这样过去了……")
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
