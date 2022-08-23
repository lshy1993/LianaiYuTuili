using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_VideoShop : TextScript
    {
        public default_VideoShop(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                //——背景：音像店 外景——
                f.OpenDialog(),
                f.t("【李云萧】", "来看看今天发售了什么新的专辑吧！"),
                f.t("【李云萧】", "顺带看看有没有喜欢的动画。"),
                f.t("【李云萧】", "……"),
                f.t("【李云萧】", "…………"),
                f.t("【李云萧】", "真可惜，什么事也没发生。"),
                f.t("【李云萧】", "既然这样，还是离开这里吧."),
                f.t("【李云萧】", "（感觉到自己的某方面的到了提升。）"),
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
