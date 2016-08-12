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
        }

        public override void Update()
        {
            /* TODO: 过场动画？ */
            Debug.Log("EndTurnNode");
            Debug.Log("forceEventTable size:" + em.getForceEvents().Count);
            //if(em.currentEvent != null)
            //{
            em.FinishCurrentEvent();
            DataManager.GetInstance().MoveOneTurn();
            //}

            end = true;
        }

        public override GameNode NextNode()
        {
            MapEvent e = em.GetCurrentForceEvent();
            if (e != null)
            {
                //根据强制事件表执行
                return factory.FindTextScript(e.entryNode);
            }
            else
            {
                return factory.GetMapNode();
            }
        }

    }
}
