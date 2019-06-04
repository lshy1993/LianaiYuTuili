using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class S21_02A : TextScript
    {
        public S21_02A(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //dic.Add("算了", "S2000_A");
                f.OpenDialog(),
                f.t("李云萧", "不好意思，没什么兴趣……"),
                f.t("喵星人","你确定？不后悔？"),
                f.t("李云萧","我对偶像之类的没什么兴趣呀。"),
                f.t("喵星人","那好吧，拿这个照片我就自己收下了。"),
                f.t("李云萧","嗯，不说这个了。"),
                f.t("喵星人","我的吹吹！可惜了这个木头不能见到你了！"),
                f.t("李云萧","你在自言自语什么呢？"),
                f.t("喵星人","没没事。"),
                f.t("李云萧","奇怪的人……"),
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
