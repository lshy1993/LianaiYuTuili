﻿using Assets.Script.GameStruct.Model;
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

        private EduManager eduManager;
        private EduUIManager uiManager;

        private List<EduEvent> allEvents;
        private GameNode next;
        private NodeFactory factory;

        public EduNode(Hashtable gVars, Hashtable lVars,GameObject root, PanelSwitch ps):
            base(gVars, lVars, root, ps)
        {
            Init();
            ps.SwitchTo_VerifyIterative("Edu_Panel");
            player = manager.GetGameVar<Player>("玩家");
        }

        public void Init()
        {
            allEvents = (List<EduEvent>)DataPool.GetInstance().GetStaticVar("养成按钮");

            uiManager = root.transform.Find("Edu_Panel").GetComponent<EduUIManager>();
            //uiManager.transform.gameObject.SetActive(true);
            uiManager.SetEduNode(this);
            //uiManager.SetEduButton();
            uiManager.SetRandomSchedule();
            uiManager.SetEduEvent(allEvents);

            factory = NodeFactory.GetInstance();
        }

        public override void Update()
        { }

        public void EduExit()
        {
            //DataManager.GetInstance().MoveOneTurn();
            this.next = factory.GetEndTurnNode();
            base.end = true;
        }

        public void ReturnMap()
        {
            this.next = factory.GetMapNode();
            base.end = true;
        }

        public override GameNode NextNode()
        {
            return next;
        }

    }
}
