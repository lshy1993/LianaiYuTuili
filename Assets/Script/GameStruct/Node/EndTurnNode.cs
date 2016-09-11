using Assets.Script.GameStruct.EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct.Node
{
    public class EndTurnNode : GameNode
    {

        private EventManager em;
        private NodeFactory factory;
        public EndTurnNode(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps) : base(gVars, lVars, root, ps)
        {
            em = EventManager.GetInstance();
            factory = NodeFactory.GetInstance();
            Update();
        }

        public override void Update()
        {
            DataManager.GetInstance().MoveOneTurn();
            em.FinishCurrentEvent();
            Debug.Log("Endturnnode:" + gVars["回合"]);
            end = true;
        }

        public override GameNode NextNode()
        {
            //MapEvent e = em.GetCurrentForceEvent();
            if (em.GetCurrentForceEvent() != null)
            {
                //根据强制事件表执行
                //em.currentEvent = e;
                //return factory.FindTextScript(e.entryNode);
                return em.RunForceEvent();
            }
            else
            {
                DateTime date = (DateTime)gVars["日期"];
                int week = Convert.ToInt32(date.DayOfWeek);
                //DOTO : 对节日判断
                if (week == 6 || week == 7)
                {
                    return factory.FindTextScript("S0000");
                }
                else
                {
                    return factory.GetEduNode();
                }
            }
        }

    }
}
