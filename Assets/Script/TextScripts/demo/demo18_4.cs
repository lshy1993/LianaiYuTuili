using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo18_4 : TextScript
    {
        public demo18_4(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "钥匙？"),
                f.t("叶婷", "办公室的钥匙。"),
                f.t("李云萧", "为什么你会有办公室的钥匙？"),
                f.t("叶婷", "每个科目的总代表可以得到对应办公室的备用钥匙。"),
                f.t("李云萧", "是么？"),
                f.t("叶婷", "这样，即使老师不在的时候，我也能将作业交上去。"),
                f.t("李云萧", "也就是说，你在任何时间能进入办公室？"),
                f.t("叶婷", "是的……")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ03");
        }

    }
}
