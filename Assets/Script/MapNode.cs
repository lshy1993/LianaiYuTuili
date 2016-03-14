using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    /// <summary>
    /// GameNode
    /// 
    /// </summary>
    public class MapNode : GameNode
    {

        private GameNode next = null;

        public MapNode(Hashtable gVars, GameObject root, PanelSwitch ps):base(gVars, root, ps)
        {
            this.next = this;
        }


        public override void Init()
        {
            base.Init();

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
