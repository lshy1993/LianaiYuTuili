using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo061 : TextScript
    {
        public demo061(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.StopBGM(),
                f.OpenDialog(),
                f.t("叶婷", "哪里不对了？"),
                f.t("李云萧", "叶婷，你看看这是什么？"),
                f.t("叶婷", "这，这不是考试的试卷吗？你怎么会有的？"),
                f.t("李云萧", "这个啊，我是在办公桌下面发现的。"),
                f.t("李云萧", "叶婷，既然你说在这期间什么都没有发生。"),
                f.t("李云萧", "那么，我手上的试卷是怎么回事？"),
                f.t("叶婷", "那当然是喵星人偷师卷的时候，不小心掉在地上的！"),
                f.t("李云萧", "哦？那么你为什么没有发现呢？"),
                f.t("李云萧", "你在喵星人抽出试卷的同时进入了办公室，\n他手中的试卷想必也到了你手里。"),
                f.t("李云萧", "那么，这地上的试卷是什么时候掉的呢？"),
                f.t("叶婷", "这……这个……"),
                f.t("李云萧", "还是说，你在说谎？"),
                f.t("叶婷", "对，对不起……"),
                f.t("叶婷", "啊！我应该这么说的，让你误会了。"),
                f.t("叶婷", "我并不是从头到尾一直跟着他的。"),
                f.t("叶婷", "地上的那张试卷，可能是我进入教室之前的事情了。"),
                f.t("李云萧", "也就是说，你没有目击到全部事件？"),
                f.t("叶婷", "没有。"),
                f.t("李云萧", "那好，那请你说说看究竟是怎么一回事。"),
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ01");
        }

    }
}
