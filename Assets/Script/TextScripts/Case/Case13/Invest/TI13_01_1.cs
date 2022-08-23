using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TI13_01_1 : TextScript
    {
        public TI13_01_1(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*调查->窗户
                f.t("李云萧", "普通的窗户，窗外可以看得到操场。"),
                f.t("李云萧", "阳光照进来的部分刚好。"),
                f.t("李云萧", "在一个午后坐在这里看书是非常有意思的吧。"),
                /*
                这里要跳回现场调查】
                */
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.FindTextScript("T11002");
        }

    }
}
