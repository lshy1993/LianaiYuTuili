using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class T11005_1 : TextScript
    {
        public T11005_1(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps):base(gVars, lVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            pieces = new List<Piece>()
            {
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "说道盗窃，最容易想到的就是钱吧。"),
                //——背景 助手侧——
                //——立绘 苏梦忆侧面——
                f.t("【苏梦忆】", "哎哟……我的头好痛……"),
                f.t("【李云萧】", "怎么了？"),
                f.t("【苏梦忆】", "我求你看下调查记录好吗？"),
                f.t("【李云萧】", "（唔……看来是答错了……）"),
                //——背景 法庭-审判长侧——
                //——立绘 审判长——
                f.t("【审判长】", "我再问一次，请认真作答。"),
                f.t("【审判长】", "被盗窃的东西是？",() => pieces.Count)
                /*
                这里要跳到选项处
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
