using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    /// <summary>
    /// WeekNode
    /// 一周的行为
    /// </summary>
    public class WeekNode : GameNode
    {
        private bool finished;

        private EventManager em;
        private GameNode next;
        public WeekNode(Hashtable gVars, GameObject root, PanelSwitch ps):base(gVars, root, ps)
        {
            next = this;
            em = EventManager.GetInstance();
        }

        public override void Init()
        {
            base.Init();
            finished = false;
            ps.SwitchTo("Map");
        }
        public override void Update()
        {
            if (!finished)
            {
                // 每日刷新
                
                
            }

        }

        public void SetNext(GameNode node)
        {
            this.next = node;

        }
        public override GameNode NextNode()
        {
            return next;
        }
    }
}
