using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class S3002_1 : TextScript
    {
        public S3002_1(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——背景 商场——
                f.t("【李云萧】", "（还是不要去管闲事了……）"),
                //——CG 消失——
                //——立绘 背影——
                f.t("【店员】", "喂，你在干什么！"),
                //——SE 跑步声——
                //——立绘 消失——
                f.t("【李云萧】", "好像被发现了，一溜烟跑走了。"),
                f.t("【李云萧】", "（看到了奇怪的一幕……）"),
                f.t("【李云萧】", "算了，还是想想今天该买些什么吧。",() => pieces.Count),
                //——背景 消失——
            };
        }

        public override GameNode NextNode()
        {
            //return base.NextNode();
            Finish();
            //return nodeFactory.GetEduNode("");
            return nodeFactory.GetMapNode();
        }

    }
}
