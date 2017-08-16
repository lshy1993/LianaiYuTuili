using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo000 : TextScript
    {
        public demo000(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("","[00ff00]恭喜你学会了如何进入大地图。[-]"),
                f.t("","[00ff00]但是由于作者没有写完本该发生的事件……[-]"),
                f.t("","[00ff00]所以就跳过这回合，以后的双休日也是！[-]"),
                f.t("","[00ff00]哦，对了，在双休日可以继续学习！[-]"),
                f.t("","[00ff00]好了，晚安吧！[-]"),
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
