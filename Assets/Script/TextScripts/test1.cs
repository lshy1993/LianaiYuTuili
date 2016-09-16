using Assets.Script.GameStruct;
using Assets.Script.UIScript;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class test1 : TextScript
    {
        public test1(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.t("测试","测试强制事件"),
                f.t("图片特效", "切换淡出背景"),
                f.ChangeBackgroundFade("tt"),
                f.t("测试","直接进入map")
                // f.e(BuildEffect(effect1, effect2...))
                // f.t("", "", () => { ImageManager.RunEffect(BuildEffect(effect)) }) 
                // f.t("", "", ()=>{特效})
                //f.e(1),
                //f.e(2),
                //f.t(1),
                //f.e(3),
                //f.t(4),
            
            };
        }

        public override GameNode NextNode()
        {
            //return base.NextNode();
            Finish();
            return nodeFactory.GetEndTurnNode();
        }

    }
}
