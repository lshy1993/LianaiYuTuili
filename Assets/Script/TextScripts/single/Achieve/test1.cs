using Assets.Script.GameStruct;
using Assets.Script.UIScript;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class test1 : TextScript
    {
        public test1(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.ChangeBackground("tt"),
                f.OpenDialog(),
                f.t("测试","进入侦探事件")
            };
        }

        public override GameNode NextNode()
        {
            //return base.NextNode();
            Finish();
            return nodeFactory.GetDetectJudgeNode("detest1");
            //return nodeFactory.GetEndTurnNode();
        }

    }
}
