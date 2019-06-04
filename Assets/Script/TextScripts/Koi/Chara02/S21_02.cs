using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class S21_02 : TextScript
    {
        public S21_02(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "这我怎么可能知道……"),
                f.t("李云萧", "别卖关子了，快说吧。"),
                f.t("喵星人","好吧……"),
                f.t("喵星人","其实，我去查关于“吹雪”的情报了。"),
                f.t("李云萧","吹雪，那是什么？"),
                f.t("陆菲菲","啊，我好像有在哪里听说过。"),
                f.t("李云萧","真的？在哪？"),
                f.t("陆菲菲","嗯……嗯……"),
                f.t("陆菲菲","啊，好像是个高中生组合！"),
                f.t("喵星人","猜对了！"),
                f.t("李云萧","高中生组合？那是什么？"),
                f.t("喵星人","偶像啊，偶像！"),
                f.t("李云萧","啊？"),
                //——CG 喵星人的情报——
                f.t("喵星人","你看，就是这个。"),
                f.t("李云萧","[66ccff]（喵星人拿在手里的是张照片，照片上有2个女孩。）[-]"),
                f.t("李云萧","这是谁啊？"),
                f.t("喵星人","阿吹和雪儿，她们两位就是吹雪。"),
                f.t("陆菲菲","好像她们两个人的年纪和我们一样。"),
                f.t("陆菲菲","一想到同龄人已经成为了偶像……好气啊……"),
                f.t("李云萧","[66ccff]（这没什么好气的吧……）[-]"),
                f.t("喵星人","额……这个……如果班长你也相当偶像……"),
                f.t("喵星人","我的意思是如果你出道的话，我勉强以同学的身份支持你。"),
                f.t("李云萧","啊，我也是。"),
                f.t("陆菲菲","你——你们——"),
                f.t("陆菲菲","哼！不理你们了！"),
                //——走掉了——
                f.t("喵星人","喂，你别生气啊！"),
                f.t("李云萧","算了，班长已经走远了……"),
                f.t("喵星人","那么话说回来，怎么样？"),
                f.t("李云萧","什么怎么样？"),
                f.t("喵星人","跟我一起加入吹雪的粉丝团吧！"),
                f.t("李云萧","哈……为什么？我有什么好处？"),
                f.t("喵星人","你要是现在加入的话，就可以得到限量的签名照！"),
                f.t("喵星人","我好不容易才找到两张的，怎么样？")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("算了", "S21_02A");
            dic.Add("那就给我一张吧", "S21_02B");
            return nodeFactory.GetSelectNode(dic);
        }

    }
}
