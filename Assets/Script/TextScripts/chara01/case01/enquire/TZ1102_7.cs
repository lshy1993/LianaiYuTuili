using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1102_7 : TextScript
    {
        public TZ1102_7(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "你绝对没有认错人吗？"),
                //——背景 证人台侧——
                //——立绘 叶枫婷——
                f.t("【叶枫婷】", "没有，绝对没有。"),
                //——背景 法庭-检察方侧——
                //——立绘 检察官——
                f.t("【沈尘业】", "没有搞错是吧？"),
                //——背景 助手侧——
                //——立绘 苏梦忆侧面——
                f.t("【苏梦忆】", "李云萧，怎么办？"),
                f.t("【李云萧】", "该怎么办，苏梦忆？"),
                f.t("【苏梦忆】", "你别问我呀，总之，要证明他并没有看到那一幕。"),
                f.t("【李云萧】", "（刚才的证词里，一定哪里存在着矛盾……）"),
                /*
                这里要跳转【继续询问】
                */
            };
        }

        public override GameNode NextNode()
        {
            //return base.NextNode();
            Finish();
            return nodeFactory.FindTextScript("T11002");
            //return nodeFactory.GetMapNode();
        }

    }
}
