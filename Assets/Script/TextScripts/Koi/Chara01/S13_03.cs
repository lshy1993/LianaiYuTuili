﻿using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class S13_03 : TextScript
    {
        public S13_03(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("喵星人","你说的是？"),
                f.t("李云萧","就是刚才坐在我身边的那位。"),
                f.t("喵星人","啊……那个啊，我想应该是新生……"),
                f.t("李云萧","少装蒜，如果是新生的话，为什么把整理好的文档放在那里？"),
                f.t("李云萧","其他的学生会成员，人手一叠资料，唯有那个位置单独准备了。"),
                f.t("喵星人","那，那是……"),
                f.t("李云萧","从进场开始，我一直以为我被当成了学生会的一员。"),
                f.t("李云萧","那么，其他人看来，我是什么身份呢？"),
                f.t("喵星人","……"),
                f.t("李云萧","但却是我搞错了，事实上，我被当成了新生。"),
                f.t("李云萧","几个部长对于我的提问非常自然地进行了解答。"),
                f.t("喵星人","毕竟我和大家说的是从你位置开始都是新生。"),
                f.t("李云萧","那么问题是，那时候是不是还有另一位被“误会”的学生呢？"),
                f.t("喵星人","……"),
                f.t("李云萧","那个时候，我右手边的座位十分特殊，明明是属于新生的座位。"),
                f.t("李云萧","但是，桌上却同时摆放着资料，而且更加精细。"),
                f.t("李云萧","新生是不可能需要这些资料的，那么，那位女生的身份应该就很明朗了。"),
                f.t("李云萧","是真正的主席团记录员！"),
                f.t("喵星人","喵！"),
                f.t("李云萧","当时，有着这么一位伪装成新生的主席团成员，在私下里观察着所有新生！"),
                f.t("李云萧","然而，其他的新生并没有察觉到这一点，也以为她是新生。"),
                f.t("喵星人","喵喵！"),
                f.t("李云萧","怎么样，我说对了吗？"),
                f.t("喵星人","嗯，对了一半。"),
                f.t("李云萧","只有一半？"),
                f.t("喵星人","不是的，你说的都是正确的，只是那个女生不是什么记录员。"),
                f.t("李云萧","啊……？"),
                f.t("喵星人","她是我们的会长。"),
                f.t("李云萧","你说什么？！"),
                f.t("喵星人","这个计划是会长她自己想的，我们只是帮助她而已。"),
                f.t("李云萧","她这么做的目的是什么？"),
                f.t("喵星人","不知道，不过我猜是她想从新生里找出新的接班人吧。"),
                f.t("李云萧","这么做能找到么？"),
                f.t("喵星人","大概只有会长她自己知道了。"),
                f.t("喵星人","今天的说明会还没有结束，你要是有兴趣的话可以去单独询问。"),
                f.t("李云萧","算了，并没有这个兴趣。"),
                f.t("喵星人","我还有些收尾工作要做，你先回去吧！"),
                f.t("李云萧","那我就先走了!"),
                f.FadeoutAll()
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEndTurnNode();
        }

    }
}