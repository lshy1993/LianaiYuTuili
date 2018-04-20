using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_LakePark : TextScript
    {
        public default_LakePark(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                //——背景：前湖公园——
                f.ChangeBackground("lake_day"),
                f.OpenDialog(),
                f.t("李云萧", "感觉一个人来，少了一些什么。"),
                f.t("李云萧", "今天也难得的出来休息一下吧……"),
                f.t("李云萧", "……"),
                f.t("李云萧", "…………"),
                f.t("李云萧", "真可惜，什么事也没发生。"),
                f.t("李云萧", "既然这样，还是离开这里吧."),
                f.t("李云萧", "一天就这样过去了……"),
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
