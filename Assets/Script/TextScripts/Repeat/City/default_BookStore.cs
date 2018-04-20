using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_BookStore : TextScript
    {
        public default_BookStore(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //——背景：新华书店——
                f.ChangeBackground(""),
                f.OpenDialog(),
                f.t("【李云萧】", "好，今天就是来补充知识的！"),
                f.t("【李云萧】", "那么，要做什么呢？")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("买书", "default_BookStore_1");
            dic.Add("随便翻翻", "default_BookStore_0");
            return nodeFactory.GetSelectNode(dic);
        }

    }
}
