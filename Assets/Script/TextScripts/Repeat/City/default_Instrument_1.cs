using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_Instrument_1 : TextScript
    {
        public default_Instrument_1(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                //——背景：乐器行——
                f.ChangeBackground(""),
                f.OpenDialog(),
                f.t("李云萧", "好，那我就听一下。"),
                f.t("店员", "请跟我来，这里是专门教室。"),
                f.t("李云萧", "哇，还有专门的教室。"),
                f.t("讲师", "那么，我们这堂课从……"),
                f.t("李云萧", "……"),
                f.t("李云萧", "…………"),
                f.t("李云萧", "原来如此……"),
                f.t("李云萧", "……"),
                f.t("李云萧", "…………"),
                //——背景 傍晚——
                f.t("李云萧", "听完了这节课，顿时对乐器的理解更进了一步。"),
                f.t("李云萧", "差不多该回家了吧。"),
                f.t("李云萧", "（感觉自己的音乐水准提升了！）"),
                //——背景 消失——
                f.FadeoutAll()
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            //TODO:随机增加艺术
            DataManager.GetInstance().gameData.player.AddRandom("艺术", 5, 10);
            return nodeFactory.GetEndTurnNode();
        }

    }
}
