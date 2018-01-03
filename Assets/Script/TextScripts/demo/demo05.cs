using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo05 : TextScript
    {
        public demo05(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "[66ccff]（现场也已经调查完了，最后……）[-]"),
                f.t("李云萧", "好了，这么说来，你应该是第一目击者。"),
                f.t("李云萧", "刚才没有问你，我叫李云萧，请问你是？"),
                f.SetCharacterSprite(0,"ch5"),
                f.t("女生", "想不到，居然有人连我也不认识！"),
                f.t("李云萧", "抱歉，我刚转校过来……\n[66ccff]（何况，我也没有这段时间的记忆……）[-]"),
                f.t("女生", "哼！整个年级没有不知道我的人，你是第一个！"),
                f.t("李云萧", "[66ccff]（话说回来，我为什么要认识你……）[-]"),
                f.t("李云萧", "苏梦忆，她很有名吗？"),
                f.t("苏梦忆", "额，好像是吧……"),
                f.t("女生", "我就是从进入高中开始，语文成绩一直年级第一的，叶婷！"),
                f.t("李云萧", "哦……\n[66ccff]（一直第一？那么其他科目呢？）[-]"),
                f.t("叶婷", "现在，担任全年级的语文课总代表！"),
                f.t("李云萧", "总代表是什么？"),
                f.t("苏梦忆", "是从每个班级的课代表中选出来的，类似老师助理的职位。"),
                f.t("李云萧", "这学校还有这种职位？"),
                f.t("叶婷", "你们的考试成绩，有时候就是我给的分，明白吗？"),
                f.t("叶婷", "如果你们不交作业，被我知道的话……"),
                f.t("李云萧", "[66ccff]（……会怎么样？）[-]"),
                f.t("李云萧", "我知道了，总之，你说你有证据？"),
                f.t("叶婷", "没错，来偷试卷的就是他，我亲眼看到的！"),
                f.t("","[00ff00]听到她说的话了吗？她亲眼目睹了这一切。[-]"),
                //f.t("","[00ff00]但人不能直接读取记忆，总会出现各种问题的。[-]"),
                f.t("","[00ff00]接下来将要进入另一个核心环节【无休止询问】。[-]"),
                f.t("","[00ff00]这个环节需要找出证词中，与现实存在[ff6600]矛盾[-]的地方。[-]"),
                f.t("","[00ff00]在证词出现的时候，请挑选并[ff6600]点击[-]合适的证据进行反驳。[-]"),
                f.t("","[00ff00]当然，有的时候证词里没有什么矛盾，\n这时就需要对证词进行[ff6600]深究[-]。[-]"),
                f.t("","[ff6600]深究[-][00ff00]的操作方法，就是[ff6600]点击[-]这条证词本身。[-]"),
                f.t("","[00ff00]证词会随时间流逝[ff6600]自动更换[-]，请注意时间的把握。[-]"),
                f.t("","[00ff00]按下[ff6600]左Ctrl[-]可以进行加速。\n按下[ff6600]空格键[-]进行减速，但是会消耗注意力。[-]"),
                f.t("","[00ff00]另外，这个环节的名字取自“无休止议论”与“询问开始”。\n那么，【无休止询问】开始！[-]"),
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ00");
        }

    }
}
