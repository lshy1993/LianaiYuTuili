using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo15 : TextScript
    {
        public demo15(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(0),
                f.t("李云萧", "项茂说过，是队里的守门员把4楼的窗户给踢碎了。"),
                f.t("李云萧", "刚才就觉得这张表上的名字有点眼熟，作为守门员的果然就是戚海超。"),
                f.t("李云萧", "那么，他来办公室的理由，就是他自己弄碎了窗户。"),
                f.t("李云萧", "还有最后一个问题，他赶到办公室花费了多久的时间？")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetReasoningNode("Qdemo03");
        }

    }
}
