using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demofin : TextScript
    {
        public demofin(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("","[00ff00]衷心感谢您试玩本游戏的Alpha开发版本！！[-]"),
                f.t("","[00ff00]本试玩版仅用于展示游戏玩法，人物设定等还在紧张地制作中。[-]"),
                f.t("","[00ff00]在下一个版本中，将会不断完善游戏画面与逻辑剧情。[-]"),
                f.t("","[00ff00]敬请期待！[-]")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetFinNode();
        }

    }
}
