using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_BookStore_1 : TextScript
    {
        public default_BookStore_1(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.t("【李云萧】", "那么，准备买什么类型的书呢？"),
                f.t("", "本系统还未制作完善，自动进入看书")
                //——背景 消失——
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            //TODO：购物系统
            return nodeFactory.FindTextScript("default_BookStore_0");
        }

    }
}
