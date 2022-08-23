using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class S42_01 : TextScript
    {
        public S42_01(DataManager manager, GameObject root, PanelSwitch ps) : base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //——背景：艺术楼 走廊——
                //——SE 钢琴声——
                f.OpenDialog(),
                f.t("李云萧", "[66ccff]（又是那琴声……）[-]"),
                f.t("李云萧", "[66ccff]（这回悄悄地走过去吧……）[-]"),
                //——背景 舞台上——
                f.t("李云萧", "[66ccff]（找个位置坐下来。）[-]"),
                //——CG 车小曼独奏——
                f.t("李云萧", "[66ccff]（啊……）[-]"),
                f.t("？？？", "……"),
                f.t("李云萧", "不好意思啊，擅自进来了。"),
                f.t("？？？", "这里本来就是开放的地方，谁都可以来。"),
                f.t("李云萧", "[66ccff]（台上的女生，优雅地走到舞台中心，朝我鞠了一躬）[-]"),
                f.t("？？？", "谢谢你来听我的演奏。"),
                f.t("李云萧", "为什么要谢我……"),
                f.t("？？？", "现在能来这里的人，太少了。"),
                f.t("李云萧", "[66ccff]（少女用非常小声的口吻抱怨着。）[-]"),
                f.t("？？？", "男生都喜欢去打球，女生则留在教室里做作业。"),
                f.t("李云萧", "是啊，我班上的同学也是这样。"),
                f.t("李云萧", "[66ccff]（一到下课，就去球场上打球，女生则默默地开始刷作业）[-]"),
                f.t("？？？", "我好像从来都没有见过你，你叫什么？几班的？"),
                f.t("李云萧", "我叫李云萧，是高二（3）班的，刚转学过来。"),
                f.t("？？？", "转学？这个时间点很少见呢。"),
                f.t("？？？", "难怪我觉得你面生……"),
                f.t("李云萧", "[66ccff]（莫非，她能记得所有的学生么？）[-]"),
                f.t("？？？", "果然还是高二的过得轻松啊……"),
                f.t("李云萧", "欸……因为作业还不怎么多……"),
                f.t("李云萧", "对了，我还不知道你的名字。"),
                f.t("车小曼", "车小曼，高三（6）班。"),
                f.t("李云萧", "高三？你已经高三了吗！？"),
                f.t("车小曼", "怎么了？为什么我不能是高三的？"),
                f.t("李云萧", "不、我不是这个意思，只是……"),
                f.t("李云萧", "在我印象中，高三非常忙，每天沉浸在学习中。"),
                f.t("车小曼", "没错，大家都在为高考做准备……"),
                f.t("车小曼", "没有人会像我一样还在这里弹琴。"),
                f.t("李云萧", "[66ccff]（她又一次用极小的声音说着。）[-]"),
                f.t("李云萧", "我、我还会来这里的。"),
                f.t("车小曼", "噗……"),
                f.t("李云萧", "[66ccff]（眼前的她忍不住笑了出来。）[-]"),
                f.t("车小曼", "……"),
                f.t("李云萧", "[66ccff]（嗯？她刚才好像说了一句什么话……）[-]"),
                f.t("李云萧", "[66ccff]（很快就被又一次开始的钢琴声所覆盖了。）[-]"),
                f.t("车小曼", "再见。")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetMapNode();
        }

    }
}