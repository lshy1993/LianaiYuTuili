using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class T11003_2 : TextScript
    {
        public T11003_2(Hashtable gVars, GameObject root, PanelSwitch ps):base(gVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            pieces = new List<Piece>()
            {
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "被告的话，是指我身边的苏梦忆吧。"),
                //——背景 助手侧——
                //——立绘 苏梦忆侧面——
                f.t("【苏梦忆】", "喂，李云萧，我回去了！"),
                f.t("【李云萧】", "别，别丢下我一人，抱歉，刚刚开个玩笑。"),
                f.t("【苏梦忆】", "真是的！被告人指的是现在受审的人，也就是你的委托人！"),
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
