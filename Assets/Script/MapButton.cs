using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using LitJson;
using Assets.Script.GameStruct;
using System.Collections;

namespace Assets.Script.UIScript
{
    /// <summary>
    /// MapButton
    /// 大地图按钮
    /// </summary>
    public class MapButton : MonoBehaviour
    {
        private List<MapEvent> events;
        public string position;
        private MapNode node;
        private EventManager eventManager;

        private GameManager gm;
        private MapManager mapm;
        private Hashtable gVars;

        void Start()
        {
            GameObject root = GameObject.Find("UI Root");
            mapm = root.transform.Find("Map_Panel").gameObject.GetComponent<MapManager>();
            gVars = GameManager.GetGlobalVars();

            events = new List<MapEvent>();
            eventManager = EventManager.GetInstance();

        }
        

        void OnClick()
        {
            events.Union(eventManager.GetAvailableEvents(position));
            
            if(events == null || events.Count < 1)
            {
                // do nothing 

            }
            else
            {
                GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
                MapNode node = gm.node as MapNode;
                if(node != null)
                {
                    node.
                    node.end = true;
                }


            }

        }

    }

}
