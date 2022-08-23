using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TI13_01_3 : TextScript
    {
        public TI13_01_3(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*调查->桌上的东西
                f.t("李云萧", "社团活动的课桌，上面摆放着一些书法工具。"),
                f.t("李云萧", "砚台、毛笔，这是软笔书法使用的。"),
                f.t("李云萧", "那个黑色的一块是什么？"),
                f.t("苏梦忆", "那个就是墨，沾水后再砚台上磨，就变成墨水了。"),
                f.t("李云萧", "原来是这样使用的……"),
                /*
                这里要跳回现场调查
                */
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.FindTextScript("T11002");
        }

    }
}
