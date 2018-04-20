using Assets.Script.GameStruct.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public class TimeSwitchNode : GameNode
    {
        private GameNode next = null;
        private TimeUIManager uiManager;
        private NodeFactory factory;
        private GameObject timePanel;
        private string timeStr, placeStr;
        public bool finished;

        public TimeSwitchNode(DataManager manager, GameObject root, PanelSwitch ps, string time, string place, string next)
            : base(manager, root, ps)
        {
            uiManager = root.transform.Find("Avg_Panel/TimeSwitch_Panel").GetComponent<TimeUIManager>();
            factory = NodeFactory.GetInstance();
            uiManager.SetNode(this);
            uiManager.SetLabel(time, place, next);
            //ps.SwitchTo_VerifyIterative("Avg_Panel", uiManager.Show);
        }

        public override void Update()
        { }

        /// <summary>
        /// 结束当前NODE
        /// </summary>
        /// <param name="entry">下个NODE</param>
        public void NodeExit(string entry)
        {
            uiManager.gameObject.SetActive(false);
            next = factory.FindTextScript(entry);
            end = true;
        }

        public override GameNode NextNode()
        {
            return next;
        }

    }
}
