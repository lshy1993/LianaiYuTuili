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
