using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_ConcertHall : TextScript
    {
        public default_ConcertHall(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                //——背景：音乐厅——
                f.OpenDialog(),
                f.t("【李云萧】", "来到了音乐厅……"),
                f.t("【李云萧】", "今天没有演出，连排练也没有。"),
                f.t("【李云萧】", "……"),
                f.t("【李云萧】", "…………"),
                f.t("【李云萧】", "空旷的音乐厅还是有点冷啊……"),
                f.t("【李云萧】", "不行了，冷的发抖，赶紧离开这里。"),
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
