using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo00_3 : TextScript
    {
        public demo00_3(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.FadeinBackground("qs"),
                f.OpenDialog(),
                f.t("", "[00ff00]恭喜你学会了试玩版的主要玩法！[-]"),
                f.t("", "[00ff00]由于是试玩版，接下来的剧情需要达成一定的条件才能进入。"),
                f.t("", "[00ff00]本次游戏中，总计[ff6600]20天[-]，到期后自动结束游戏。[-]"),
                f.t("", "[00ff00]请将[ff6600]文科[-]提升至[ff6600]55[-]，[ff6600]理科[-]提升至[ff6600]55[-]后，在校园内触发相关事件。[-]"),
                f.t("", "[00ff00]在正式游戏中，有些事件具有[ff6600]时效性[-]，请注意时间的安排。[-]"),
                f.t("", "[00ff00]敬请体验新的玩法吧！[-]")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEduNode();
        }

    }
}
