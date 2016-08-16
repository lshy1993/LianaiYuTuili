using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class T11003_1 : TextScript
    {
        public T11003_1(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps):base(gVars, lVars, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "被告人指的是我，李云萧吧。"),
                //——背景 助手侧——
                //——立绘 苏梦忆侧面——
                f.t("【苏梦忆】", "喂喂喂！你在开什么国际玩笑！"),
                f.t("【苏梦忆】", "你听清楚没有，问的是被告的名字。"),
                f.t("【苏梦忆】", "被告人指的是现在受审的人，而你是辩护律师呀！"),
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "啊哈哈哈……"),
                //——背景 法庭-审判长侧——
                //——立绘 审判长——
                f.t("【审判长】", "我没有听清楚，请再回答一遍。"),
                f.t("【审判长】", "本次事件的被告人是？……请说说看。",() => pieces.Count)
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
