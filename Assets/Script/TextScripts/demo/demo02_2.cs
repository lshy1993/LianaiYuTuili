using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo02_2 : TextScript
    {
        public demo02_2(DataManager manager, GameObject root, PanelSwitch ps) : base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*调查->办公桌上的袋子
                f.OpenDialog(0),
                f.t("李云萧", "档案袋，上面写着“单元测试”，已经被打开了。"),
                f.t("李云萧", "这不是昨天刚考完的试卷么？")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetDetectJudgeNode("detest2");
        }

    }
}
