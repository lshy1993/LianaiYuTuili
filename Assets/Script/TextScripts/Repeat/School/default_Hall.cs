using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_Hall : TextScript
    {
        public default_Hall(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                //——背景：大礼堂——
                f.OpenDialog(),
                f.t("【李云萧】", "这里就是学校的大礼堂了。"),
                f.t("【李云萧】", "想象一下在这里的毕业典礼……"),
                f.t("【李云萧】", ""),
                f.t("【李云萧】", "……"),
                f.t("【李云萧】", "…………"),
                f.t("【李云萧】", "不要发呆了，回去吧."),
                f.t("【李云萧】", "什么事也没发生……")
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
