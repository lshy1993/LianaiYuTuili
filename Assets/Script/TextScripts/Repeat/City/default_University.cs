using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_University : TextScript
    {
        public default_University(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                //——背景：大学外景——
                f.OpenDialog(),
                f.t("【李云萧】", "夕大，可以看到来来往往的学生，赶着去上课。"),
                f.t("【李云萧】", "要不以后也来这里读书吧？"),
                f.t("【李云萧】", "先参观一下！"),
                f.t("【李云萧】", "……"),
                f.t("【李云萧】", "…………"),
                f.t("【李云萧】", "校园好大了，光是走一圈就花了几个小时。"),
                f.t("【李云萧】", "真可惜，什么事也没发生，回家吧。"),
                f.t("【李云萧】", "一天就这样过去了……"),
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
