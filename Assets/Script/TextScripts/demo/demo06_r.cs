using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo06_r : TextScript
    {
        public demo06_r(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("苏梦忆", "李云萧，这是怎么回是啊？"),
                f.t("李云萧", "总觉得，她刚才的话，有些模糊的地方。"),
                f.t("苏梦忆", "可是我们也不证明她在说谎啊。"),
                f.t("李云萧", "[66ccff]（但是，就调查结果看，不对劲。）[-]"),
                f.t("李云萧", "[66ccff]（试着进行那个吧。）[-]")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ00");
        }

    }
}
