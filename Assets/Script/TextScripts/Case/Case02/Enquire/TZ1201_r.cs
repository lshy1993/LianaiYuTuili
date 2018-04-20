﻿using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1201_r : TextScript
    {
        public TZ1201_r(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——立绘 李云萧侧面——
                f.OpenDialog(0),
                f.t("【李云萧】", "她的理由就到这里了……"),
                //——立绘 叶枫婷——
                f.t("【苏梦忆】", "李云萧，能证明喵星人的清白吗？"),
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "现在证明清白是不可能的，证据还不够……"),
                f.t("【李云萧】", "但是，如果能把嫌疑人的范围扩大就好了。")
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
