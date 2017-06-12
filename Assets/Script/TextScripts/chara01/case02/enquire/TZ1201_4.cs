using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1201_4 : TextScript
    {
        public TZ1201_4(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——立绘 李云萧侧面——
                f.OpenDialog(0),
                f.t("【李云萧】", "当时没有别的人吗？"),
                //——立绘 叶枫婷——
                f.t("【叶枫婷】", "当然，我是仔细看过的，只有他一人。"),
                //——立绘 李云萧侧面——
                f.t("【叶枫婷】", "嗯……")
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
