using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_NetCafe : TextScript
    {
        public default_NetCafe(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                //——背景：网咖外景——
                f.ChangeBackground("netcafe_day"),
                f.OpenDialog(),
                f.t("【李云萧】", "来，战个痛！"),
                f.t("【李云萧】", "……"),
                f.t("【李云萧】", "…………"),
                f.t("【李云萧】", "真可惜，什么事也没发生。"),
                f.t("【李云萧】", "但是，觉得自己变强了！"),
                f.t("【李云萧】", "（宅力提升了！）"),
                f.FadeoutAll()
                //——背景 消失——
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            //TODO: 随机属性增加
            DataManager.GetInstance().gameData.player.AddRandom("宅力", 5, 10);
            return nodeFactory.GetEndTurnNode();
        }

    }
}
