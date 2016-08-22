using Assets.Script.GameStruct.Model;
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
        private Player player;

        public EduNode(Hashtable gVars, Hashtable lVars,GameObject root, PanelSwitch ps, string type):
            base(gVars, lVars, root, ps)
        {
            player = (Player)gVars["玩家数据"];
        }
        public override void Update(){ /* DO NOTHING */}

        public override void Init()
        {
            base.Init();

            ps.SwitchTo("Edu");
        }


        public override GameNode NextNode()
        {
            DataManager.GetInstance().MoveOneTurn();
            return NodeFactory.GetInstance().GetMapNode();
        }
    }
}
