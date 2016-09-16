using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class T11004_2 : TextScript
    {
        public T11004_2(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "是盗窃案。"),
                //——背景 法庭-审判长侧——
                //——立绘 审判长——
                f.t("【审判长】", "完全正确，那么最后一个问题。"),
                f.t("【审判长】", "被盗窃的东西是？",() => pieces.Count)
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
