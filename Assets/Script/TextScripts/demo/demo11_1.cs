using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo11_1 : TextScript
    {
        public demo11_1(DataManager manager, GameObject root, PanelSwitch ps) : base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*项茂->关于足球比赛
                f.OpenDialog(0),
                f.t("李云萧", "你们是刚踢完一场比赛吗？"),
                f.t("项茂", "……"),
                f.t("李云萧", "唉……"),
                f.t("苏梦忆", "那、那个，你们是刚结束一场比赛吗？"),
                f.t("项茂", "是的，刚刚结束不久。"),
                f.t("苏梦忆", "能告诉我比赛的具体情况吗？"),
                f.t("项茂", "好啊，没问题。"),
                f.t("项茂", "这是我们足球社内部的比赛，从上午第四节的体育课开始，预计进行90分钟。"),
                f.t("李云萧", "现在离下课已经过去1小时了，怎么到现在才结束？"),
                f.t("项茂", "啊，那是因为中途出了点小“意外”，耽误了的一阵子。"),
                f.t("李云萧", "（意外？是发生了什么事吗？）"),
                f.t("项茂", "为了能早点结束，我们还提早5分钟开始上课，结果还是拖延了。"),
                f.t("李云萧", "提早上课？"),
                f.t("项茂", "是啊，因为一节课只有45分钟，\n利用课间的几分钟做准备，就能在上课铃响同时开始比赛。"),
                f.t("苏梦忆", "欸？你们所有人都提早到的吗？"),
                f.t("项茂", "因为是提前说好的队内的训练，大家不敢迟到。"),
                f.t("苏梦忆", "是这样啊，我明白了，谢谢！"),
                f.t("李云萧", "[66ccff]（有两个比较在意的地方，再详细地问他下吧。）[-]")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetDetectJudgeNode("detest3");
        }

    }
}
