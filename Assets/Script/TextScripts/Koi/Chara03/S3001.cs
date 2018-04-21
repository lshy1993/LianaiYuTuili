using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class S3001 : TextScript
    {
        public S3001(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——背景 商场——
                f.FadeinBackground("square_day"),
                f.OpenDialog(),
                f.t("李云萧", "[66ccff]（去买点要用的东西吧……）[-]"),
                //——SE 人声——
                f.t("李云萧", "[66ccff]（双休日的时候，人就是多啊……）[-]"),
                f.t("？？？", "啊！？"),
                f.t("李云萧", "哇！"),
                //——CG 转角相撞坐地上——
                f.t("李云萧", "[66ccff]（好像撞到什么人了……）[-]"),
                f.t("李云萧", "对不起，你没事吧？"),
                //——CG 伸出手去拉——
                f.t("李云萧", "我扶你起来吧。"),
                //——SE 站立——
                //——CG结束——
                f.t("？？？", "干什么啊，你这个人……（日语）"),
                f.t("李云萧", "对不起，刚刚我没注意……"),
                f.t("李云萧", "嗯？刚才，你说什么？"),
                f.t("？？？", "痛死我了……（日语）"),
                f.t("李云萧", "[66ccff]（糟糕，难道是外国人？）[-]"),
                f.t("？？？", "……"),
                f.t("李云萧", "[66ccff]（但她外表看过去年龄和我差不多大吧，可能也是高中生。）[-]"),
                f.t("李云萧", "[66ccff]（试着用英语吧……）[-]"),
                f.t("李云萧", "Eh...I am sorry...…"),
                f.t("？？？", "喂，你！"),
                f.t("李云萧", "诶？你会说中文？"),
                f.t("？？？", "怎么了，有什么奇怪的？"),
                f.t("李云萧", "没、没有。"),
                f.t("？？？", "看你的戴着校徽，是枫溪的学生啊。"),
                f.t("李云萧", "[66ccff]（忘记把校徽摘下来了……）[-]"),
                f.t("？？？", "能不能借我一点钱？"),
                f.t("李云萧", "[66ccff]（我没听错吧，哪有陌生人一上来就借钱的？）[-]"),
                f.t("李云萧", "……")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetMapNode();
        }

    }
}
