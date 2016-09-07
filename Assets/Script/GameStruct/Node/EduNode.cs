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

        private EduManager eduManager;
        private EduUIManager uiManager;

        private List<EduEvent> allEvents;
        private GameNode next;
        private NodeFactory factory;

        public EduNode(Hashtable gVars, Hashtable lVars,GameObject root, PanelSwitch ps, string type):
            base(gVars, lVars, root, ps)
        {
            Init(type);
            ps.SwitchTo_VerifyIterative("Edu_Panel");
            //player = (Player)gVars["玩家数据"];
        }

        public void Init(string type)
        {
            eduManager = EduManager.GetInstance();

            allEvents = EduManager.GetStaticEduEvents();
            
            uiManager = root.transform.Find("Edu_Panel").GetComponent<EduUIManager>();

            uiManager.transform.gameObject.SetActive(true);

            uiManager.SetEduNode(this);
            uiManager.SetEduEvent(allEvents);
            uiManager.SetEduButton(type);

            factory = NodeFactory.GetInstance();
        }

        public override void Update()
        { }

        public void EduExit()
        {
            //DataManager.GetInstance().MoveOneTurn();
            next = factory.GetEndTurnNode();
            end = true;
        }

        public override GameNode NextNode()
        {
            return next;
        }

    }
}
