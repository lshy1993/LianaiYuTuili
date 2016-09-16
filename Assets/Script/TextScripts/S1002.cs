using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class S1002 : TextScript
    {
        public S1002(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——背景 书法社——
                f.t("【李云萧】", "【苏梦忆】事件2"),
                f.t("【李云萧】", "进入下一天",() => pieces.Count)
                //——背景 消失——
            };
        }

        public override GameNode NextNode()
        {
            //return base.NextNode();
            Finish();
            //return nodeFactory.GetEduNode("");
            return nodeFactory.GetMapNode();
        }

    }
}
