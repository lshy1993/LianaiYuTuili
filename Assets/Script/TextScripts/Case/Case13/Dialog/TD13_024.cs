using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TD13_024 : TextScript
    {
        public TD13_024(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*高练和暮馨
                f.t("李云萧", "刚才你说的高练和暮馨，能再详细介绍一下吗？"),
                f.t("陈雨涵", "高练是我们活动部的部长，活动部就是专门策划各种社团活动的部门。"),
                f.t("陈雨涵", "这次的展出活动就是高练提出来的。"),
                f.t("李云萧", "那么，他现在人在哪里呢？"),
                f.t("陈雨涵", "他的话，现在这个点估计在隔壁社团串门呢，就是文学社。"),
                f.t("李云萧", "那么还有一位，暮馨她是？"),
                f.t("陈雨涵", "她是一般的社员，不过她不是活动部的的。"),
                f.t("陈雨涵", "暮馨的工作一般是教社员们练习书法。"),
                f.t("李云萧", "是吗，那么我在哪里可以见到她呢？"),
                f.t("陈雨涵", "唔……这我就不知道了，因为她行踪不定。"),
                f.t("李云萧", "行踪不定？这……"),
                f.t("陈雨涵", "不过，有一个人很了解她，也许会知道她在哪里。"),
                f.t("李云萧", "是谁？"),
                f.t("苏梦忆", "不会是高练吧？"),
                f.t("陈雨涵", "没错，悄悄告诉你，他们已经发展到那一步了。"),
                f.t("苏梦忆", "诶，真的假的？"),
                f.t("陈雨涵", "真的，平时社团集结的时候，他们两个一直是一起的。"),
                f.t("陈雨涵", "还有啊……（悄悄话）"),
                f.t("苏梦忆", "……（悄悄话）"),
                f.t("李云萧", "（完全听不到她们两个在讲什么……）"),
                f.t("陈雨涵", "总之，可千万别说是我告诉你的。"),
                f.t("苏梦忆", "好的。"),
                f.t("李云萧", "苏梦忆，刚才你们两个说了什么？"),
                f.t("苏梦忆", "秘密。"),
                f.t("李云萧", "……"),
                f.t("李云萧", "算了，先去文学社找高练吧。")
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
