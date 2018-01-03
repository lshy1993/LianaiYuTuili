using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo20_6 : TextScript
    {
        public demo20_6(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(0),
                f.t("李云萧", "那你为什么不早说？"),
                f.t("叶婷", "第一次遇到这种事情，有些紧张。"),
                f.t("叶婷", "你看，人无完人，总会出错的。"),
                f.t("李云萧", "你这好像已经不是第一次了吧……"),
                f.t("叶婷", "啊哈，啊哈哈……"),
                f.t("李云萧", "[ff6600]（看来是问不出什么有用的信息了。）[-]")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ04");
        }

    }
}
