using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo10 : TextScript
    {
        public demo10(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.FadeinBackground("ground"),
                f.OpenDialog(),
                f.t("李云萧", "[66ccff]（这里就是学校的操场了……）[-]"),
                f.t("李云萧", "[66ccff]（除了足球场外，外面还有露天的篮球场和排球场。）[-]"),
                f.t("苏梦忆", "我们赶紧开始调查吧！"),
                f.t("李云萧","你怎么对这个特别感兴趣？"),
                f.t("苏梦忆", "你不觉得很有趣吗？"),
                f.t("李云萧", "不能说完全没有兴趣，还好吧……"),
                f.t("苏梦忆", "快看！那边有个穿着球服的同学。"),
                f.t("李云萧", "过去看看……"),
                f.SetCharacterSprite(0,"ch1"),
                f.t("李云萧", "你好，我想向你打听点事。"),
                f.t("? ? ?", "……"),
                f.t("李云萧", "同学？"),
                f.t("? ? ?", "……"),
                f.t("苏梦忆", "噗嗤，好像完全无视你了。"),
                f.t("李云萧", "同！学！问！你！点！事！"),
                f.t("? ? ?", "……"),
                f.t("李云萧", "你来……"),
                f.t("苏梦忆", "那个，这位同学，你好……"),
                f.t("? ? ?", "！！！"),
                f.t("? ? ?", "啊，你好！"),
                f.t("李云萧", "[66ccff]（为什么她一来就行……）[-]"),
                f.t("苏梦忆", "我叫苏梦忆，是高二（3）班的。"),
                f.t("项茂", "我叫项茂，你是来看我们比赛的吗？"),
                f.t("苏梦忆", "不、不是的，我们想问你一些问题。"),
                f.t("项茂", "好呀，随便问，我都会回答你的。"),
                f.t("李云萧", "[66ccff]（他的脸怎么红了？没问题吧……）[-]")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetDetectJudgeNode("detest3");
        }

    }
}
