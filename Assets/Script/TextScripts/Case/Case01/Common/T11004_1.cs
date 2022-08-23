using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class T11004_1 : TextScript
    {
        public T11004_1(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "是杀人案。"),
                //——背景 助手侧——
                //——立绘 苏梦忆侧面——
                f.t("【苏梦忆】", "你是怎么一本正经地胡说八道的？"),
                f.t("【李云萧】", "诶，不对吗？不是杀人案吗？"),
                f.t("【苏梦忆】", "当然不是！你好好看下调查记录呀！"),
                f.t("【李云萧】", "我知道了，我看下记录。"),
                //——背景 法庭-审判长侧——
                //——立绘 审判长——
                f.t("【审判长】", "我没有听清楚你的答案。"),
                f.t("【审判长】", "这回审理的案件性质是？")
                /*
                这里要跳到选项处
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
