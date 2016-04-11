using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TD1202_2 : TextScript
    {
        public TD1202_2(Hashtable gVars, GameObject root, PanelSwitch ps):base(gVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            pieces = new List<Piece>()
            {
                //*询问->关于提早上课
                //——背景 办公室——
                f.t("【苏梦忆】", "你们一般都是提早多少时间开始呢？"),
                f.t("【项茂】", "这要看队员们赶过来的速度了。"),
                f.t("【项茂】", "在1、2楼的高一年级的同学来的最快，3、4楼的高二又稍微慢一点。"),
                f.t("【项茂】", "如果上节课的老师没有拖堂的话，走过来一般要3到5分钟才能到这里。"),
                f.t("【李云萧】", "要这么久么？我们走过来没用了这么多吧？"),
                f.t("【项茂】", "是你没察觉的原因，我们测过，队里短跑第一的人都要2分钟。"),
                f.t("【苏梦忆】", "唔……那我肯定要花更多的时间了……"),
                f.t("【项茂】", "教学楼的出口和操场虽然近，但是隔了条河，最近的桥必须要绕过教学楼。"),
                f.t("【李云萧】", "（学校也不设置个离桥近一点的门……）"),
                f.t("【李云萧】", "诶，那反过来说，从操场回到教室，也要5分钟左右吧。"),
                f.t("【项茂】", "差不多，上完体育课大家都累了，而且上楼比下楼要累。"),
                f.t("【李云萧】", "嗯……（这一点先记下来。）",() => pieces.Count)
                /*
                这里要跳转【对话】
                */
            };
        }

        public override GameNode NextNode()
        {
            //return base.NextNode();
            Finish();
            return nodeFactory.FindTextScript("T11002");
            //return nodeFactory.GetMapNode();
        }

    }
}
