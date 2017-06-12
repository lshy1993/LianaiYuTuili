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
                f.t("李云萧", "是事件发生的时间，也就是玻璃碎掉的时间。"),
                f.t("李云萧", "根据项茂的证言，窗户破掉的时间应该是中午12点整。"),
                f.t("李云萧", "提前10分钟就看到碎掉的玻璃，这是不可能的。"),
                f.t("李云萧", "那么，[ff6600]那个人[-]的证词就存在问题了……"),
                f.t("李云萧", "但是，另一个人也目击到了喵星人，这是怎么回事呢？"),
                f.t("李云萧", "戚海超，他又为什么会到办公室去呢？"),
                f.t("李云萧", "这一点，我应该是有证据的可以证明的……")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ04");
        }

    }
}
