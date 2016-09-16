using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1101_5 : TextScript
    {
        public TZ1101_5(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "密、密室？"),
                //——背景 证人台侧——
                //——立绘 张傲——
                f.t("【张傲】", "没错，就是传说中的密室，里面的人不能出去，外面的人不能进来。"),
                f.t("【张傲】", "办公室的门是锁上的，人也不能从窗外飞进来，这不就是一个完美密室吗？"),
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "唔……"),
                //——背景 助手侧——
                //——立绘 苏梦忆侧面——
                f.t("【苏梦忆】", "真是无聊的询问呢，李云萧。"),
                f.t("【李云萧】", "连你也来笑我……"),
                f.t("【苏梦忆】", "不过，现场一定不是密室。"),
                f.t("【李云萧】", "你为什么这么肯定？"),
                f.t("【苏梦忆】", "因为如果真的是密室的话，我们就输了。"),
                f.t("【李云萧】", "（承认现场是密室的话，就等于承认了只有他能进入现场了。）"),
                f.t("【李云萧】", "（一定有什么地方疏漏了！）",() => pieces.Count),
                /*
                这里要跳转【继续询问】
                */
            };
        }

        public override GameNode NextNode()
        {
            //return base.NextNode();
            Finish();
            //return nodeFactory.FindTextScript("T11002");
            return nodeFactory.GetDetectJudgeNode("detest1");
            //return nodeFactory.GetMapNode();
        }

    }
}
