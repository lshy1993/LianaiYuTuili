using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_Museum : TextScript
    {
        public default_Museum(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                //——背景：博物馆——
                f.ChangeBackground("museum_day"),
                f.OpenDialog(),
                f.t("李云萧", "站在博物馆外面，就能感受到历史的厚重感。"),
                f.t("李云萧", "哦，正好在举行特展，可以看看。"),
                f.t("李云萧", "……"),
                f.t("李云萧", "…………"),
                f.t("李云萧", "不好，一不小心看入迷了……"),
                f.t("李云萧", "真可惜，什么事也没发生，回家吧。"),
                f.t("李云萧", "（感觉某些方面提升了！）"),
                //——背景 消失——
                f.FadeoutAll()
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            //TODO: 随机增加属性
            return nodeFactory.GetEndTurnNode();
        }

    }
}
