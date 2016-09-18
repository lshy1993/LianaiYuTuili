using Assets.Script.GameStruct.Model;
using Assets.Script.UIScript;
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
        private Player player;
        private MapUIManager uiManager;


        public MapNode(DataManager manager, GameObject root, PanelSwitch ps):
            base(manager, root, ps)
        {
            Init();
            ps.SwitchTo_VerifyIterative("Map_Panel");
        }

        public override void Init()
        {
            base.Init();
            uiManager = root.transform.Find("Map_Panel").GetComponent<MapUIManager>();
            uiManager.mapNode = this;
            //uiManager.GetOut();
        }

        public override void Update() { }

        public void ChooseNext(GameNode next)
        {
            this.next = next;

            base.end = true;
        }

        public void ChooseEdu()
        {
            this.next = NodeFactory.GetInstance().GetEduNode();
            base.end = true;
        }

        public override GameNode NextNode()
        {
            //GameNode temp = this.next;

            //base.end = false;

            //this.next = null;

            //return temp;

            return next;
        }

    }
}
