using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_Library : TextScript
    {
        public default_Library(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                //——背景：图书馆——
                f.OpenDialog(),
                f.t("【李云萧】", "学校的图书馆，来了就拿本书看吧。"),
                f.t("【李云萧】", "……"),
                f.t("【李云萧】", "原来如此……"),
                f.t("【李云萧】", "…………"),
                f.t("【李云萧】", "都这个时间了吗？该回去了。"),
                f.t("【李云萧】", "什么事也没发生。（但似乎某些成绩得到了提升）")
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
