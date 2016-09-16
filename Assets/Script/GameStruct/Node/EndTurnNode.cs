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
            em.FinishCurrentEvent();
            DataManager.GetInstance().MoveOneTurn();

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
                return factory.GetMapNode();
            }
        }

    }
}
