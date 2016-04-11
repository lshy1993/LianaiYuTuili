using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class T12006 : TextScript
    {
        public T12006(Hashtable gVars, GameObject root, PanelSwitch ps):base(gVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            pieces = new List<Piece>()
            {
                //——背景 走廊——
                //——立绘 戚海——
                f.t("【李云萧】", "刚才就觉得不对劲了。"),
                f.t("【戚海】", "哪、哪里？"),
                f.t("【李云萧】", "你在发现窗被足球砸碎前，是在对面的走廊上吧？"),
                f.t("【戚海】", "是、是的，我在吹风。"),
                f.t("【李云萧】", "那就奇怪了，在那边是看不到这边的办公室，因为有阻挡。"),
                f.t("【李云萧】", "就算角度正好，你能看到办公室，也是不可能看到办公室里面的窗户！"),
                f.t("【戚海】", "哎！"),
                f.t("【李云萧】", "请不要来什么“哎”。"),
                f.t("【戚海】", "真、真是这样的吗？"),
                f.t("【李云萧】", "你不相信的话，我们走过去，站在那边朝这里看。"),
                f.t("【李云萧】", "到底能不能看清这里，一下子就能弄清楚了。"),
                f.t("【戚海】", "不、不必了……"),
                f.t("【李云萧】", "那么，你是怎么知道是足球打碎了窗户呢？"),
                f.t("【戚海】", "这个，那个，额……对不起！"),
                f.t("【戚海】", "其实我不是在这里看到的，而是在操场。"),
                f.t("【李云萧】", "操场？看来有必要去操场调查看看。"),
                f.t("【叶枫婷】", "有这个必要吗？他不是和我一起目击了，这就够了！"),
                f.t("【李云萧】", "嗯？谨慎地调查不行吗，这可是严重的作弊行为？"),
                f.t("【李云萧】", "还是说，操场里有什么不好的东西？"),
                f.t("【叶枫婷】", "没、没有……"),
                f.t("【叶枫婷】", "调查可以，但他得跟我待在一起，防止他销毁证据。"),
                f.t("【李云萧】", "没问题，你们就待在这里，锁好办公室的门。"),
                f.t("【李云萧】", "不要让任何人靠近，也不要别的同学发现。"),
                f.t("【叶枫婷】", "嗯、嗯……"),
                f.t("【李云萧】", "喵星人，你看好他，不要让他做小动作。（小声）"),
                f.t("【喵星人】", "哦，我不会让任何人进现场的。"),
                f.t("【李云萧】", "（现在可以去操场了）",() => pieces.Count),
                /*
                这里跳转【调查】
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
