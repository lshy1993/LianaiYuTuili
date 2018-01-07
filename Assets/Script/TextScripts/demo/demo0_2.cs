using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo0_2 : TextScript
    {
        public demo0_2(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.PlayBGM("popsky",0),
                f.t("", "呼……呼……终于来到这所学校了……"),
                f.t("", "[66ccff]出现在我眼前的是私立华欣外国语学校。[-]"),
                f.t("", "不愧是重点高中，正门都不一样……"),
                f.t("", "[66ccff]今天是八月的最后一天，明天就正式开学了。[-]"),
                f.t("", "[66ccff]其实，我是从其他学校转过来的，至于原因嘛，以后会讲。[-]"),
                f.t("", "华欣啊，接下来的生活就都在这里了……"),
                f.CloseDialog(),
                f.FadeoutBackground(),
                f.Wait(0.3f),
                //——背景 走廊——
                f.FadeinBackground("corridor")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetSwitchNode("8月31日 上午", "1号教学楼 4楼 走廊", "demo0_3");
        }

    }
}
