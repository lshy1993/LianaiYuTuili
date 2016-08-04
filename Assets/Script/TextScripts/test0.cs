using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class test0 : TextScript
    {
        public test0(Hashtable gVars, GameObject root, PanelSwitch ps):base(gVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            pieces = new List<Piece>()
            {
                f.t("","直接进入map"),
                f.t("","直接进入map",() => pieces.Count),
            };
        }

        public override GameNode NextNode()
        {
            //return base.NextNode();
            Finish();
            return nodeFactory.GetMapNode();
        }

    }
}
