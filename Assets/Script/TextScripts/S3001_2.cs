using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class S3001_2 : TextScript
    {
        public S3001_2(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps):base(gVars, lVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            pieces = new List<Piece>()
            {
                //——背景 商场——
                f.t("【李云萧】", "额，好吧，我没带太多的钱。"),
                f.t("【？？？】", "那么，先借我100块吧？"),
                f.t("【李云萧】", "给你……"),
                f.t("【？？？】", "谢啦，我以后会还给你的！"),
                //——立绘消失——
                f.t("【李云萧】", "（等下！好像哪里不对……）"),
                f.t("【李云萧】", "（我都不知道她的名字，这不会是诈骗吧？）"),
                f.t("【李云萧】", "喂！等——"),
                //——SE 人声——
                f.t("【李云萧】", "（已经走了……可恶，居然被骗了！）",() => pieces.Count)
                //——金钱减少100——
                //——背景 消失——
            };
        }

        public override GameNode NextNode()
        {
            //return base.NextNode();
            Finish();
            //return nodeFactory.GetEduNode("");
            return nodeFactory.GetMapNode();
        }

    }
}
