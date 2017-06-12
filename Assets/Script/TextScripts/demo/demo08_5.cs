using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo08_5 : TextScript
    {
        public demo08_5(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "你碰到了叶婷？"),
                f.t("戚海超", "没错，我刚到的时候，她正好从教室出来。"),
                f.t("李云萧", "那个时候，她在干什么呢？"),
                f.t("戚海超", "她掏出钥匙，准备开办公室的门。"),
                f.t("李云萧", "办公室是锁着的吗？"),
                f.t("戚海超", "我想是的，不然她也不会用钥匙去开。"),
                f.t("李云萧", "嗯……")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ02");
        }

    }
}
