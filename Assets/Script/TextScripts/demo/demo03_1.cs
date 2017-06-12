using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo03_1 : TextScript
    {
        public demo03_1(DataManager manager, GameObject root, PanelSwitch ps) : base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*喵星人->关于事件
                f.OpenDialog(0),
                f.t("李云萧", "把至今发生了什么告诉我吧。"),
                f.t("喵星人", "事情是这样的喵，下课后我去了办公室。"),
                f.t("喵星人", "但是，办公室的门是锁着的，所以我就想回教室了。"),
                f.t("喵星人", "临走的时候，我发现到门边上的窗户没有关，所以……"),
                f.t("李云萧", "所以你就翻了窗进来了？"),
                f.t("喵星人", "是。"),
                f.t("苏梦忆", "那你直接说清楚不就行了，\n这样的话，也不会被人误会了。"),
                f.t("喵星人", "可是，你看到了桌上的袋子了没有？"),
                f.t("喵星人", "那里面装的是，昨天刚考完试的试卷。"),
                f.t("李云萧", "考试卷？那又如何？"),
                f.t("喵星人", "我进来的时候，桌上的档案袋是开着的……"),
                f.t("喵星人", "然后，我就想看看里面是什么，就随便抽了一张出来。"),
                f.t("喵星人", "那个时候，突然就有人开门进来了。"),
                f.t("喵星人", "所以状况就是这个样子。"),
                f.t("苏梦忆", "所以说，你为什么要去翻卷子嘛？"),
                f.t("喵星人", "对不起，我就是好奇喵。"),
                f.t("李云萧", "算了，现在后悔也没有意义……"),
                f.t("李云萧", "我有一点比较在意，那个时候大概是什么时间？"),
                f.t("喵星人", "抱歉，我不记得了。"),
                f.t("苏梦忆", "幸好进来的不是老师，如果你被老师看到的话就惨了。"),
                f.t("喵星人", "可是那个人却一口咬定，是我想要偷试卷……"),
                f.t("喵星人", "但是我只是翻窗进来而已，其他的什么都没有做！"),
                f.t("李云萧", "那你告诉她真实情况，不是更好？"),
                f.t("喵星人", "李云萧，你知道我和语文老师的关系不好的！"),
                f.t("喵星人", "这里又没有什么摄像头，老师肯定会觉得我在说谎啊。"),
                f.t("喵星人", "而且这件事闹大了，我可是要被退学的！"),
                f.t("李云萧", "[66ccff]（没这么严重吧……）[-]"),
                f.t("喵星人", "我知道你喜欢推理，求你帮我证明清白！"),
                f.t("李云萧", "等、等等，这么大的事，我怎么帮你？"),
                f.t("喵星人", "你就在现场看看有没有线索啥的。"),
                f.t("李云萧", "你以为这是在玩游戏么？"),
                f.t("喵星人", "拜托了，现在只有你能救我了！"),
                f.t("李云萧", "[66ccff]（你能说的更有诚意些么……）[-]"),
                f.t("李云萧", "诶……好吧，我知道了。")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetDetectJudgeNode("detest1");
        }

    }
}
