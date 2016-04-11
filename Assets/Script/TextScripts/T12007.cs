using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class T12007 : TextScript
    {
        public T12007(Hashtable gVars, GameObject root, PanelSwitch ps):base(gVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            pieces = new List<Piece>()
            {
                //——背景 操场——
                f.t("【李云萧】", "（这里就是学校的操场了，没想到现在还有人在踢球……）"),
                f.t("【李云萧】", "（除了足球场外，露天的篮球场和排球场也有不少人……）"),
                f.t("【苏梦忆】", "我们赶紧开始调查吧！"),
                f.t("【李云萧】", "你怎么对这个特别感兴趣？"),
                f.t("【苏梦忆】", "你不觉得很有趣吗？"),
                f.t("【李云萧】", "还好吧……"),
                f.t("【苏梦忆】", "快看！那边有个穿着球服的同学。"),
                f.t("【李云萧】", "过去问问看……"),
                //——立绘 项茂——
                f.t("【李云萧】", "你好，我想问你点事。"),
                f.t("【？？？】", "……"),
                f.t("【李云萧】", "同学，你好！"),
                f.t("【？？？】", "……"),
                f.t("【苏梦忆】", "噗，好像完全无视你了。"),
                f.t("【李云萧】", "同！学！问！你！点！事！"),
                f.t("【？？？】", "……"),
                f.t("【李云萧】", "……换你来"),
                f.t("【苏梦忆】", "看好了！"),
                f.t("【苏梦忆】", "那个，同学你好……"),
                f.t("【？？？】", "……你好！"),
                f.t("【李云萧】", "（为什么她一来就行……）"),
                f.t("【苏梦忆】", "我叫苏梦忆，是高二（3）班的。"),
                f.t("【项茂】", "我叫项茂，你是来看我们比赛的吗？"),
                f.t("【苏梦忆】", "不、不是的，我想问你一些问题。"),
                f.t("【项茂】", "好啊，随便问，我都会回答你的。"),
                f.t("【李云萧】", "（他的脸怎么红了？没关系吧……）",() => pieces.Count)
                /*
                这里跳转【对话前】
                */
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
