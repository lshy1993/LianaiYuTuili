using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_TeachOne : TextScript
    {
        public default_TeachOne(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                //——背景：一号教学楼——
                f.OpenDialog(),
                f.t("【李云萧】", "……"),
                f.t("【李云萧】", "…………"),
                f.t("【李云萧】", "走廊上没有看到什么人。"),
                f.t("【李云萧】", "同学们都在教室里看着书。"),
                f.t("【李云萧】", "……"),
                f.t("【李云萧】", "…………"),
                f.t("【李云萧】", "真可惜，什么事也没发生。"),
                f.t("【李云萧】", "既然这样，还是离开这里吧."),
                f.t("【李云萧】", "一天就这样过去了……"),
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
