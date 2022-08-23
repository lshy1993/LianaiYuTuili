using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_WcDonald_1 : TextScript
    {
        public default_WcDonald_1(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                //——背景：WcDonald——
                f.OpenDialog(),
                f.t("【李云萧】", "该出手时就出手！"),
                f.t("【店员】", "你好，请问先生点些什么？"),
                f.t("【李云萧】", "我要……"),
                //背景转黑
                f.t("【李云萧】", "……"),
                f.t("【李云萧】", "…………"),
                //——背景：WcDonald——
                f.t("【李云萧】", "呼——吃饱了，果然还是这个口味适合我。"),
                f.t("【李云萧】", "不过，什么事情也没发生，也差不多该走了。"),
                f.t("【李云萧】", "（体力得到了恢复）")
                //——背景 消失——
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            //TODO
            return nodeFactory.GetEndTurnNode();
        }

    }
}
