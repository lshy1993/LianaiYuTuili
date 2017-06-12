using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo11_2 : TextScript
    {
        public demo11_2(DataManager manager, GameObject root, PanelSwitch ps) : base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*项茂->提早上课
                f.OpenDialog(0),
                f.t("苏梦忆", "你们能提早到什么程度呢？"),
                f.t("项茂", "这要看队员们赶过来的速度了。"),
                f.t("项茂", "在1、2楼的高一年级的同学来的最快，3、4楼的高二又稍微慢一点。"),
                f.t("项茂", "如果上节课的老师没有拖堂的话，全力跑过来一般要3分钟才能到这里。"),
                f.t("项茂", "走过来的话，至少要5分钟左右。"),
                f.t("李云萧", "这么久？我们走过来，花了这么久时间么？"),
                f.t("苏梦忆", "我们？大概5分钟吧。"),
                f.t("项茂", "没办法，教学楼的出口和操场虽然近，但必须要绕教学半圈过来。"),
                f.t("李云萧", "学校也不设置点边门……"),
                f.t("李云萧", "如果反过来，从操场回到教室，也要5分钟左右吧。"),
                f.t("项茂", "我想是的，毕竟下楼比上楼轻松多了。"),
                f.GetEvidence("操场与教学楼的距离"),
                f.t("李云萧", "原来如此，还有这么一说……")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetDetectJudgeNode("detest3");
        }

    }
}
