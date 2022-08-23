using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TD13_031 : TextScript
    {
        public TD13_031(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*关于自己
                f.t("李云萧", "你是在书法社做什么的呢？"),
                f.t("高练", "我是活动部的部长，负责社团活动的安排。"),
                f.t("高练", "告诉你，这回的书法展出活动，也是我策划的。"),
                f.t("高练", "一周前是我提出了方案，没想到今天就能举行了。"),
                f.t("李云萧", "（可还是出了点事故……）"),
                f.t("高练", "不过好可惜，丢了一副作品，社长的心情一定不太好吧。"),
                f.t("高练", "本来我还想了另外几个活动方案呢……"),
                f.t("李云萧", "你还真是认真啊……"),
                f.t("高练", "那是自然，因为我是活动部的部长。"),
                f.t("李云萧", "（这家伙的责任心也太强了……）"),
                f.t("高练", "如果这次活动能成功的话，我就能证明给暮馨看了……"),
                f.t("李云萧", "说起来，暮馨和你是什么关系？"),
                f.t("高练", "关、关、关系？当然就是普通的同学关系……"),
                f.t("李云萧", "（明显就不是普通的关系……）"),
                f.t("李云萧", "我听说，你和暮馨的关系不一般。"),
                f.t("高练", "胡说！你听说谁说的？我和她才没有……"),
                f.t("李云萧", "好吧，只是听说的，你也别当真。"),
                f.t("李云萧", "不过，我们要找她了解一些事，她的行踪你清楚。"),
                f.t("高练", "嗯……现在的话，我也不知道。"),
                f.t("李云萧", "……（靠不靠谱啊……）"),
                f.t("高练", "不过再过一会的话，她就会来书法社的，我保证。"),
                f.t("李云萧", "……"),
                f.t("高练", "我保证！"),
                f.t("李云萧", "（算了，现在也只有相信他的话了。）")
                /*
                这里跳转对话
                */
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.FindTextScript("T11002");
        }

    }
}
