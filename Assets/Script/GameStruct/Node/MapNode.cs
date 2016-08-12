using Assets.Script.GameStruct.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.GameStruct.Model;

namespace Assets.Script.GameStruct
{
    /// <summary>
    /// GameNode
    /// 
    /// </summary>
    public class MapNode : GameNode
    {

        private GameNode next = null;
        private Player player;

        public MapNode(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps):
            base(gVars, lVars, root, ps)
        {
            this.next = this;

            player = (Player)gVars["玩家数据"];
        }


        public override void Init()
        {
            base.Init();

            // TODO: 检查是否有特殊事件，有则跳转

            ps.SwitchTo("Map");


        }
        public override void Update() { }

        public void ChooseNext(GameNode next)
        {
            this.next = next;

            base.end = true;
        }

        public override GameNode NextNode()
        {
            GameNode temp = this.next;

            base.end = false;

            this.next = null;

            return temp;
        }

    }
}
