using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo_fail : TextScript
    {
        public demo_fail(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧","不行，找不出结果……"),
                f.t("苏梦忆","那可怎么办？"),
                f.t("李云萧","脑内已经没有……思路了……"),
                f.t("喵星人","李云萧，你可不能抛弃我啊！"),
                f.t("李云萧","可是，已经没有办法了了……"),
                f.t("李云萧","[66ccff]（抱歉……）[-]"),
                f.t("苏梦忆","李云萧……"),
                //背景变黑
                f.t("李云萧","[66ccff]（在那之后，老师回来了。）[-]"),
                f.t("李云萧","[66ccff]（喵星人在接受了严厉的询问后，安全回来了……）[-]"),
                f.t("李云萧","[66ccff]（但是，看喵星人的脸色，或许，不是那么的安全。）[-]"),
                f.t("李云萧","[66ccff]（而苏梦忆，虽然没有说什么，却给我一种疏远感。）[-]"),
                f.t("李云萧","[66ccff]（究竟是哪里出错了呢？）[-]"),
                f.t("李云萧","[66ccff]（事到如今，再回想这个也没有意义了吧……）[-]")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            manager.gameData.player.AddGirlPoint("苏梦忆", -5);
            return nodeFactory.GetEndTurnNode();
        }

    }
}
