﻿using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class S0000 : TextScript
    {
        public S0000(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧","学校周末不上课，该去哪里呢？")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetMapNode();
        }

    }
}
