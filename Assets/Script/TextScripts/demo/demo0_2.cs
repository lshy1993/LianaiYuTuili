using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo0_2 : TextScript
    {
        public demo0_2(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.CloseDialog(),
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetSelectNode("00001");
            //return nodeFactory.GetExamNode();
        }

    }
}
