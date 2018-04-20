﻿using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class T11001 : TextScript
    {
        public T11001(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——背景 全黑——
                f.OpenDialog(),
                f.t("李云萧", "……嗯……嗯……"),
                f.t("李云萧", "……嗯……好困……"),
                //——背景 候审室——
                //——背景 全黑——
                f.t("李云萧", "……嗯……谁开的灯……"),
                f.t("李云萧", "……嗯……还没睡够……"),
                //——背景 候审室——
                //——背景 全黑——
                f.t("李云萧", "……好亮……嗯？"),
                //——背景 审判室外——
                f.t("李云萧", "这、这是哪？"),
                //——立绘 正装（西装）苏梦忆？——
                f.t("？？？", "快醒醒！快醒醒！"),
                f.t("李云萧", "苏、苏梦忆！？"),
                f.t("苏梦忆", "你在说什么梦话呢？给我点醒过来！"),
                //——镜头震动——
                f.t("李云萧", "哇！别打了！"),
                f.t("苏梦忆", "马上就要开庭审理了！"),
                f.t("李云萧", "开庭？什么情况？"),
                f.t("苏梦忆", "怎么睡迷糊了？"),
                f.t("李云萧", "是有点没睡醒，但、但是这是哪？"),
                f.t("苏梦忆", "这里？这里是被告人候审室啊。"),
                f.t("李云萧", "不对，我怎么会在这里？还有，你怎么也在这里？"),
                f.t("苏梦忆", "喂喂，别开玩笑，你当然是来辩护的啦！"),
                f.t("苏梦忆", "我是你的助手，自然要跟过来啊。"),
                f.t("李云萧", "辩护？替谁啊？再说，我还是学生不是律师。"),
                f.t("李云萧", "还有，你什么时候是我的助手了？我们才成为同学不久……"),
                f.t("苏梦忆", "你是刚刚进入凌理律师事务所的新人律师。"),
                f.t("李云萧", "嗯？律师？（这是什么情况？）"),
                f.t("苏梦忆", "将来一定会成为著名的大律师，姐姐是这么说的。"),
                f.t("苏梦忆", "当然，我从心底也是这么想的！"),
                f.t("李云萧", "等、等会！你、你有姐姐？"),
                f.t("苏梦忆", "是啊，昨天你和姐姐她还见过面呢。"),
                f.t("李云萧", "昨天？（昨天我明明还呆在学校里啊？）"),
                f.t("苏梦忆", "怎么睡得连这个都忘记了，赶快醒醒！"),
                f.t("李云萧", "（可是我一点也记不起来了……难道说，是我失忆了？）"),
                f.t("李云萧", "（从我高中转学开始到现在的记忆，全部忘记了？）"),
                f.t("李云萧", "现在……是哪一年？"),
                f.t("法警", "时间快到了，请辩护方尽快入庭！"),
                f.t("苏梦忆", "好了，赶紧进去！"),
                f.t("李云萧", "哇……别推我啊……"),
                //——立绘 正装苏梦忆 消除——
                //——背景 全黑——
                f.t("李云萧", "（于是，在还没有搞清楚的情况下，本该在学校里过着普通的高中生活，就这样被眼前这个自称我助手的人，拖了进去。）")
                //——背景 消失——
            };
        }

        public override GameNode NextNode()
        {
            //return base.NextNode();
            Finish();
            return nodeFactory.FindTextScript("T11002");
            //return nodeFactory.GetMapNode();
        }

    }
}
