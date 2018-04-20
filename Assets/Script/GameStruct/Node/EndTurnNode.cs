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
        private DataManager dm;

        public EndTurnNode(DataManager manager, GameObject root, PanelSwitch ps) : base(manager, root, ps)
        {
            em = EventManager.GetInstance();
            dm = DataManager.GetInstance();
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
            GameObject.Find("GameManager").GetComponent<SoundManager>().StopBGM();
            //如果有强制事件 则执行
            if (em.GetCurrentForceEvent() != null)
            {
                return em.RunForceEvent();
            }
            int turn = dm.gameData.gameTurn;

            //满足180回合 进入不同的结局
            if (turn == 150)
            {
                return em.RunFinEvent();
            }
            //否则按照日历进行
            DateTime date = DataManager.START_DAY.AddDays(turn);
            int week = Convert.ToInt32(date.DayOfWeek);

            //TODO : 对特殊节日判断
            if (week == 6 || week == 0)
            {
                //进入双休日剧情 考虑废弃 直接进入Map
                return factory.FindTextScript("S0000");
            }
            else
            {
                return factory.GetEduNode();
            }

        }

    }
}
