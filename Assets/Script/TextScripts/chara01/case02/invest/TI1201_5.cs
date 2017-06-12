using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TI1201_5 : TextScript
    {
        public TI1201_5(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //*调查->地上的玻璃
                f.t("【李云萧】", "掉在地上的碎玻璃，好像上面有点红色的血迹。"),
                f.t("【李云萧】", "是有什么人被玻璃划破了吗？")
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
