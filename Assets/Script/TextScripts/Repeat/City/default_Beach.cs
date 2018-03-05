using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_Beach : TextScript
    {
        public default_Beach(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //——背景：沙滩白天——
                f.OpenDialog(),
                f.t("【李云萧】", "沙滩，大海，扶风！"),
                f.t("【李云萧】", "——都是人啊！"),
                f.t("【李云萧】", "来都来了，就久违地享受一下吧！"),
                f.t("【李云萧】", "……"),
                f.t("【李云萧】", "…………"),
                //——背景：沙滩傍晚——
                f.t("【李云萧】", "真可惜，什么事也没发生。"),
                f.t("【李云萧】", "也差不多该回家了。"),
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
