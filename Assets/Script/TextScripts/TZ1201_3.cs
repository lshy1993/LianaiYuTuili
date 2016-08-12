using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1201_3 : TextScript
    {
        public TZ1201_3(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps):base(gVars, lVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            pieces = new List<Piece>()
            {
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "钥匙？"),
                //——立绘 叶枫婷——
                f.t("【叶枫婷】", "办公室的钥匙。"),
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "为什么你会有办公室的钥匙？"),
                f.t("【苏梦忆】", "每个科目的总代表可以得到对应办公室的备用钥匙。"),
                f.t("【李云萧】", "还有这么一回事？"),
                f.t("【叶枫婷】", "这样，即使老师不在的时候，我也能将作业交上去。"),
                f.t("【李云萧】", "也就是说，你在任何时间能进入办公室？"),
                f.t("【叶枫婷】", "是的……",() => pieces.Count)
                /*
                这里要跳转【继续询问】
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
