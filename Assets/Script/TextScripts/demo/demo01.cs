using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo01 : TextScript
    {
        public demo01(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("? ? ?","李云萧！"),
                f.t("李云萧","[66ccff]（突然的喊声，将我从神游中拉了回来。）[-]"),
                f.SetBackground("corridor"),
                f.t("李云萧","！！！"),
                f.FadeInCharacterSprite(0,"su00_2"),
                f.t("苏梦忆","李云萧，不好了！","","voice_test"),
                f.t("李云萧","是苏梦忆啊？怎么了，什么不好了？"),
                f.t("苏梦忆","喵星人被抓了！","","voice_test2"),
                f.t("李云萧","喵星人被抓了？什么意思？"),
                f.t("苏梦忆","喵星人好像去偷试卷，但被人抓住了。"),
                f.t("李云萧","偷试卷？你没搞错？再怎么说这也太……"),
                f.t("苏梦忆","是真的！总之你快点和我走啦！"),
                f.t("李云萧","[66ccff]（说完，她一把抓起我的手，拽着我走向走廊另一侧。）[-]"),
                f.t("李云萧","喂，你等……等一……"),
                f.FadeoutAll(),
                f.FadeinBackground("office"),
                f.OpenDialog(),
                f.t("李云萧","[66ccff]（这现场真是凌乱啊……）[-]"),
                f.SetCharacterSprite(0,"ch4"),
                f.t("喵星人", "这是办公室，我为什么不能来？"),
                f.ChangeCharacterSprite(0,"ch5"),
                f.t("女生", "门明明是锁好的，你又是怎么进来的？"),
                f.t("李云萧","[66ccff]（刚才站在门外，就能听到里面的声音……）[-]"),
                f.t("李云萧","[66ccff]（果然是喵星人……）[-]"),
                f.ChangeCharacterSprite(0,"ch4"),
                f.t("苏梦忆","喵星人，我带李云萧过来了。"),
                f.ChangeCharacterSprite(0,"ch5"),
                f.t("女生", "你们是谁？赶紧出去！\n这里不能随便进来！"),
                f.t("苏梦忆", "我是高二（3）班的班长，他是我们班的。"),
                f.t("女生", "原来是班长啊，你来的正好，\n这件事要通知你们班主任。"),
                f.t("苏梦忆", "班主任她现在不在学校，所以有什么事情，由我来代为处理。"),
                f.t("女生", "那好吧，但这是很严重的教学事件！"),
                f.t("女生", "这个家伙想要偷放在办公室桌上的试卷！"),
                f.ChangeCharacterSprite(0,"ch4"),
                f.t("喵星人", "我没有！"),
                f.ChangeCharacterSprite(0,"ch5"),
                f.t("女生", "还敢狡辩！"),
                f.ChangeCharacterSprite(0,"ch4"),
                f.t("喵星人", "没有就是没有！"),
                f.t("李云萧","你是三岁小孩么……"),
                f.t("李云萧","[66ccff]（话虽如此，不知道为什么，\n我自内心觉得喵星人是无辜的。）[-]"),
                f.t("李云萧","那么这位同学，你既然这么肯定，一定有着什么证据？"),
                f.ChangeCharacterSprite(0,"ch5"),
                f.t("女生", "那当然！"),
                f.t("李云萧", "在正式处理前，能不能让我问他一些话？"),
                f.t("女生", "可以，就一些。"),
                f.ChangeCharacterSprite(0,"ch4"),
                f.t("喵星人", "又要来了吗，你最擅长的推理。"),
                f.t("李云萧","没有到擅长那种地步，只是游戏玩得多了点。"),
                f.t("李云萧","以及你说“又”，难道我之前已经做了什么吗？"),
                f.t("喵星人", "那是在[ff6600]正式版[-]中的事情了，你想不起来就不要去想了。"),
                f.t("李云萧","[66ccff]（正式版？）[-]"),
                f.ChangeCharacterSprite(0,"ch3"),
                f.t("苏梦忆", "那我又要成为你的助手了。"),
                f.t("李云萧", "[66ccff]（“又”？还有助手是什么鬼？）[-]"),
                f.t("李云萧","不过，能先告诉我你的名字吗？"),
                f.t("苏梦忆","啊！抱歉，抱歉。"),
                f.t("苏梦忆","我是苏梦忆啦，我都忘记你失忆了这设定了。"),
                f.t("李云萧","你知道这件事？"),
                f.t("苏梦忆", "我当然知道啊，因为这是[ff6600]试玩版[-]啊。"),
                f.t("李云萧","[66ccff]（试玩版？）[-]"),
                f.t("苏梦忆", "好啦，喵星人还等着你呢。"),
                f.ChangeCharacterSprite(0,"ch4"),
                f.t("喵星人", "先帮我摆脱困境吧。"),
                f.t("", "[00ff00]接下来将会进入游戏的核心之一，侦探模式。[-]"),
                f.t("", "[00ff00]在这一模式中，你需要充当侦探的角色，去发掘事件的真相。[-]"),
                f.t("", "[00ff00]比如，搜索遗留在现场的证据，收集不同的证人的证言。[-]"),
                f.t("", "[00ff00]可能有些不知所措，那么请先与喵星人对话吧。[-]"),
                f.t("", "[00ff00]由于是试玩版，只开放了部分功能，\n请点击下方的按钮进行操作。[-]"),
                f.PlayBGM("Examine")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetDetectJudgeNode("detest1");
        }

    }
}
