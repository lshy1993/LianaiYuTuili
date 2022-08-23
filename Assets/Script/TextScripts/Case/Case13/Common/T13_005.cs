using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class T13_005 : TextScript
    {
        public T13_005(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //——背景 文学社——
                f.t("李云萧", "这里就是文学社了……"),
                f.t("苏梦忆", "高练他在不在这里呢？"),
                f.t("李云萧", "那个男生是不是？"),
                //立绘显示
                f.t("？？？", "…………"),
                f.t("苏梦忆", "那个，请问你是高练吗？"),
                f.t("高练", "我就是，请问你是？"),
                f.t("苏梦忆", "我是推理社的社长，我叫苏梦忆。"),
                f.t("高练", "推理社？找我有什么事情吗？"),
                f.t("李云萧", "书法社丢失了一副书法，你知道吗？"),
                f.t("高练", "你怎么会知道的？这件事明明只有我们知道的。"),
                f.t("李云萧", "我们是受了柳萱的委托，来调查此次事件的。"),
                f.t("高练", "社长委托了你们？不会吧……"),
                f.t("李云萧", "总之，我们想详细了解下情况可以吧。"),
                f.t("高练", "……好吧，你问吧。")
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
