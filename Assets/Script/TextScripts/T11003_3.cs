using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class T11003_3 : TextScript
    {
        public T11003_3(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps):base(gVars, lVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            pieces = new List<Piece>()
            {
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "被告人应该指的是喵星人吧，这份材料上这么写着。"),
                //——背景 法庭-审判长侧——
                //——立绘 审判长——
                f.t("【审判长】", "嗯，没错，就是这样。"),
                f.t("【审判长】", "你平静地回答就可以了，不紧张了吧。"),
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "嗯、嗯。"),
                f.t("【李云萧】", "（喵星人，这名字怎么感觉哪里听过……）"),
                //——背景 法庭-审判长侧——
                //——立绘 审判长——
                f.t("【审判长】", "下一个问题，像这样回答好了。"),
                f.t("【审判长】", "这回审理的案件性质是？",() => pieces.Count)
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
