using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TD21_1_03 : TextScript
    {
        public TD21_1_03(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //——后台的工作人员——
                f.OpenDialog(),
                f.t("李云萧","这次你们带了多少名工作人员？"),
                f.t("蔡助理","嗯，应该有十几个吧。"),
                f.t("李云萧","十几个？我不记得有见过这么多人啊。"),
                f.t("蔡经理","其他人都在舞台和外场准备工作，不是所有工作人员能进入演员准备区的。"),
                f.t("蔡经理","除了我以外，化妆助理小陈，负责服装的小郭能进到这里。"),
                f.t("李云萧","他们两个人呢？"),
                f.t("蔡经理","一个应该在小雪的休息室里，另一个在最里面的服装室。"),
                f.t("李云萧","（看来，等会得去那边了解下情况……）"),
                f.t("李云萧","说起来，这次，你们借用了多少个房间？"),
                f.t("蔡经理","休息区的话，有两个房间，还有个的服装间。"),
                f.t("李云萧","唔……最好有个示意图啥的……"),
                f.t("喵星人","给，我刚刚画下来的。"),
                f.t("李云萧","你什么时候画的？"),
                f.t("喵星人","在你回教室的时候。"),
                f.t("李云萧","你的速度还真快啊……"),
                f.t("李云萧","还是说你一开始就知道会出事？"),
                f.t("喵星人","没没，怎么会呢，别怀疑我，我是清白的。"),
                f.t("李云萧","那么，根据示意图，能进出的地方，总共有2个。"),
                f.t("李云萧","一个连接着舞台，而另一个就是这里的门。"),
                f.t("喵星人","这样的话，谁都可以进来啊，外面就是走廊。"),
                f.t("蔡经理","但是，那扇门不是一直锁着的吗？"),
                f.t("蔡经理","至少，我们来的时候就一直是锁着的。"),
                f.t("李云萧","唔……"),
                f.t("李云萧","（好像他们并不知道我们是从那扇门进来的……）"),
                f.t("喵星人","那扇门，我可是锁好的，应该没有其他人能进。"),
                f.t("蔡经理","舞台那边有我们的人看着，并没有什么可疑人进入。"),
                f.t("李云萧","会不会是你们没有注意呢？"),
                f.t("蔡经理","不会的，如果没有证件的人进来的话，一定会被赶出去的。"),
                f.t("李云萧","这样啊……。"),
                f.FadeoutAll()
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetDetectJudgeNode("T21_01");
        }

    }
}
