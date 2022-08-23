using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_DomTwo : TextScript
    {
        public default_DomTwo(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                //——背景：一号教学楼——
                f.OpenDialog(),
                f.t("【李云萧】", "……"),
                f.t("【李云萧】", "…………"),
                f.t("【李云萧】", "为什么要来到女生楼下……"),
                f.t("【李云萧】", "被人误会了就不好了……"),
                f.t("【李云萧】", "还是赶紧离开这里吧……"),
                f.t("【李云萧】", "一天就这样过去了……")
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
