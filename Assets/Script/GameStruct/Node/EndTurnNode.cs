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
        public EndTurnNode(DataManager manager, GameObject root, PanelSwitch ps) : base(manager, root, ps)
        {
            em = EventManager.GetInstance();
            factory = NodeFactory.GetInstance();
            Update();
        }

        public override void Update()
        {
            DataManager.GetInstance().MoveOneTurn();
            em.FinishCurrentEvent();
            end = true;
        }

        public override GameNode NextNode()
        {
            if (em.GetCurrentForceEvent() != null)
            {
                //根据强制事件表执行
                return em.RunForceEvent();
            }
            else
            {
                int turn = DataManager.GetInstance().GetGameVar<int>("回合");
                DateTime date = DataManager.START_DAY.AddDays(turn);
                int week = Convert.ToInt32(date.DayOfWeek);
                //TODO : 对节日判断
                if (week == 6 || week == 0)
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
