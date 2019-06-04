using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TD13_022 : TextScript
    {
        public TD13_022(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*关于保险桌
                f.t("李云萧", "关于“保险桌”你了解多少？"),
                f.t("陈雨涵", "基本的情况我都知道。"),
                f.t("苏梦忆", "那解锁密码？"),
                f.t("陈雨涵", "我知道，因为曾经社长让我帮她开过一次，她把密码告诉我了。"),
                f.t("李云萧", "那么自然钥匙的事情你也知道？"),
                f.t("陈雨涵", "嗯，钥匙就挂在课桌侧边，不过一般人发现不了。"),
                f.t("陈雨涵", "你别这么看我，虽然我知道密码，但是我没有拿啊。"),
                f.t("陈雨涵", "而且，今天，我是和社长一起开教室的门。"),
                f.t("李云萧", "关于怎么打开那个保险桌，我还想了解一点。"),
                f.t("陈雨涵", "怎么打开，首先要输对密码，插入钥匙顺时针转就可以了。"),
                f.t("苏梦忆", "那如果输错了密码呢？"),
                f.t("陈雨涵", "密码错误的话，钥匙是转不动的，只能拔出来。"),
                f.t("李云萧", "（也就是说密码是第一级的锁。）"),
                f.t("李云萧", "说起来，你知道钥匙孔上ON和OFF的含义吗？"),
                f.t("陈雨涵", "OFF就是锁上的意思，ON就是解锁，不是吗？"),
                f.t("李云萧", "但是为什么有两个ON呢？顺时针转动的话，只写右边的那个就够了。"),
                f.t("李云萧", "你有试过反方向旋转钥匙吗？"),
                f.t("陈雨涵", "没有，社长告诉我怎么打开，我就按照她的方法开的。"),
                //证据-解锁的方法（首先输正确密码，再顺时针转钥匙）
                f.t("李云萧", "还有什么其他人知道密码吗？"),
                f.t("陈雨涵", "我没有告诉过其他人，柳萱她有没有告诉其他人我就不知道了。"),
                f.t("陈雨涵", "不过，知道如何打开的人只有我和社长两人。"),
                f.t("李云萧", "嗯……"),
                f.t("陈雨涵", "请你相信我，我真的没有拿！"),
                f.t("李云萧", "（保险桌的情况就是这样……）")
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
