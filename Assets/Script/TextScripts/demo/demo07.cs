using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo07 : TextScript
    {
        public demo07(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.StopBGM(),
                f.OpenDialog(),
                f.ChangeCharacterSprite(0,"ch5"),
                f.t("叶婷", "怎么样，证据确凿！你没话好讲了。"),
                f.t("李云萧", "[66ccff]（只能用下这招了……）[-]"),
                f.t("李云萧", "等一下叶婷，你自己也有办公室的钥匙。"),
                f.t("叶婷", "那又如何？"),
                f.t("李云萧", "你也有可能偷偷打开密封袋，拿到试卷。"),
                f.t("李云萧", "而你目击到喵星人这一点，也有可能是在说谎。"),
                f.t("叶婷", "哼，没想到你为了给他开脱，往我头上扣……"),
                f.t("李云萧", "……"),
                f.t("叶婷", "但是很遗憾，那是不可能的。"),
                f.t("李云萧", "为什么？"),
                f.t("叶婷", "因为不止我一个人看到了。"),
                f.t("李云萧", "什么意思？"),
                f.t("叶婷", "意思就是不止我一个人看到了。"),
                f.t("叶婷", "门外还有一个人，他跟我一起看到的。"),
                f.t("李云萧", "不会吧……"),
                f.t("叶婷", "你进来吧！"),
                f.t("李云萧", "[66ccff]（究竟什么时候在这里的？）[-]"),
                f.ChangeCharacterSprite(0,"ch2"),
                f.t("? ? ?", "我、我也看到了。"),
                f.t("李云萧", "你是？"),
                f.t("戚海超", "我是高一（6）的戚海超。"),
                f.t("苏梦忆", "咦？高一的学生，你怎么会在这里？"),
                f.t("苏梦忆", "今天所有的语文老师都不在，办公室也关了……"),
                f.t("戚海超", "这个……我来散步的……"),
                f.t("李云萧", "散步？"),
                f.ChangeCharacterSprite(0,"ch5"),
                f.t("叶婷", "这不重要，重要的是他也看到了，没错吧？"),
                f.ChangeCharacterSprite(0,"ch2"),
                f.t("戚海超", "对，我和她一起看到了。"),
                f.t("李云萧", "喵星人，我怎么没听你说过？"),
                f.t("喵星人", "我也不知道啊。"),
                f.t("李云萧", "对了，你怎么满头大汗？"),
                f.t("戚海超", "我之前刚踢完球，从操场跑过来的。"),
                f.t("李云萧", "[66ccff]（算了，他应该也是看错了的……）[-]"),
                f.t("李云萧", "既然这样，也请把你看到的，告诉我吧。")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ02");
        }

    }
}
