using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo0_3 : TextScript
    {
        public demo0_3(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //——背景 走廊——
                f.FadeinBackground("corridor"),
                f.TimeSwitch("8月31日 上午", "1号教学楼3楼 走廊"),
                f.ShowChapter(),
                f.OpenDialog(),
                f.t("", "[66ccff]（将主要的入学手续办好后，我决定前往所在班级的教室。）[-]"),
                f.t("", "[66ccff]（来的路上，问了下其他年级的学生，好像是在3楼的样子。）[-]"),
                f.t("", "高二（6），高二（5），高二（4）……"),
                f.t("", "到了！高二（3）班！"),
                //——SE 嘈杂的人声——
                f.t("", "[66ccff]（唔，其他学生都已经在教室里了么？）[-]"),
                f.t("", "[66ccff]（不过，在进教室之前，得先去见一下班主任。）[-]"),
                f.t("", "好像，她的办公室就在教室的旁边……"),
                f.t("", "[66ccff]（应该就是这里了吧……）[-]"),
                //——SE 敲门——
                f.t("男老师", "请进。"),
                //——SE 推门——
                f.CloseDialog(),
                f.TransBackground("office"),
                f.OpenDialog(),
                f.t("", "请问，郭老师是哪位？"),
                //——立绘 郭老师——
                f.FadeInCharacterSprite(0, "ch7"),
                f.t("郭老师", "我就是，请问你是？"),
                f.t("", "您好！我是新转来的学生，我的名字叫……"),
                f.SetName(),
                f.t("李云萧", "李云萧！"),
                f.t("郭老师", "啊！你就是李云萧，欢迎来到华欣！"),
                f.t("郭老师", "我是高二（3）班的班主任，我叫郭珊珊。"),
                f.t("郭老师", "对了，你已经去过学生宿舍了吗？"),
                f.t("李云萧", "我已经全部整理好了，我是来报道的。"),
                f.t("郭老师", "那我就放心了，你有领到[ff6600]电子学生手册[-]吗？"),
                f.t("李云萧", "电子学生手册？"),
                f.t("郭老师", "教务处应该会发给你的一张卡片，没有领吗？"),
                f.t("李云萧", "指的是这张校园卡吗？"),
                f.t("郭老师", "对对，就是这张卡，它就是[ff6600]电子学生手册[-]。"),
                f.t("李云萧", "原来是这样，我还以为是有什么别的用处。"),
                f.t("郭老师", "你知道怎么开机吗？可以试试打开它。"),
                f.t("李云萧", "怎么开？我从来没用过。"),
                //——新手教程-打开校园卡界面——
                f.t("郭老师", "看到右下角的[ff6600]NOTE按钮[-]了吧。"),
                f.t("郭老师", "平时[ff6600]点击[-]一下就能打开主界面了，\n走路和学习的时候就不要去看了。"),
                f.t("李云萧", "原来如此，我看看……"),
                f.t("郭老师", "不急，你先去教室和同学们见个面吧。"),
                f.t("郭老师", "等一会我来开个班会，到时候需要你做自我介绍。"),
                f.t("李云萧", "我知道了。可是郭老师，教室里有多余的位置吗？"),
                f.t("郭老师", "哎呀！瞧我这记性。"),
                f.t("郭老师", "时间也差不多了，你和我一起去吧。"),
                f.t("李云萧", "好的。"),
                f.CloseDialog(),
                f.FadeoutAll()
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.FindTextScript("demo0_4");
        }

    }
}
