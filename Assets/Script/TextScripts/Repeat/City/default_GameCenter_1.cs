using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_GameCenter_1 : TextScript
    {
        public default_GameCenter_1(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.t("【李云萧】", "上吧，我的双手！"),
                f.t("【李云萧】", "……"),
                f.t("【李云萧】", "…………"),
                //——背景：电玩 傍晚——
                f.t("【李云萧】", "好累啊，打电玩也是需要体力的。"),
                f.t("【李云萧】", "今天到这里吧。"),
                f.t("【李云萧】", "（感觉到自己的宅力又提升了。）")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            //TODO:随机增加宅
            return nodeFactory.GetEndTurnNode();
        }

    }
}
