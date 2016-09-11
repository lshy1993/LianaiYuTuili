using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class test0 : TextScript
    {
        public test0(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps):base(gVars, lVars, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                f.t("李云萧","今天是第6回合了，进入了强制事件"),
                f.t("李云萧","接下来的测试中，如果属性达到一定的值"),
                f.t("李云萧","闲逛进入【一号教学楼】会触发达成事件"),
                f.t("李云萧","好了，请留意，下一回合")
            };
        }

        public override GameNode NextNode()
        {
            //return base.NextNode();
            Finish();
            return nodeFactory.GetEndTurnNode();
        }

    }
}
