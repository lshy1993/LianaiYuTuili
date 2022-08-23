using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TZ1102_6 : TextScript
    {
        public TZ1102_6(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "你看到了，他从袋子里拿出来试卷的那一幕？"),
                //——背景 证人台侧——
                //——立绘 叶枫婷——
                f.t("【叶枫婷】", "是的，因为桌上正好放着试卷袋。"),
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "视力真好……"),
                //——背景 助手侧——
                //——立绘 苏梦忆侧面——
                f.t("【苏梦忆】", "视力再好，也总有看错的时候吧。"),
                f.t("【李云萧】", "嗯？那么，就是他看错了？"),
                f.t("【苏梦忆】", "我不知道……"),
                f.t("【李云萧】", "……"),
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
