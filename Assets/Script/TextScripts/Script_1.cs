using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class Script_1 : TextScript
    {
        public Script_1(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps):base(gVars, lVars, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                f.t("第一句", "11111"),
                f.t("第二句", "22222"),
                f.t("第三句", "33333"),
                f.t("第四句", "44444"),
                f.t("第五句","设置局部变量a为true", ((Hashtable gVar, Hashtable lVar)=> {
                    if(lVars.ContainsKey("a")) lVars["a"] = false;
                    else lVar.Add("a", true);
                    return 5;
                })),

                f.t("第六句", "55555"),
                f.t("第七句", "如果变量a为true跳转第九句",(Hashtable gVar, Hashtable lVar)=> {
                    if((bool)lVar["a"]) return 8;
                    else return 7;
                }),
                f.t("第八句", "如果变量a为true则不显示"),
                f.t("第九句","9999999"),
                //f.t("跳转", "简单跳转第二句", ()=> 1)
                f.t("结束", "跳转到大地图部分", () => pieces.Count)
            };
        }

        public override GameNode NextNode()
        {
            //return base.NextNode();
            Finish();
            return nodeFactory.GetMapNode();
        }

    }
}
