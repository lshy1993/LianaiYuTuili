using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo12_4 : TextScript
    {
        public demo12_4(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*调查->长椅
                f.OpenDialog(0),
                f.t("李云萧", "替补队员休息的长椅。"),
                f.t("李云萧", "嗯，上面好像有份东西……“足球社比赛积分表”？"),
                f.t("李云萧", "上面写了一串人名，还有一些不知道什么意思的数字。"),
                f.t("苏梦忆", "红队？蓝队？这些又是什么？"),
                f.t("李云萧", "大概是在统计每个队员的成绩……等等！"),
                f.t("李云萧", "这不是戚海超的名字吗，他是红队的守门员？"),
                f.GetEvidence("00006"),
                f.t("李云萧", "不会是重名吧？"),
                f.t("苏梦忆", "足球社的话，戚海超应该就是刚才的男生。"),
                f.t("李云萧", "你怎么知道？"),
                f.t("苏梦忆", "我浏览过以前的社团申请表，那上面有他的照片。"),
                f.t("李云萧", "连这都能记得，你这是什么记忆力。"),
                f.t("苏梦忆", "这个……我也不清楚啦……"),
                f.t("李云萧", "不管这个，还是把这个表格放回原处吧。"),
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetDetectJudgeNode("detest3");
        }

    }
}
