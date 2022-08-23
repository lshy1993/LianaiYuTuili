﻿using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class T21_001 : TextScript
    {
        public T21_001(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //——背景 后台走廊准备室——
                f.OpenDialog(),
                f.t("喵星人","我、我带他回来了。"),
                //——立绘 蔡助理-哀伤——
                f.t("蔡助理","……"),
                f.t("李云萧","这是什么情况？"),
                //——立绘 王经纪人-哀伤——
                f.t("？？？","你是李云萧同学是吧？"),
                f.t("李云萧","是。不好意思，请问您是？"),
                f.t("陈嘉敏","你好，我是陈嘉敏，吹雪组合的经纪人。"),
                f.t("李云萧","您好。"),
                f.t("陈嘉敏","非常抱歉耽误你，事出紧急，长话短说。"),
                f.t("陈嘉敏","西门吹的东西被人拿走了。"),
                f.t("陈嘉敏","那孩子，现在没恢复过来，躲在房间里不肯出来。"),
                f.t("陈嘉敏","我听说，李云萧同学在事件调查方面非常有能力。"),
                f.t("李云萧","请等一下，请问你们是听谁说的？"),
                f.t("陈嘉敏","我认识你们的班主任，我和她是大学同学，是她说的。"),
                f.t("李云萧","（这也太巧合了吧……）"),
                f.t("陈嘉敏","虽然这样做会占用你的私人时间，但是，迫不得已……"),
                f.t("陈嘉敏","所以想以私人的名义，请你帮我们。"),
                f.t("陈嘉敏","拜托了！！"),
                f.t("李云萧","这……"),
                f.t("蔡助理","我也拜托了！"),
                f.t("李云萧","我理解了你的说法，但是……"),
                f.t("李云萧","如果是贵重物品丢失的话，还是找学校为好……"),
                f.t("陈嘉敏","我知道，但是这次情况特殊，我不希望校方介入。"),
                f.t("李云萧","（特殊？）"),
                f.t("陈嘉敏","还有，其实，并不是什么贵重的物品……"),
                f.t("李云萧","那是？"),
                f.t("蔡助理","是西门吹的发卡。"),
                f.t("李云萧","发卡？"),
                f.t("陈嘉敏","你别看只是个发卡，它对西门吹很重要。"),
                f.t("陈嘉敏","没错，那孩子每次外出表演都会带着它。"),
                f.t("陈嘉敏","她以前和我说过，没有了它，就不能安心上台表演。"),
                f.t("李云萧","（有这么重要吗……）"),
                f.t("陈嘉敏","我也劝过好几次，但是她始终不肯放弃那个东西。"),
                f.t("李云萧","既然只是个发卡，应该不会有人偷才对啊。"),
                f.t("喵星人","但是就结论而言，东西最后依然不见了。"),
                f.t("李云萧","大概是谁拿走了吧……等等！"),
                f.t("李云萧","这么一说，岂不是只有知道其价值的人？"),
                f.t("喵星人","果然和我想得一样。"),
                f.t("李云萧","说起来，为什么不看一下学校的监控呢？"),
                f.t("李云萧","这样，不就知道是谁动了西门吹的东西了吗？"),
                f.t("喵星人","对啊喵……"),
                f.t("陈嘉敏","我想后台里面，应该没有监控。"),
                f.t("李云萧","那么外面的呢？"),
                f.t("陈嘉敏","我让学校把音乐厅附近的摄像头给关了。"),
                f.t("陈嘉敏","另外，我还派人把所有可能的镜头遮住了。"),
                f.t("李云萧","……"),
                f.t("陈嘉敏","这是为了保护我们艺人的隐私。"),
                f.t("李云萧","（那也不用做这么彻底吧……）"),
                f.t("喵星人","（李云萧，你觉得这事件？）"),
                f.t("李云萧","（应该不是外面的人进来偷窃，剩下的可能，就是内部工作人员。）"),
                f.t("喵星人","（原来之前的“特殊”是这个么，有点意思……）"),
                f.t("陈嘉敏","而且，小蔡说，你是西门吹的好朋友吧。"),
                f.t("蔡助理","我是第一次见到她主动让人进休息室呢。"),
                f.t("李云萧","……好吧，我知道了。"),
                f.t("李云萧","只是调查一下，我不保证能找到发卡。"),
                f.t("陈嘉敏","没关系，只要你能让西门吹上台表演，什么都可以商量。"),
                f.t("李云萧","这个就等我完成任务后说吧。"),
                f.t("陈嘉敏","接下来，我还有些其他事情，这里就留给蔡助理了。"),
                f.t("陈嘉敏","有什么事情你可以问她，她回回答你的。"),
                f.t("李云萧","且慢，我需要询问你……陈经纪人的情况。"),
                f.t("蔡助理","陈经理很忙的，虽然委托与你，但是不要太过分了！"),
                f.t("陈嘉敏","小蔡，没关系的……"),
                f.t("蔡助理","知道了。"),
                f.t("陈嘉敏","那么小侦探，你想知道什么？"),
                f.t("李云萧","我想知道，在事件发生的时间，您在做什么？"),
                f.t("陈嘉敏","你是在怀疑我吗？"),
                f.t("李云萧","我只是在想，能不能先排除陈经纪人的嫌疑。"),
                f.t("陈嘉敏","哈哈哈，有意思。"),
                f.t("陈嘉敏","我是10分钟前刚到学校的，这一点你可以询问任何一个工作人员。"),
                f.t("陈嘉敏","至于事件的详情，是小蔡汇报给我的，我只是负责找到了你而已。"),
                f.t("李云萧","原来如此，那就没问题了。"),
                f.t("陈嘉敏","那么，剩下的事情，就交给你了。"),
                f.t("李云萧","还有，我希望您能尽快回来。"),
                f.t("喵星人","她走了。"),
                f.t("李云萧","（那么，开始吧！）")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetDetectJudgeNode("T21_01");
        }

    }
}
