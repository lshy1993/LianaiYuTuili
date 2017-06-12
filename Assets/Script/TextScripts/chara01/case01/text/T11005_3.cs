using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class T11005_3 : TextScript
    {
        public T11005_3(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "根据案件记录上所写，被盗的是高中期中考试的试卷。"),
                //——背景 法庭-审判长侧——
                //——立绘 审判长——
                f.t("【审判长】", "嗯，这回审理的案件，正是试卷偷窃案。"),
                f.t("【审判长】", "问题到此结束，我们正式开始审理案件。"),
                //——背景 助手侧——
                //——立绘 苏梦忆侧面——
                f.t("【苏梦忆】", "李云萧，喵星人他为什么会去偷试卷呢？"),
                f.t("【李云萧】", "我怎么知道，又不是我偷的……"),
                f.t("【李云萧】", "而且我想说，为什么偷试卷这种小事会上法庭？"),
                f.t("【苏梦忆】", "这法庭叫做序审法庭，是为了处理大量的案件而设立的。"),
                f.t("【苏梦忆】", "一切的案件最终都会经过审判，不论其大小。"),
                f.t("【李云萧】", "这样的法庭真的没问题吗？（司法体系早就完蛋了吧？）"),
                //——变黑——
                f.t("【李云萧】", "呼——（总之，先整理下目前的情况……）"),
                f.t("【李云萧】", "（我好像成为了辩护律师……而且，苏梦忆成为了我的助手……）"),
                f.t("【李云萧】", "（难道是我真的失去了这段时间的记忆？说起来，现在的我几岁了？）"),
                f.t("【李云萧】", "（但是看苏梦忆和“昨天”没有什么变化……）"),
                f.t("【李云萧】", "（有没有人能告诉我这是怎么一回事！这是谁和我开的玩笑！）"),
                //——背景 助手侧——
                //——立绘 苏梦忆侧面——
                f.t("【苏梦忆】", "总之，你也渐渐适应了吧，接下来就是正片了。"),
                f.t("【李云萧】", "（但是看起来，真的不像是在开玩笑……）"),
                //——背景 法庭-审判长侧——
                //——立绘 审判长——
                f.t("【审判长】", "沈检察官，请陈述下案情。"),
                f.t("【沈尘业】", "有请负责案件的刑警入庭。"),
                /*
                考虑是否合并到一块
                */
            };
        }

        public override GameNode NextNode()
        {
            //return base.NextNode();
            Finish();
            return nodeFactory.FindTextScript("T11006");
            //return nodeFactory.GetMapNode();
        }

    }
}
