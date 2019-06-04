using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class S27_01 : TextScript
    {
        public S27_01(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                //——背景 食堂——
                f.OpenDialog(),
                f.t("李云萧","（一如既往地全是人……）"),
                f.t("李云萧","（没办法，这里的食物相当地好吃。）"),
                //——立绘 喵星人 普通——
                f.t("李云萧","怎么了，吃不下？"),
                f.t("喵星人","嘻……"),
                f.t("喵星人","嘻嘻……"),
                f.t("喵星人","嘻嘻嘻……"),
                f.t("李云萧","傻笑什么呢？"),
                //——立绘 喵星人 悄悄话——
                f.t("喵星人","你知道不？今天下午有大事情。"),
                f.t("李云萧","大事情？是什么？"),
                f.t("喵星人","有人要来我们学校演出啊。"),
                f.t("李云萧","演出？"),
                f.t("喵星人","对，有个学校安排的晚会。"),
                f.t("李云萧","难怪中午我总看到些家长带着小孩进来。"),
                f.t("李云萧","等一下，最近是有什么活动纪念吗？"),
                f.t("喵星人","比如？"),
                f.t("李云萧","校庆？开放日？之类的。"),
                f.t("喵星人","嗯，应该不是，只是单纯地有活动表演。"),
                f.t("喵星人","枫溪的商业表演每年有好几次，不管是那些特殊的日子。"),
                f.t("李云萧","这样啊。"),
                f.t("喵星人","怎么感觉你打不起兴趣？你不想知道是谁来表演吗？"),
                f.t("李云萧","……"),
                f.t("李云萧","啊！难道！是那个吹雪吗？"),
                f.t("喵星人","答对了！"),
                //——立绘 喵星人-考虑——               
                f.t("喵星人","没想到你还记得。"),
                f.t("喵星人","我以为你早忘了这一档事了。"),
                f.t("李云萧","这么重要的事情我怎么能忘记？"),
                f.t("李云萧","那么，消息是什么呢？"),
                f.t("喵星人","我从粉丝群内得到消息，今天晚上，她们会在学校的音乐厅演出。"),
                f.t("喵星人","啊，对了，现在是几点了？"),
                f.t("李云萧","3点。"),
                f.t("喵星人","我估计现在，她们应该已经到学校了。"),
                f.t("李云萧","然后呢，你想做什么嘛？"),
                f.t("喵星人","当然是去探探班喵。"),
                f.t("喵星人","偷偷潜入后台，然后就能见到我的偶像！"),
                f.t("李云萧","潜入后台？你认真的吗？"),
                f.t("喵星人","你放心，我们学校艺术楼的构造我再清楚不过了。"),
                f.t("喵星人","而且，我还有秘密武器。"),
                f.t("李云萧","抱歉，对于这种潜入类的没什么兴趣……"),
                f.t("喵星人","别、别介、别介喵，你和我一起去吧~"),
                f.t("李云萧","别卖萌！别撒娇！"),
                f.t("喵星人","盯……"),
                f.t("李云萧","怎么办？")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("只能跟着去了", "S22_02");
            return nodeFactory.GetSelectNode(dic);
        }

    }
}
