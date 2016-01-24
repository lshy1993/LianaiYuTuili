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
        public WeekNode(Hashtable gVars, GameObject root, PanelSwitch ps):base(gVars, root, ps)
        { }
        public override void Init()
        {
            base.Init();
            finished = false;
            ps.SwitchTo("Edu");
        }
        public override void Update()
        {
            if (!finished)
            {
                // 每日刷新
                
                
            }

        }
    }
}
