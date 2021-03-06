﻿using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo0_4 : TextScript
    {
        public demo0_4(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //——背景 讲台视角的教室——
                f.FadeinBackground("classroom"),
                f.TimeSwitch("8月31日 上午", "高二（3）班 教室"),
                f.ShowChapter("Chapter 0-1"),
                f.OpenDialog(),
                //——无立绘 头像——
                f.t("李云萧","[66ccff]（都是陌生的脸……）[-]"),
                f.t("女生","他是谁啊？"),
                f.t("女生","不知道，没见过。"),
                f.t("男生","不会是转校生吧？"),
                f.t("男生","但是这种时候还有转校吗？"),
                f.t("? ? ?","大家安静一下——"),
                f.t("李云萧","[66ccff]（不从是哪里传来了声音，整个教室瞬间静了下来。）[-]"),
                f.FadeInCharacterSprite(0, "ch7"),
                f.t("郭老师","同学们，从明天开始，新的学期就到来了。"),
                f.t("郭老师","在开始今天的班会前，有一个消息要告诉大家。"),
                f.t("郭老师","从今天开始，我们班将多一名新的成员。"),
                f.t("郭老师","那么，接下来由他来自我介绍一下。"),
                f.t("李云萧","[66ccff]（轮到我了吗……）[-]"),
                f.t("李云萧","各位同学，大家好，我的名字叫李云萧。"),
                f.t("李云萧","因为家里的关系，从其他学校转到了这里。"),
                f.t("李云萧","我的爱好是游戏与推理，希望能在接下来的时间里，\n和大家成为朋友，一起度过高中时光，谢谢。"),
                f.t("","掌声——"),
                //——SE 掌声——
                f.t("郭老师","非常感谢李云萧同学，也希望同学们能友好相处。"),
                f.t("郭老师","教室只剩下最后一排空着了，要么你坐靠窗的那个位置吧。"),
                f.t("李云萧","[66ccff]（靠窗的座位，也不错……）[-]"),
                //——背景 后排视角的教室——
                //f.TransBackground("classroom"),
                f.t("郭老师","这个暑假，同学们…………"),
                f.FadeoutCharacterSprite(0),
                f.t("李云萧","[66ccff]（在简短的开场白之后……）[-]"),
                f.t("李云萧","[66ccff]（老师开始对上个学期进行了总结。）[-]"),
                f.StopBGM(),
                f.t("","…………"),
                f.t("","……"),
                f.PlayBGM("people1"),
                f.t("？？？","哟，新来的——"),
                f.t("李云萧","[66ccff]（果然不管在哪个学校……）[-]"),
                f.t("？？？","喂，那个谁——"),
                f.t("李云萧","[66ccff]（新学期的开场白都如此地相似。）[-]"),
                f.t("？？？","喂！(#`O′)"),
                f.t("李云萧","嗯？叫我吗？"),
                f.FadeInCharacterSprite(0,"ch4"),
                f.t("？？？","不叫你叫谁啊喵？"),
                f.t("李云萧","哦，怎么了？"),
                f.t("李云萧","[66ccff]（他刚才说了“喵”，我没听错吧……）[-]"),
                f.t("喵星人","你好，我叫喵星人。"),
                f.t("李云萧","喵星人？好奇怪的名字……"),
                f.t("喵星人","你怎么连你也这么说，和我当初刚进来的时候一样。"),
                f.t("苗星任","我姓苗，苗疆的苗，天上的星，重任的任。"),
                f.t("李云萧","是这样啊，抱歉啊，因为……"),
                f.t("喵星人","和“喵星人”谐音，所以大家都叫我喵星人，我还蛮喜欢的喵。"),
                f.t("李云萧","……"),
                f.t("喵星人","怎么愣住了喵？"),
                f.t("李云萧","你这个算是恶意卖萌吗？"),
                f.t("喵星人","你说呢？╮(╯▽╰)╭"),
                f.t("李云萧","[66ccff]（颜文字都出来了啊，喂！）[-]"),
                f.t("喵星人","你才刚来，可能对枫溪不太熟悉。"),
                f.t("李云萧","我只知道是有名的重点中学，其他的事情就不太清楚了。"),
                f.t("喵星人","说对了一半，枫溪的确是市里的一级重点高中……"),
                f.t("喵星人","但是我们枫溪，和其他的重点高中还是有区别的。"),
                f.t("李云萧","哦？有什么区别？"),
                f.PauseBGM(),
                f.TransCharacterSprite(0,"ch7"),
                f.t("郭老师","苗星任！"),
                f.t("喵星人","！！","ch4"),
                f.t("郭老师","你的暑假作业做完了没有？！"),
                f.t("喵星人","做、做完了！！！","ch4"),
                f.t("郭老师","很好，等会由你带领班上的男生去领这学期的教材。"),
                f.t("郭老师","好了，希望同学们回去好好准备下，明天开始正式上课。"),
                f.FadeoutCharacterSprite(0),
                f.UnpauseBGM(0.5f),
                f.t("喵星人","呼……差点被发现了……","ch4"),
                f.t("李云萧","让你开小差……"),
                f.t("喵星人","还不是因为和你说话！","ch4"),
                f.t("李云萧", "[66ccff]（明明是你转过头来和我讲话的！）[-]"),
                f.t("喵星人","等老师讲完吧！","ch4"),
                f.FadeoutBackground(),
                f.t("","…………"),
                f.t("","……"),
                f.FadeinBackground("classroom"),
                f.FadeInCharacterSprite(0,"ch7"),
                f.t("郭老师","那么，同学们，今天的班会就到这里了。"),
                f.t("郭老师","请各位好好休息，明天开始新的学期。"),
                f.FadeoutCharacterSprite(0),
                f.t("喵星人","终于，结束了啊！！","ch4"),
                f.t("李云萧", "你站起来干什么？"),
                //——CG 喵星人振臂高呼——
                f.FadeInCharacterSprite(0,"ch4"),
                f.t("喵星人","咳咳，男同胞们，跟我一起去领书！！"),
                f.t("男生","又是我们几个啊……诶，走啦！走啦！"),
                f.t("李云萧","嚯，想不到，你还蛮有号召力的。"),
                f.t("喵星人","还有李云萧，你也和我们一起去！"),
                f.t("李云萧","我也要去？"),
                f.t("喵星人","当然！你已经是我们班级的人了，是男的都要去！"),
                f.t("李云萧","知道啦！"),
                f.t("李云萧","[66ccff]（于是，我跟随着喵星人，离开了教室……）[-]"),
                //f.t("李云萧","[66ccff]（我朝着这个新的大家庭，迈出了第一步。）[-]"),
                f.FadeoutAll(),
                f.Wait(0.5f),
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.FindTextScript("demo00_1");
        }

    }
}
