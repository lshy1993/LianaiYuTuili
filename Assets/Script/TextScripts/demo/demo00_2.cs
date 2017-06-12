using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo00_2 : TextScript
    {
        public demo00_2(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.FadeinBackground("qs"),
                f.OpenDialog(),
                f.t("喵星人", "好！这样你就学会了[ff6600]大地图[-]的使用方法了喵。"),
                f.t("喵星人", "因为今天比较特殊，所以就简简单单让你[ff6600]触发[-]事件了喵。"),
                f.t("喵星人", "之后的游戏中，只有满足[ff6600]一定的条件[-]，\n在[ff6600]特定的地点[-]才会发生。"),
                f.t("喵星人", "些地方虽然可以[ff6600]点击[-]，但是不一定有好玩事情喵。"),
                f.t("李云萧", "喂，你一下子说这么多我记不住啊！"),
                f.t("喵星人", "游戏玩法可以在[ff6600]电子校园卡[-]里查看，虽然还没制作好。"),
                //——EDU介绍——
                f.t("喵星人", "最后，我要介绍[ff6600]能力提升[-]模式。"),

                f.t("喵星人", "身为学生，最重要的还是学习，左侧是[ff6600]学习面板[-]。"),
                f.t("喵星人", "选择并[ff6600]点击[-]按钮就可以进行，\n鼠标置于其上方时，会显示该行动的简介。"),
                f.t("喵星人", "看到右侧的课表了吗？这张表上会显示今天将要上的课。"),
                f.t("喵星人", "如果你选择的行动，和当天的课表[ff6600]相符[-]，\n那么学习效率将会大幅[ff6600]提高[-]。"),
                f.t("李云萧", "哦，明白了，只要选择就行了是吗……"),
                f.t("喵星人", "然而当天能做的，第二天不一定能。\n每天的[ff6600]学习项目[-]会发生改变。"),
                f.t("喵星人", "另外，华欣是相当自由的，你完全可以选择自己想做的事情。"),
                f.t("李云萧", "[66ccff]（这么说，我可以[ff6600]不上课[-]？）[-]"),
                f.t("李云萧", "那下面的几个成绩，是什么意思？"),
                f.t("喵星人", "数值越大意味着你的对应能力越高，\n而[ff6600]考试排名[-]则是综合了[ff6600]多科[-]的成绩。"),
                f.t("喵星人", "需要注意的是[ff6600]体力值[-]，体力小于一定数值，\n会导致身心疲惫，严重时就会生病。"),
                f.t("喵星人", "而且，体力越低，行动[ff6600]失败[-]的可能性就越大。"),
                f.t("李云萧", "那我该怎么恢复体力值呢？"),
                f.t("喵星人", "平时的教学安排十分紧凑，对我们来说分秒必争。"),
                f.t("喵星人", "放假的时候，没有教学上的压力，一定要注意休息回复。"),
                f.t("喵星人", "人的时间精力有限，你要合理安排好。"),
                f.t("李云萧", "我明白了……"),
                f.t("喵星人", "好了喵，我的任务结束了，想想该学点什么吧。"),
                f.t("李云萧", "诶？容我想想……"),
                f.t("李云萧", "[66ccff]（那么，今天要做些什么呢？）[-]")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEduNode();
        }

    }
}
