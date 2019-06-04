using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TD13_023 : TextScript
    {
        public TD13_023(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*关于书法
                f.t("李云萧", "那副消失的书法，你见过吗？"),
                f.t("陈雨涵", "唔，怎么说呢，见过又没见过。"),
                f.t("苏梦忆", "什么意思？"),
                f.t("陈雨涵", "见过是我看到过社长拿着装书法的盒子。"),
                f.t("李云萧", "盒子？书法是装在盒子里的吗？"),
                f.t("陈雨涵", "嗯，一个镶金的红色锦盒，我昨天中午见社长拿着它。"),
                f.t("陈雨涵", "但那书法卷轴展开后是什么样子，我就不知道了。"),
                f.t("陈雨涵", "大概是想在今天展出的时候再挂出来吧。"),
                f.t("李云萧", "书法卷轴？你是怎么知道的？"),
                f.t("陈雨涵", "社长跟我说的，她说过“书法卷起来后，再放进盒子里”。"),
                f.t("苏梦忆", "那个盒子的大小呢？"),
                f.t("陈雨涵", "嗯……大小么……我没有近距离量过……"),
                f.t("陈雨涵", "…………啊，我记起来了！"),
                f.t("陈雨涵", "我看到过社长放进保险桌的情形，和文具盒差不多大小。"),
                //证据-装书法的盒子（约是一个文具盒的大小，镶金红色锦盒）
                f.t("李云萧", "你确定吗？"),
                f.t("陈雨涵", "当时社长放进保险桌的时候，右半边的空间还剩一半呢。"),
                f.t("苏梦忆", "这个盒子的事情，怎么没听社长说起过？"),
                f.t("陈雨涵", "大概是社长她忘记说了吧，毕竟太细碎的。"),
                f.t("李云萧", "（是吗……仅仅是太细碎了？）")
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
