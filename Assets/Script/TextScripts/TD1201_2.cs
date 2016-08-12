using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TD1201_2 : TextScript
    {
        public TD1201_2(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps):base(gVars, lVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            pieces = new List<Piece>()
            {
                //*喵星人->现场情况
                f.t("【李云萧】", "说起来，这里的玻璃窗碎了。"),
                f.t("【喵星人】", "我翻窗进来的时候就已经是这样了。"),
                f.t("【李云萧】", "你进来的时候，还发现了什么？"),
                f.t("【喵星人】", "除了地上掉着几张试卷，很让我在意，没有别的了。"),
                f.t("【苏梦忆】", "地上掉落的试卷？"),
                f.t("【喵星人】", "就是那几张，好像是从试卷袋里抽出来的。"),
                f.t("【李云萧】", "那么，办公桌上的情况呢？"),
                f.t("【喵星人】", "就只有那个被打开的试卷袋，还有就是普通的作业了。"),
                f.t("【李云萧】", "是这么一回事啊。"),
                f.t("【苏梦忆】", "什么意思？"),
                f.t("【李云萧】", "大概是有谁先你一步，打开了试卷袋，偷拿出了试卷。"),
                f.t("【李云萧】", "之后，大概发生了什么意外，那个人没来得及放回原位，就逃跑了。"),
                f.t("【苏梦忆】", "我去跟叶亭风解释一下，这样就能解释清楚了。"),
                f.t("【李云萧】", "等下，这一切的前提是，你没有撒谎。"),
                f.t("【苏梦忆】", "你难道不相信喵星人吗？"),
                f.t("【喵星人】", "真的不是我干的喵！"),
                f.t("【李云萧】", "当然不是我不相信。只是现在，她同样可以认为你在撒谎。"),
                f.t("【喵星人】", "那怎么办？"),
                f.t("【李云萧】", "先得搜集的信息……"),
                f.t("【喵星人】", "我相信你还有苏梦忆的喵！"),
                f.t("【苏梦忆】", "就算你相信我，我什么也不会啊。"),
                f.t("【喵星人】", "完了喵……"),
                f.t("【李云萧】", "既然有人来过这里，又匆忙离开，那么肯定留下了什么线索。"),
                f.t("【李云萧】", "即使找不到能证明你清白的东西，也应该能发现点什么的！"),
                f.t("【苏梦忆】", "嗯，我相信你。"),
                f.t("【李云萧】", "（现在可以尝试着调查了……）",() => pieces.Count)
                /*
                这里要跳转【对话】
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
