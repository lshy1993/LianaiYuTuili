using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo11_3 : TextScript
    {
        public demo11_3(DataManager manager, GameObject root, PanelSwitch ps) : base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*项茂->关于意外
                f.OpenDialog(0),
                f.t("苏梦忆", "能不能告诉我们，到底发生了什么“意外”？"),
                f.t("项茂", "悄悄告诉你，那个窗户其实是被我们打碎的。"),
                f.t("苏梦忆", "啊？？"),
                f.t("项茂", "嘘！比赛到下半场的时候，红队的守门员发球的时候，\n好像是用力过头，直接把球踢向了教学楼。"),
                f.t("项茂", "然后就这样，把4楼的一扇窗户给打碎了。"),
                f.t("李云萧", "那是什么时候的事情！？"),
                f.t("项茂", "我记得很清楚，那时距离比赛结束，只剩15分钟。"),
                f.t("李云萧", "苏梦忆，第四节课开始的时间是？"),
                f.t("苏梦忆", "好像是10点45分。"),
                f.t("李云萧", "10点45分开始比赛，经过了了75分钟……"),
                f.t("李云萧", "也就是说，打碎窗户的时间是12点？"),
                f.t("项茂", "对对对，那个时候我的手表还整点报时了。"),
                f.t("李云萧", "先问下，你的手表准时吗？"),
                f.t("项茂", "当然，我跟学校的时间是同步的。"),
                f.GetEvidence("打碎窗户的时间"),
                f.t("苏梦忆", "然后呢？你们派人过去了吗？"),
                f.t("项茂", "估计是吓到了吧，那家伙还傻站在原地好久。"),
                f.t("项茂", "等我们叫他了，他才反应过来，跑去道歉的。"),
                f.t("李云萧", "原来如此……")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetDetectJudgeNode("detest3");
        }

    }
}
