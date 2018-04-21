using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class S4001 : TextScript
    {
        public S4001(DataManager manager, GameObject root, PanelSwitch ps) : base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //——背景：艺术楼 走廊——
                //——SE 钢琴声——
                f.OpenDialog(),
                f.t("李云萧", "[66ccff]（远处传来了钢琴声……）[-]"),
                f.t("李云萧", "[66ccff]（但是现在这个时间点，大家都去食堂了吧。）[-]"),
                f.t("李云萧", "[66ccff]（肯定是哪个学生在这里练习。）[-]"),
                f.t("李云萧", "……")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetMapNode();
        }

    }
}