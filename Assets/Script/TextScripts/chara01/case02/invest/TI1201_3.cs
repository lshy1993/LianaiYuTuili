using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TI1201_3 : TextScript
    {
        public TI1201_3(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //*调查->玻璃窗
                f.t("【李云萧】", "办公室的外面就是操场，从操场传来了打球的声音。")
                /*
                这里要跳回【现场调查】
                */
            };
        }

        public override GameNode NextNode()
        {
            //return base.NextNode();
            Finish();
            return nodeFactory.FindTextScript("T11002");
            //return nodeFactory.GetMapNode();
        }

    }
}
