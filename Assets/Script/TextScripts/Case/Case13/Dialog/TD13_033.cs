using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TD13_033 : TextScript
    {
        public TD13_033(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*关于“保险桌”
                f.t("李云萧", "你知道“保险桌”吗？"),
                f.t("高练", "你是说那个像普通课桌的保险箱吗？"),
                f.t("李云萧", "对，你知道？"),
                f.t("高练", "基本上成立时间较早的社团都有。"),
                f.t("李云萧", "你知道怎么打开吗？"),
                f.t("高练", "这个我还是知道的，要同时有钥匙和密码。"),
                f.t("李云萧", "哦？这件事只有社长和副社长知道，你是怎么知道的？"),
                f.t("高练", "本来我是不知道的，但是这个东西不止书法社有。"),
                f.t("高练", "成立时间较早的社团都有，所有……我就知道了。"),
                f.t("李云萧", "是这样啊……"),
                f.t("高练", "这要是弄丢了，该如何是好啊？"),
                f.t("李云萧", "有一个不幸的消息，丢失的那副作品就是。"),
                f.t("高练", "我想也是的……"),
                //证据-超出计划的部分
                f.t("李云萧", "那么，你有见过那副书法家的作品吗？"),
                f.t("高练", "没有，我是今天才知道的，而且昨天我没有来过社团教室。"),
                f.t("李云萧", "嗯，这样啊……")
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
