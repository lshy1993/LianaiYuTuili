using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_ThemePark : TextScript
    {
        public default_ThemePark(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                //——背景：游乐园 白天——
                f.OpenDialog(),
                f.t("【李云萧】", "远方的过山车传来了人们的叫喊声。"),
                f.t("【李云萧】", "感觉一个人来，少了些什么。"),
                f.t("【李云萧】", "机会难得，好好放松吧！"),
                f.t("【李云萧】", "……"),
                f.t("【李云萧】", "…………"),
                f.t("【李云萧】", "哈——哈——"),
                f.t("【李云萧】", "没想到——还是——有点刺激的——"),
                f.t("【李云萧】", "差不多玩够了，回家吧。"),
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
