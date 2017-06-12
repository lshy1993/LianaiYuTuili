using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1101_1 : TextScript
    {
        public TZ1101_1(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.OpenDialog(0),
                f.t("【李云萧】", "现场真的是完全封闭的吗？"),
                //——背景 证人台侧——
                //——立绘 张傲——
                f.t("【张傲】", "当然，我们检查过，门是用钥匙锁上的。"),
                f.t("【张傲】", "而且，办公室的门没有被撬过的痕迹，除非有钥匙，不然是绝对打不开的。"),
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "嗯……（进入办公室的方法，真的只有从门进入吗？）")
                /*
                这里要跳转【继续询问】
                */
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("Z1101");
        }

    }
}
