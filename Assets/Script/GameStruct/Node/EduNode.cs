using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public class EduNode : GameNode
    {
        public EduNode(Hashtable gVars, GameObject root, PanelSwitch ps, string type):base(gVars, root, ps)
        {

        }
        public override void Update(){ /* DO NOTHING */}

        public override void Init()
        {
            base.Init();

            ps.SwitchTo("Edu");
        }


        public override GameNode NextNode()
        {
            return base.NextNode();
        }
    }
}
