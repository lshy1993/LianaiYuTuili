using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1101_x : TextScript
    {
        public TZ1101_x(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "证人现在的话，充满了……矛……矛盾……"),
                //——背景 法庭-检察方侧——
                //——立绘 检察官——
                f.t("【沈尘业】", "哪里？我怎么没看出来！"),
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "也许……没有吧……"),
                //——背景 法庭-检察方侧——
                //——立绘 检察官——
                f.t("【沈尘业】", "辩护律师，请你慎重考虑。"),
                //减血条
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "额……给审判长留下不好的印象了。")
                /*
                这里要跳转【继续询问】
                */
            };
        }

        public override GameNode NextNode()
        {
            //return base.NextNode();
            Finish();
            //return nodeFactory.FindTextScript("T11002");
            //return nodeFactory.GetMapNode();
            return nodeFactory.GetEnquireNode("Z1101");
        }

    }
}
