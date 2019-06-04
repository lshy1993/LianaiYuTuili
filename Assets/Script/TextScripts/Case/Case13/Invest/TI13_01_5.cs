using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TI1301_5 : TextScript
    {
        public TI1301_5(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*调查->保险桌内部
                f.t("李云萧", "原来内部的空间还挺大的。"),
                f.t("苏梦忆", "嗯，主要分成左右两个空间。"),
                f.t("李云萧", "现在左侧的空间放满了杂物，右边的空间则是空的。"),
                //证据-保险箱内部
                f.t("苏梦忆", "也就是说，书法被放在了右边了吧？"),
                f.t("李云萧", "我想是的，卷轴按对角线应该正好放得下。"),
                f.t("苏梦忆", "果然是被人拿走了……"),
                f.t("李云萧", "不过，我总感觉这里少了些什么。")
                /*
                这里要跳回现场调查
                */
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.FindTextScript("T11002");
        }

    }
}
