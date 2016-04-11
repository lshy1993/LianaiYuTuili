using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TD1202_3 : TextScript
    {
        public TD1202_3(Hashtable gVars, GameObject root, PanelSwitch ps):base(gVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            pieces = new List<Piece>()
            {
                //*询问-关于“意外”
                //——背景 办公室——
                f.t("【苏梦忆】", "能不能告诉我，那个“意外”是什么？"),
                f.t("【项茂】", "我悄悄告诉你，今天踢球的时候把窗户打碎了。"),
                f.t("【苏梦忆】", "啊？"),
                f.t("【项茂】", "看到4楼的那个窗户了没有，就是那个。"),
                f.t("【项茂】", "守门员发球的时候，不知怎么就用力过头了，直接把球踢向了教学楼。"),
                f.t("【项茂】", "然后就这样把4楼的一扇窗户给打碎了。"),
                f.t("【李云萧】", "那是什么时候的事情？"),
                f.t("【项茂】", "我记得很清楚，比赛到下半场的时候，距离结束还有15分钟。"),
                f.t("【李云萧】", "你怎么记得这么清楚？"),
                f.t("【项茂】", "我就是比赛的裁判兼计时员，所以我记得很清楚。"),
                f.t("【李云萧】", "怪不得……"),
                f.t("【李云萧】", "第四节课上课时间是10点45分，之后经过了75分钟……"),
                f.t("【李云萧】", "那么打碎窗户的时间是，12点整？"),
                f.t("【项茂】", "算对了！那时候我的手表还整点报时了。"),
                f.t("【李云萧】", "（12点……这可是有用的证据。）"),
                f.t("【项茂】", "我们的守门员估计是吓到了吧，还傻站在原地好久。"),
                f.t("【项茂】", "等我们叫他了，他才反应过来去道歉的。"),
                f.t("【李云萧】", "（换作是我，估计也会愣一下吧……）"),
                f.t("【苏梦忆】", "谢谢你。"),
                f.t("【项茂】", "不、不用谢……嘿嘿……",() => pieces.Count)
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
