﻿using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class S0003 : TextScript
    {
        public S0003(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——背景 后教室——
                f.FadeinBackground("classroom"),
                f.OpenDialog(),
                f.t("李云萧", "[66ccff]（总算完成今天的学习了……）[-]"),
                f.t("李云萧", "[66ccff]（不愧是重点中学，上课的方式就不一样。）[-]"),
                //f.t("李云萧", "（还好能跟得上老师的节奏……）"),
                f.t("喵星人", "啊~终于下课了！"),
                f.t("李云萧", "[66ccff]（喵星人非常懒散地伸了个懒腰……）[-]"),
                f.t("李云萧", "第一天就这么过去了，话说，接下来没课了？"),
                f.t("喵星人", "对啊，接下来就是自由活动时间喵。"),
                f.t("李云萧", "等会，这才几点，这么早就下课了？"),
                f.t("喵星人", "你刚来，可能不了解这一点。"),
                f.t("喵星人", "这就是枫溪的特色，不像其他学校那样，一整天都在学习。"),
                f.t("李云萧", "不学习，还有别的事情可以做吗？"),
                f.t("喵星人", "一般来说，这个时间，大家都会去参加各自的社团活动。"),
                f.t("喵星人", "你应该还没有加入什么社团，对吧？"),
                f.t("李云萧", "废话，我都不知道有这种事。"),
                f.t("李云萧", "等等！枫溪有学生社团？"),
                f.t("喵星人", "怎么没有？电影社、文学社、足球社、计算机社，我能给你派出一堆来。"),
                f.t("李云萧", "居然有这么多，完全没想到……"),
                f.t("？？？", "你们是想创立社团？"),
                f.t("李云萧", "[66ccff]（眼前突然多了少女的呼喊声。）[-]"),
                f.t("李云萧", "班、班长？"),
                f.t("陆菲菲", "别叫我班长了，我叫陆菲菲，你是新来的李云萧是吧？"),
                f.t("李云萧", "是的。"),
                f.t("陆菲菲", "那么，继续刚才的话题吧。"),
                f.t("李云萧", "[66ccff]（被她这么一打断，我和喵星人都忘记该说什么了。）[-]"),
                f.t("喵星人", "说到创立社团，其中一个条件是达到[ff6600]一定[-]的人数。"),
                f.t("喵星人", "所以，枫溪虽然有众多社团，自然不是什么都有。"),
                f.t("陆菲菲", "对了，这个学期再过不久，会开始[66ccff]百团大战[-]。"),
                f.t("李云萧", "什么？百团大战？"),
                f.t("陆菲菲", "当然这是夸张的说法。"),
                f.t("喵星人", "再过不久，新的一轮社团招新就要开始了。"),
                f.t("李云萧", "哦？那我得考虑一下加入什么社团。"),
                f.t("李云萧", "对了，你接下去要参加什么社团活动？"),
                f.t("喵星人", "睡觉，我可是睡觉社的副社长。"),
                f.t("李云萧", "…………你是认真的？"),
                f.t("喵星人", "开玩笑，开玩笑，跟我来。"),
                //——地图选择介绍——
                f.t("李云萧", "这不是学校的地图吗？带我来这干什么？"),
                f.t("喵星人", "下课后的时间是学生的自由行动时间，在这段时间里，你可以去学校的各个地方。"),
                f.t("喵星人", "根据时间、地点的不同，你会遇到不同的事件。"),
                f.t("李云萧", "是什么事件？"),
                f.t("喵星人", "各种各样的都有可能发生，有时也会间接改变你的能力值。"),
                f.t("喵星人", "点击不同的地方按钮，就可以进入那个的地点进而触发事件了。"),
                f.t("李云萧", "哦……"),
                f.t("喵星人", "甚至，你可以选择回寝室休息，体力的回复也是很重要的。"),
                f.t("李云萧", "我可不会让自己生病的。"),
                f.t("喵星人", "当然，你也可以选择回“教学楼”，如果你愿意的话。"),
                f.t("喵星人", "那样的话，就进入了之前的“能力提升”模式，可以利用这个机会恶补一下。"),
                f.t("喵星人", "注意一心不能两用，如果选择了继续学习的话，就不能去其他地方闲逛了。"),
                f.t("李云萧", "那这个“叹号”又是什么意思？"),
                f.t("喵星人", "有“叹号”显示的地点，将会发生重要的事情，但具体是怎么样的，我就不知道了。"),
                f.t("李云萧", "那我不去会怎么样？"),
                f.t("喵星人", "不去的话就再也没有了，总之，尽可能不要错过喵。"),
                f.t("李云萧", "原来如此。"),
                f.t("喵星人", "我该去社团了，先走一步喵。"),
                f.t("李云萧", "嗯，我也考虑下该去哪里好呢。"),
                f.t("喵星人", "哦，对了，忘记告诉你了喵。"),
                f.t("李云萧", "又、又怎么了？"),
                f.t("喵星人", "今天上午，为了让你安心学习，没让你逃课。"),
                f.t("喵星人", "接下去的每一天，你都会有1次能力提升和1次自由行动的时间。"),
                f.t("喵星人", "是选择参加活动，还是继续认真学习，全凭你自己的心。"),
                f.t("喵星人", "你今日所做的选择，就是你明日的基石。"),
                f.t("喵星人", "那我先撤去社团了，拜~"),
                f.t("李云萧", "（喵星人的这番话好像有颇有深意。）"),
                f.t("李云萧", "等等、你不是睡觉社的吗？"),
                f.t("李云萧", "……已经走远了，算了，考虑下该怎么办吧。"),
                //——背景 消失——
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEndTurnNode();
        }

    }
}
