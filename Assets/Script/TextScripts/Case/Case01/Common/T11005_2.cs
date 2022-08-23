using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class T11005_2 : TextScript
    {
        public T11005_2(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "我想偷的是作业吧。"),
                //——背景 助手侧——
                //——立绘 苏梦忆侧面——
                f.t("【苏梦忆】", "你是在自言自语么，为什么会想到作业？"),
                f.t("【李云萧】", "唔……（可能因为我是学生吧……）"),
                f.t("【苏梦忆】", "还是不要开这种玩笑了，审判长正看着呢。"),
                f.t("【李云萧】", "（还是再看下调查记录吧。）"),
                //——背景 法庭-审判长侧——
                //——立绘 审判长——
                f.t("【审判长】", "我再问一次，请认真作答。"),
                f.t("【审判长】", "被盗窃的东西是？")
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
