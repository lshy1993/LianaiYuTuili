using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TD13_032 : TextScript
    {
        public TD13_032(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*关于事件
                f.t("李云萧", "关于这次事件，你知道多少？"),
                f.t("高练", "书法社丢了一副书法，仅此而已。"),
                f.t("李云萧", "你知道丢失的书法是有名的书法家写的吗？"),
                f.t("高练", "什么？还有这回事？我怎么没听说过？"),
                f.t("李云萧", "你不知道？"),
                f.t("高练", "社长只告诉了我们，说她丢了一副普通的书法。"),
                f.t("高练", "我提出的方案，只是让每一位社员上交自己的作品展出。"),
                f.t("高练", "可是社长她怎么就拿了副书法名家的作品过来展出。"),
                f.t("李云萧", "（也就是说高练不知道有这幅特殊书法之事。）"),
                f.t("高练", "这要是弄丢了，该如何是好啊？"),
                f.t("李云萧", "有一个不幸的消息，丢失的那副作品就是。"),
                f.t("高练", "我想也是的……"),
                //证据-超出计划的部分
                f.t("李云萧", "那么，你有见过那副书法家的作品吗？"),
                f.t("高练", "没有，我是今天才知道的，而且昨天我没有来过社团教室。"),
                f.t("李云萧", "问句题外话，你对这次的事件有什么看法吗？"),
                f.t("高练", "看法么，大概是书法社里的哪个人拿走了。"),
                f.t("李云萧", "为什么这么认为呢？"),
                f.t("高练", "如果是一般的小偷，目标是值钱的东西，不会选书法的。"),
                f.t("高练", "社员的作品并不是值钱的名作，而且一般人也区分不出来。"),
                f.t("高练", "如果一开始就为了那副书法，他又是如何得知，作品放在了书法社的呢？"),
                f.t("高练", "但是社长也太粗心了，怎么不把东西带回去呢……"),
                f.t("李云萧", "不错的判断。"),
                f.t("李云萧", "对了，那么你对陈雨涵她？"),
                f.t("高练", "她的话……我不是很了解……"),
                f.t("高练", "毕竟她是副社长，年级上差了一级，平时也不是走得很近。"),
                f.t("李云萧", "什么方面都可以，有什么奇怪的地方吗？"),
                f.t("高练", "要说有什么奇怪的的，为什么她会是第一个到教室的？"),
                f.t("李云萧", "（陈雨涵提早这件事，他也不知道……）"),
                f.t("高练", "平时都是暮馨第一个到的……"),
                f.t("李云萧", "嗯？第一个？再详细了解下……")
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
