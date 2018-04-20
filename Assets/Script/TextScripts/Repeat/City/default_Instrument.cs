using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_Instrument : TextScript
    {
        public default_Instrument(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                //——背景：乐器行——
                f.ChangeBackground(""),
                f.OpenDialog(),
                f.t("李云萧", "总觉得，这些钢琴的价格……"),
                f.t("李云萧", "哇，这么贵……"),
                f.t("店员", "乐理课程现在特价啦！"),
                f.t("店员", "这位帅哥，乐器培训了解一下！"),
                f.t("李云萧", "嗯？乐器……培训？"),
                f.t("店员", "没错！现在加入的话，还能享受特别优惠！"),
                f.t("店员", "只要98，只要98块钱！"),
                f.t("李云萧", "哦哦……好好……"),
                f.t("李云萧", "（怎么办，要去听一下课吗？）"),
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("听一堂", "default_Instrument_1");
            dic.Add("算了，太贵了", "default_Instrument_0");
            return nodeFactory.GetSelectNode(dic);
        }

    }
}
