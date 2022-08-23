using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class T13_004 : TextScript
    {
        public T13_004(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //——背景 走廊——
                f.t("李云萧", "唔，之前遇到的是副社长还在吗？"),
                f.t("苏梦忆", "好像已经离开了。"),
                //立绘一闪
                f.t("苏梦忆", "啊！"),
                f.t("李云萧", "等一下！"),
                //立绘显示
                f.t("陈雨涵", "怎么了？"),
                f.t("苏梦忆", "那个，我们有些事情想要向你了解一下。"),
                f.t("陈雨涵", "社长她已经跟你们说过了？"),
                f.t("苏梦忆", "嗯，已经告诉我们详情，不过……"),
                f.t("陈雨涵", "不过什么？"),
                f.t("李云萧", "据说，你是这次事件的第一发现人。"),
                f.t("李云萧", "所以，向你详细了解下今天早上的事情。"),
                f.t("陈雨涵", "原来是这样啊，好吧。"),
                /*
                这里跳转调查
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
