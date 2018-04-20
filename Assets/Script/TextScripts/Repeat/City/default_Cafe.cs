using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_Cafe : TextScript
    {
        public default_Cafe(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //——背景：咖啡馆白天——
                f.ChangeBackground("cafe_day"),
                f.OpenDialog(),
                f.t("店员", "欢迎光临！"),
                f.t("李云萧", "不知道为什么，很喜欢这家咖啡馆。"),
                f.t("李云萧", "今天也难得的出来休息一下吧……"),
                f.t("李云萧", "你好，请给我来……"),
                //——背景：变蓝天——
                f.t("李云萧", "……"),
                f.t("李云萧", "…………"),
                //——背景：咖啡馆傍晚——
                f.t("李云萧", "真可惜，什么事也没发生。"),
                f.t("李云萧", "既然这样，还是离开这里吧."),
                f.t("李云萧", "一天就这样过去了……"),
                //——背景 消失——
                f.FadeoutAll()
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEndTurnNode();
        }

    }
}
