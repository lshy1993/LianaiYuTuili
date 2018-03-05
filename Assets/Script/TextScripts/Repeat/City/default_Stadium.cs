using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_Stadium : TextScript
    {
        public default_Stadium(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //——背景：体育馆外景——
                f.OpenDialog(),
                f.t("【李云萧】", "好多人！今天什么情况？"),
                f.t("【男】", "今天是夕云队的主场！"),
                f.t("【李云萧】", "啊，这样啊！"),
                f.t("【李云萧】", "那不得不去加油啊！"),
                f.t("【李云萧】", "……"),
                f.t("【李云萧】", "…………"),
                //——背景：体育馆外景 傍晚——
                f.t("【李云萧】", "真的激烈啊，双方的分数追赶得很紧。"),
                f.t("【李云萧】", "直到最后一分钟才决出胜负。"),
                f.t("【李云萧】", "看得我热血澎湃，好，今天就这样回家吧！"),
                f.t("【李云萧】", "（感觉某方面的能力得到了刺激。）"),
                f.FadeoutAll()
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEndTurnNode();
        }

    }
}
