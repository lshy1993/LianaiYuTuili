using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class default_CityLibrary : TextScript
    {
        public default_CityLibrary(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //——背景：市立图书馆——
                f.ChangeBackground("city_library_day"),
                f.OpenDialog(),
                f.t("李云萧", "图书馆，是个适合学习的日子。"),
                f.t("李云萧", "今天也难得的出来休息一下吧……"),
                f.t("李云萧", "……"),
                f.t("李云萧", "…………"),
                //——背景：市立图书馆 傍晚——
                f.t("李云萧", "真可惜，什么事也没发生。"),
                f.t("李云萧", "不过，却意外地感到了内心的充实。"),
                f.t("李云萧", "那么就这样结束，回家吧."),
                f.t("李云萧", "（某些能力得到了提升）"),
                f.FadeoutAll()
                //——背景 消失——
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            //TODO:随机增加属性\
            DataManager.GetInstance().gameData.player.RandomAdd(4, 10);
            return nodeFactory.GetEndTurnNode();
        }

    }
}
