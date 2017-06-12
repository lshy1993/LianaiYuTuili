using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo19 : TextScript
    {
        public demo19(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(0),
                f.t("李云萧", "叶婷，你知道办公室的窗户碎掉是什么时候的事情吗？"),
                f.t("叶婷", "这个……不是11点50分的事情吗？"),
                f.t("李云萧", "根据我的调查，打碎窗户的时间是12点整。"),
                f.t("李云萧", "我这里有份足球社社员的证词，可以证实我所言不假。"),
                f.t("叶婷", "欸？是这样的吗？"),
                f.t("李云萧", "那么，你为什么会提早10分钟就听到，窗户被打碎的声音？"),
                f.t("叶婷", "！！"),
                f.t("叶婷", "这个、这个……是我记错时间了……"),
                f.t("叶婷", "11点50分不是我看到喵星人的时间，而是……"),
                f.t("李云萧", "而是什么？"),
                f.t("叶婷", "有了，我应该这么讲的，请让我再说一次。"),
                f.t("李云萧", "那么这次，请你真实地说明。")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ04");
        }

    }
}
