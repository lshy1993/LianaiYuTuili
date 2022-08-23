﻿using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class S0001_2 : TextScript
    {
        public S0001_2(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——背景：教材领取处——
                f.FadeinBackground("gate"),
                f.OpenDialog(),
                f.t("李云萧", "[66ccff]（果然还是男生更容易打成一片……）[-]"),
                f.t("李云萧", "[66ccff]（不过，一路上也被他们问了各种问题……）[-]"),
                f.t("喵星人", "我说过我们班的男生很好说话的，没错吧喵？"),
                f.t("李云萧", "你什么时候说过这句话？"),
                f.t("喵星人", "对了，你这身衣服不会是之前学校的？"),
                f.t("李云萧", "校服。"),
                f.t("喵星人", "原来还有这种校服啊……"),
                f.t("李云萧", "和你们穿的完全不是一个次元的衣服。"),
                f.t("喵星人", "嘿嘿，这就是之前说的第一个“不同”了。"),
                f.t("喵星人", "华欣的校服可以说是与众不同，毕竟清了专业设计师。"),
                f.t("李云萧", "我穿的这件，真的只是普通的运动服而已。"),
                f.t("喵星人", "可就算如此，你还是穿着过来了喵，为什么不丢掉它呢？"),
                f.t("李云萧", "因为，这是我的……"),
                f.t("喵星人", "好啦不管这个，我们学校女生的校服你看了吗？"),
                f.t("李云萧", "没有……再说，谁会一上来就盯着女生看啊……"),
                f.t("喵星人", "女生的校服就是学校里最靓丽的风景线了喵！"),
                f.t("李云萧", "啥……"),
                f.t("喵星人", "你别不信，等明天的开学升旗仪式吧！"),
                f.t("李云萧", "我也没说不信啊……"),
                f.t("男生", "喵星人，还有两摞书，交给你了！我们几个先撤了！"),
                f.t("喵星人", "哦！知道了！"),
                f.t("喵星人", "……"),
                f.t("喵星人", "唔……"),
                f.t("李云萧", "怎么了？"),
                f.t("喵星人", "我擦咧！那帮人居然留给我最重的书！"),
                f.t("李云萧", "算了吧，我帮你搬一半吧！"),
                f.t("喵星人", "唔……"),
                f.t("李云萧", "有那么重吗？我试试……"),
                f.t("李云萧", "……"),
                f.t("李云萧", "[66ccff]（传达到我手臂的重量，令我的手臂开始发麻。）[-]"),
                f.t("李云萧", "哇——好重啊！"),
                f.t("喵星人", "等我——回去——看我不弄死他们！"),
                f.TransBackground("classroom"),
                //——背景 后排视角教室——
                f.t("喵星人","呼——终于忙完了喵！"),
                f.t("李云萧", "[66ccff]（将新的教材搬回教室后，我们把课本分发给了每一位同学。）[-]"),
                f.t("李云萧", "说起来，喵星人，郭老师呢？"),
                f.t("喵星人", "工作做完了，估计已经回家去了。"),
                f.t("李云萧", "这么早就回去了！？"),
                f.t("李云萧", "[66ccff]（这样的老师没问题吗？）[-]"),
                f.t("？？？", "各位！"),
                f.t("李云萧", "[66ccff]（突然传来的声音将我的思绪打断了……）[-]"),
                //——CG 班长讲台演讲——
                f.t("？？？","各位！大家都拿到课本了吗？"),
                f.t("女生","应该都发到手里了。"),
                f.t("？？？","剩下的男生都回来了，现在就开始新一轮的班委竞选。"),
                f.t("？？？","每个人都可以参加竞选，上讲台进行简单的宣言……"),
                f.t("喵星人", "在盯着看什么呢？"),
                f.t("李云萧", "没、没什么……"),
                f.t("喵星人", "啊~原来是班长啊~"),
                f.t("李云萧", "班、班长？"),
                f.t("喵星人","喏，站在讲台上讲话的就是了。"),
                f.t("喵星人","怎么？被她迷住了？"),
                f.t("李云萧","怎么可能……"),
                f.t("李云萧", "[66ccff]（虽然真的有种令人心静的清秀……）[-]"),
                f.t("喵星人","不过不承认，班长也算是一等一的美人了。"),
                f.t("李云萧","被你这么一说，好像……还行……"),
                f.t("喵星人","不过，我劝你一句，想追求她是基本不可能的。"),
                f.t("李云萧","哈？为什么？"),
                f.t("喵星人","嗯！你就当我什么也没说吧？"),
                f.t("喵星人", "现在在进行班委的选举，班长、学习委员、劳动委员、体艺委员，有没有兴趣喵？"),
                f.t("李云萧", "不知道，你呢？"),
                f.t("喵星人", "我没什么兴趣，要么，你去试一试？"),
                f.t("李云萧", "不用了吧，我和其他同学还不太熟悉，就算去了也选不上吧。"),
                f.t("喵星人", "那得赶紧和其他的同学交流交流，尤其是女生。"),
                f.t("李云萧", "知道啦……"),
                f.t("李云萧","喵星人，说起来，我旁边的这个座位是谁的？"),
                f.t("喵星人","就是班长她的喵。"),
                f.t("李云萧","不会吧……"),
                f.FadeoutAllChara(),
                f.CloseDialog(),
                f.TransBackground("sky_day"),
                f.Wait(0.3f),
                f.OpenDialog(),
                f.t("李云萧", "[66ccff]（我并没有心思去听每个人的上台发言……）[-]"),
                f.t("李云萧", "[66ccff]（夏日的午后总是这么悠闲……）[-]"),
                f.t("李云萧", "[66ccff]（悠闲得让人不自觉地发起呆来……）[-]"),
                f.t("喵星人", "李云萧！李云萧！"),
                f.TransBackground("classroom"),
                f.FadeInCharacterSprite(0,"ch4"),
                f.t("李云萧", "欸？已经结束了？"),
                f.t("喵星人", "发言早就结束了，现在在投票呢！"),
                f.t("李云萧", "哦哦。"),
                //班长立绘
                f.t("班长", "你们两个！"),
                f.t("喵星人", "糟糕……"),
                f.t("班长", "投票投好了吗？"),
                f.t("喵星人", "这个……"),
                f.t("班长", "李云萧刚来也就算了，喵星人，你怎么每次都这么慢？"),
                f.t("喵星人", "我不是在教李云萧怎么投票嘛……"),
                f.t("喵星人", "你看，候选人的名字在黑板上呢。"),
                f.t("班长", "那么赶紧决定！"),
                f.t("喵星人", "了解！"),
                f.t("李云萧", "[66ccff]（于是，我便随便写了几个名字上去。）[-]"),
                f.t("李云萧", "这样就可以了吧。"),
                f.t("班长", "OK."),
                f.t("喵星人", "我说，你从刚开始就精神恍惚……"),
                f.t("喵星人", "你不会是真的看上班长了吧？"),
                f.t("李云萧", "你在乱说什么……"),
                f.t("喵星人", "嘿嘿~你就当我什么也没说吧，我们走！"),
                f.t("李云萧", "等等？这不是还没统计出结果吗？"),
                f.t("喵星人", "对于我们这些不参选的人来说，已经结束了。"),
                f.t("喵星人", "再说，班长她也说了解散了。"),
                f.t("李云萧", "是、是吗？"),
                f.t("喵星人", "走啦！我还要带你逛校园喵！"),
                f.t("",""),
                //——背景 校园地图——
                //*这里进入地图说明 人物采用Q版
                f.t("喵星人","接下来，给你介绍一下我们的校园喵。"),
                f.t("喵星人","首先，枫溪外国语学校是个完全中学。"),
                f.t("李云萧","完全中学？"),
                f.t("喵星人","你可能没有注意到，枫溪是分为高中部与初中部的。"),
                f.t("喵星人","这就是我们所在的[ff6600]1号教学楼[-]，整个高中部都在这里。"),
                f.t("喵星人","然后与之相连的，则是[ff6600]实验楼[-]了，全校学生的实验都在这里进行。"),
                f.t("李云萧","哦，那旁边的两个教学楼是？"),
                f.t("喵星人","左边那个是初中部所在的2号楼，右边的那个是有很多空教室的3号楼。"),
                f.t("李云萧","你是从初中部升上来的吗？"),
                f.t("喵星人","是的，不过我上初中的时候，校区还不在这里。"),
                f.t("喵星人","再过去来就是操场了，平时的体育课都在那里上。"),
                f.t("喵星人","体育馆的边上就是音乐馆，音乐课美术课，得跑去那边上。"),
                f.t("李云萧","好远啊……"),
                f.t("喵星人","所以要跑着过去喵！"),
                f.t("喵星人","以及，这里是我们平时住的地方，2号寝室楼是男生楼。"),
                f.t("喵星人","然后这里是食堂，从寝室出来要走一大段路。"),
                f.t("喵星人","最后，这里就是我最不喜欢去的图书馆了。"),
                f.t("喵星人","怎么样？记住了吗？"),
                f.t("李云萧","完全记不住……"),
                f.t("喵星人","没关系，只要把[ff6600]光标[-]移到按钮上，就能看到详细情况。"),
                f.t("喵星人","好了！讲完了，我们去吧。"),
                f.t("李云萧","去哪？？"),
                f.t("喵星人","2号寝室楼，别搞错了。")
                //——背景 消失——
            };
        }

        public override GameNode NextNode()
        {
            //return base.NextNode();
            Finish();
            return nodeFactory.GetMapNode();
        }

    }
}