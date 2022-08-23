using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_ScienceMuseum : TextScript
    {
        public default_ScienceMuseum(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                //——背景：科技馆外景——
                f.OpenDialog(),
                f.t("【老师】", "一班的同学们，请排队跟好老师，我们要去参观了！"),
                f.t("【小学生】", "好！"),
                f.t("【李云萧】", "这么多人，而且都是小学生。"),
                f.t("【李云萧】", "不过，也挺怀念这种感觉。"),
                f.t("【李云萧】", " 没想到，都高中生了，会再一次参观科技馆。"),
                f.t("【李云萧】", "……"),
                f.t("【李云萧】", "…………"),
                f.t("【李云萧】", "不得不说，科技真的改变了人类的生活。"),
                f.t("【李云萧】", "展览的最新科技，真希望能快点通用啊。"),
                f.t("【李云萧】", "真可惜，什么事也没发生，回家吧。"),
                f.t("【李云萧】", "（感觉内心充满了读书的决心！）"),
                f.FadeoutAll()
                //——背景 消失——
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEndTurnNode();
        }

    }
}
