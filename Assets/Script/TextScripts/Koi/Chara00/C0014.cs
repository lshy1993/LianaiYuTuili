﻿using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class C0014 : TextScript
    {
        public C0014(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //——背景 书法社——
                f.OpenDialog(),
                f.t("【李云萧】", "这是【共线】首个事件……"),
                f.t("【李云萧】", "进入下一天"),
                f.FadeoutAll()
                //——背景 消失——
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEndTurnNode();
        }

    }
}
