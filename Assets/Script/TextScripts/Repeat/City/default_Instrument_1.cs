using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_Instrument_1 : TextScript
    {
        public default_Instrument_1(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                //——背景：乐器行——
                f.OpenDialog(),
                f.t("【李云萧】", "听课的部分"),
                f.FadeoutAll()
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            //TODO:随机增加艺术
            return nodeFactory.GetEndTurnNode();
        }

    }
}
