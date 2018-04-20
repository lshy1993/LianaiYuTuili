using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_GameCenter_0 : TextScript
    {
        public default_GameCenter_0(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("【李云萧】", "算了，这回还是不进去玩了。"),
                f.t("", "（在周边地区漫无目的地逛了一个下午。）"),
                //——背景：电玩 傍晚——
                f.t("【李云萧】", "时候不早了，回家吧。"),
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
