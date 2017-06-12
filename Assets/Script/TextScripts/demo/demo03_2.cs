using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo03_2 : TextScript
    {
        public demo03_2(DataManager manager, GameObject root, PanelSwitch ps) : base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*喵星人->现场情况
                f.OpenDialog(0),
                f.t("李云萧", "说起来，这里的玻璃窗碎了。"),
                f.t("喵星人", "我翻窗进来的时候，就已经是这样了。"),
                f.t("李云萧", "你进来的时候，还发现了什么其他情况？"),
                f.t("喵星人", "除了地上掉着张纸以外，没有别的了。"),
                f.t("苏梦忆", "地上的纸？"),
                f.t("喵星人", "就是桌子旁边的那张白色的，我想应该是考试的试卷。"),
                f.t("喵星人", "不过我先说好，地上那张我可没有碰过。"),
                f.t("李云萧", "那么，当时办公桌上的情况呢？"),
                f.t("喵星人", "桌子的中间就放着那个被打开的袋子。"),
                f.t("喵星人", "其他的地方，和平时老师的桌面一样。"),
                f.t("李云萧", "我懂了，是这么一回事啊。"),
                f.t("苏梦忆", "什么意思？"),
                f.t("李云萧", "大概是有谁先你一步，打开了试卷袋，偷拿出了试卷。"),
                f.t("李云萧", "那时候可能发生了什么意外，\n那人并没来得及把试卷放回原位。"),
                f.t("苏梦忆", "那我去跟叶婷解释一下，这样就可以了吧？"),
                f.t("李云萧", "等下，这说到底是我的猜测，没有依据。"),
                f.t("李云萧", "而且，这个推测的大前提是，你没有撒谎。"),
                f.t("苏梦忆", "难道你不相信喵星人吗？"),
                f.t("喵星人", "真的不是我干的喵！"),
                f.t("李云萧", "我不是这个意思……"),
                f.t("李云萧", "只是现在没有任何证据，可以证明你说的就是事实。"),
                f.t("李云萧", "她完全可以认为，你在撒谎。"),
                f.t("喵星人", "那该怎么办啊，李云萧？"),
                f.t("李云萧", "情报还不够，再让我搜集一下这里的信息。"),
                f.t("喵星人", "我相信你！当然还有苏梦忆！"),
                f.t("苏梦忆", "谢谢你信任我，可是我什么也不会啊。"),
                f.t("喵星人", "有李云萧在呢。"),
                f.t("李云萧", "既然有人来过这里，又匆忙离开，那么肯定留下了什么线索。"),
                f.t("李云萧", "即使找不到能证明你清白的东西，也应该能发现点什么的！"),
                f.t("苏梦忆", "嗯好，那我们开始吧！")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetDetectJudgeNode("detest1");
        }

    }
}
