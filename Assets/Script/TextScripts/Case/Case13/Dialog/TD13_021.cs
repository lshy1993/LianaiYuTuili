using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TD13_021 : TextScript
    {
        public TD13_021(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*关于目击
                f.t("李云萧", "先说一下今早发生的事情吧。"),
                f.t("陈雨涵", "今天早上，我提前来到活动教室。"),
                f.t("苏梦忆", "为什么要提早？"),
                f.t("陈雨涵", "因为想到接下来会有活动，就想先把教室弄干净点。"),
                f.t("陈雨涵", "然后我隔着走廊的窗户看到，“保险桌”的桌板被打开了。"),
                f.t("陈雨涵", "我仔细看了一下，发现保险箱也被打开了，里面空了一半。"),
                f.t("陈雨涵", "于是，我就去教室找柳萱，和她一起开教室的门。"),
                f.t("李云萧", "嗯？我记得你也有活动教室的钥匙得吧，为什么自己不开呢？"),
                f.t("陈雨涵", "因为我忘记带了，原本我是打算先开门的……"),
                f.t("李云萧", "这样，当时你进入教室感觉有什么奇怪的地方吗？"),
                f.t("陈雨涵", "没有吧，没发现什么……"),
                f.t("陈雨涵", "因为昨晚我不是最后一个走的，所以我也不清楚。"),
                f.t("陈雨涵", "之后社长检查后发现那副书法不见了。"),
                f.t("李云萧", "之后，你们又做了什么？"),
                f.t("陈雨涵", "我让社长赶紧保护了现场，想着联系老师过来。"),
                f.t("陈雨涵", "但是社长拒绝了，她写了封信，叫我送到推理社的信箱里。"),
                f.t("李云萧", "（原来是你投的啊……）"),
                //*证据：发现者的顺序（案发时，陈雨涵发现的，但是他没有带钥匙。）
                f.t("李云萧", "那么这件事，只有你和社长知道吗？"),
                f.t("陈雨涵", "不，之后又来了两位社员，他们也知道了。"),
                f.t("李云萧", "那两位是？"),
                f.t("陈雨涵", "一个是活动部的部长高练，还有是暮馨。"),
                f.t("陈雨涵", "你可以去问问他们俩人。"),
                f.t("李云萧", "再仔细了解下这两人……")
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
