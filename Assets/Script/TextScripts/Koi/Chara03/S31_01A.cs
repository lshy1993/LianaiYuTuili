using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class S31_01A : TextScript
    {
        public S31_01A(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——背景 商场——
                f.t("李云萧", "（还是不要轻易相信陌生人的好。）"),
                f.t("李云萧", "你和我又不认识，还是不太方便吧……"),
                f.t("？？？", "是吗……不给就算啦……"),
                //——立绘 消失——
                f.t("李云萧", "走掉了，真是个奇怪的女生啊……"),
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
