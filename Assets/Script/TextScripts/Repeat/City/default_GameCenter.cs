using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_GameCenter : TextScript
    {
        public default_GameCenter(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //——背景：电玩——
                f.ChangeBackground("gamecenter_day"),
                f.OpenDialog(),
                f.t("女", "好可惜啊！"),
                f.t("男", "差一点就抓到了，下回再来吧！"),
                f.t("李云萧", "……"),
                f.t("李云萧", "风云电玩，从外面就能依稀听到内部的喧闹。"),
                f.t("李云萧", "怎么办，要花钱进去吗？")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("进！", "default_GameCenter_1");
            dic.Add("不进去", "default_GameCenter_0");
            return nodeFactory.GetSelectNode(dic);
        }

    }
}
