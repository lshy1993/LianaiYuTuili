using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo0_1 : TextScript
    {
        public demo0_1(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("","[00ff00]本游戏尚处于Alpha开发阶段，游戏的部分功能可能无法正常使用。[-]"),
                //f.t("","[00ff00]本测试版仅供试玩，不作任何商业用途，\n部分使用的免费资源均来自互联网。[-]"),
                f.t("","[00ff00]游戏还在艰难地制作中，急需人手，有意者请与作者本人联系。[-]"),
                f.t("","[00ff00]由于作者暂时不会画画，很多画面请自行脑补。[-]"),
                f.t("","[00ff00]对了忘记说了，作者是《逆转裁判》和《弹丸论破》系列的粉丝，\n这次作品的很多系统灵感来源于这两游戏。[-]"),
                f.t("","[00ff00]话有点多，那么请开始游玩吧！祝你游戏愉快！[-]"),
                f.CloseDialog(),
                f.FadeinBackground("gate")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetSwitchNode("8月31日 上午", "华欣外国语学校 校门", "demo0_2");
        }

    }
}
