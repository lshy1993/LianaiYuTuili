using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_Instrument_0 : TextScript
    {
        public default_Instrument_0(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                //——背景：乐器行——
                f.OpenDialog(),
                f.t("【李云萧】", "算了，心疼我的钱包……"),
                f.t("【李云萧】", "还是看这些精美的乐器吧……"),
                f.t("【李云萧】", "就这样一下午过去了"),
                f.FadeoutAll()
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEndTurnNode();
        }

    }
}
