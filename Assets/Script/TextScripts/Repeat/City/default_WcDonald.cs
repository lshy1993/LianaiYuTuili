using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_WcDonald : TextScript
    {
        public default_WcDonald(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                //——背景：WcDonald——
                f.OpenDialog(),
                f.t("【女生】", "最新出的樱花口味哦！"),
                f.t("【女生】", "我还是更喜欢麻将味！"),
                f.t("【女生】", "那、那个，我们在这里做作业没关系吗？"),
                f.t("【李云萧】", "（是啊，这才是重点啊……）"),
                f.t("【李云萧】", "不好，香味飘过来了……"),
                f.t("【李云萧】", "怎么办，要买点吃的吗？")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("买！买！买！", "default_WcDonald_1");
            dic.Add("忍住，我要忍住", "default_WcDonald_0");
            return nodeFactory.GetSelectNode(dic);
        }

    }
}
