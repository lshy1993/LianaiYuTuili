using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_GrandTheatre : TextScript
    {
        public default_GrandTheatre(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                //——背景：大剧院外景——
                f.OpenDialog(),
                f.t("男", "这个剧没有上次的好看啊。"),
                f.t("女", "我也这么觉得，感觉这次的编剧没有把结尾收住。"),
                f.t("小女孩", "妈妈，我下次还要来！"),
                f.t("李云萧", "最近又有什么新剧上映呢？"),
                f.t("李云萧", "……"),
                f.t("李云萧", "…………"),
                f.t("李云萧", "那就看这个把！"),
                //——背景：大剧院内景——
                f.t("李云萧", "……"),
                f.t("李云萧", "哦哦，演员入场了！"),
                //切换背景
                f.t("李云萧", "……"),
                f.t("李云萧", "…………"),
                f.t("李云萧", "真好看呐！不过，什么事也没发生。"),
                f.t("李云萧", "一天就这样过去了……"),
                f.FadeoutAll()
                //——背景 消失——
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEndTurnNode();
        }

    }
}
