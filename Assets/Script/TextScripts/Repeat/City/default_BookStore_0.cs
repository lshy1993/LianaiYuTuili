using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_BookStore_0 : TextScript
    {
        public default_BookStore_0(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.t("【李云萧】", "好，就是你了！（随手抓了一本书）"),
                f.t("【李云萧】", "……"),
                f.t("【李云萧】", "…………"),
                f.t("【李云萧】", "原来如此……"),
                f.t("【李云萧】", "……"),
                f.t("【李云萧】", "…………"),
                //——背景，傍晚的书店——
                f.t("【李云萧】", "不好，一不小心看入迷了……"),
                f.t("【李云萧】", "赶紧把书放回去，回家吧。"),
                f.t("【李云萧】", "（感觉某一项成绩提升了！）"),
                f.FadeoutAll()
                //——背景 消失——
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            //TODO：属性的随机少量增加
            return nodeFactory.GetEndTurnNode();
        }

    }
}
