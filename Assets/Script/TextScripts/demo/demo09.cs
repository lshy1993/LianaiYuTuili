using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo09 : TextScript
    {
        public demo09(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.StopBGM(),
                f.OpenDialog(),
                f.t("李云萧", "刚才就觉得不对劲了。"),
                f.t("戚海超", "哪、哪里？"),
                f.t("李云萧","你在发现窗被足球砸碎前，是在对面的走廊上吧？"),
                f.t("戚海超", "是、是的，我在吹风。"),
                f.t("李云萧", "那就奇怪了，在那边是看不到这边的办公室，因为有阻挡。"),
                f.t("李云萧", "就算角度正好，你能看到办公室，也是不可能看到办公室里面的窗户！"),
                f.t("戚海超", "哎！"),
                f.t("李云萧", "请不要来什么“哎”。"),
                f.t("戚海超", "真、真是这样子的吗？"),
                f.t("李云萧", "你不相信的话，我们走过去，站在那边朝这里看。"),
                f.t("李云萧", "到底能不能看清这里，一下子就能弄清楚了。"),
                f.t("戚海超", "不、不必了……"),
                f.t("李云萧", "那么，你是怎么知道是足球打碎了窗户呢？"),
                f.t("戚海超", "这个，那个，额……对不起！"),
                f.t("戚海超", "其实我不是在这里看到的，而是在操场。"),
                f.t("李云萧", "操场？"),
                f.t("李云萧", "足球场就在教学楼的边上，这个办公室就能看得到。"),
                f.t("李云萧", "看来有必要去操场调查看看。"),
                f.ChangeCharacterSprite(0,"ch5"),
                f.t("叶婷", "有这个必要吗？他不是和我一起目击了，这就够了！"),
                f.t("李云萧", "嗯？多调查一点不好吗？"),
                f.t("李云萧", "还是说，操场里有什么不好的东西？"),
                f.t("叶婷", "没、没有。"),
                f.t("叶婷", "调查可以，但他得跟我待在一起，防止他销毁证据。"),
                f.t("李云萧", "行，你们就待在这里，锁好办公室的门。"),
                f.t("李云萧", "不要让任何人靠近，也不要别的同学发现。"),
                f.ChangeCharacterSprite(0,"ch4"),
                f.t("李云萧", "[66ccff]（我凑近喵星人的耳边，轻声地说道。）[-]"),
                f.t("李云萧", "喵星人，你看好他，不要让他做小动作。"),
                f.t("喵星人", "哦，我不会让任何人进现场的。"),
                f.t("李云萧", "嗯，交给你了。"),
                f.t("", "[00ff00]现在可以去操场了[-]")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetDetectJudgeNode("detest3");
        }

    }
}
