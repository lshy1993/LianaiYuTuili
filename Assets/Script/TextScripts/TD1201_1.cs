using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TD1201_1 : TextScript
    {
        public TD1201_1(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps):base(gVars, lVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            pieces = new List<Piece>()
            {
                //*喵星人->关于事件
                //——背景 办公室——
                f.t("【苏梦忆】", "到底发生什么了？"),
                //——立绘 喵星人——
                f.t("【喵星人】", "事情是这样的喵，下课后你们都去吃饭了。"),
                f.t("【喵星人】", "我在教室休息了一会，就去语文办公室找老师。"),
                f.t("【喵星人】", "但是，办公室的门是锁着的，所以我就回教室了。"),
                f.t("【喵星人】", "临走的时候，我看到门边上的窗户没有关，所以……"),
                f.t("【李云萧】", "所以你就翻窗进来了？"),
                f.t("【喵星人】", "对。"),
                f.t("【苏梦忆】", "直说不就好了，这样就不会被人误会了。"),
                f.t("【喵星人】", "可是，你们看到了地上的试卷了没有？"),
                f.t("【喵星人】", "那是上午刚考完试的试卷。"),
                f.t("【李云萧】", "是考试卷又怎么了？"),
                f.t("【喵星人】", "我进来的时候，桌上的档案袋是开着的，然后我就拿了起来。"),
                f.t("【喵星人】", "那时候，突然就有人开门进来了。"),
                f.t("【喵星人】", "所以说，我跳进黄河也洗不清了！"),
                f.t("【李云萧】", "你不知道要保护现场吗……"),
                f.t("【李云萧】", "算了，那个时候大概是什么时候？"),
                f.t("【喵星人】", "我不记得了。"),
                f.t("【苏梦忆】", "幸好进来的不是老师，如果你被老师看到的话就惨了。"),
                f.t("【喵星人】", "可是她一口咬定，就是我偷的试卷。"),
                f.t("【喵星人】", "但是我真的只是翻窗进来而已，什么也没有做啊！"),
                f.t("【李云萧】", "那你老老实实告诉他们实情，不是更好？"),
                f.t("【喵星人】", "李云萧，你知道我和语文老师的关系不好的！"),
                f.t("【喵星人】", "这里又没有什么摄像头，老师肯定会觉得我在说谎啊。"),
                f.t("【喵星人】", "而且这件事闹大了，我可是要被退学的！"),
                f.t("【李云萧】", "（没这么严重吧……）"),
                f.t("【喵星人】", "我知道你喜欢推理，求你帮我证明清白！"),
                f.t("【李云萧】", "这么重大的事，要我怎么帮你？"),
                f.t("【喵星人】", "你就在现场看看有没有线索啥的。"),
                f.t("【李云萧】", "你以为是在玩游戏么……"),
                f.t("【喵星人】", "拜托了，现在只有你能救我了！"),
                f.t("【李云萧】", "（你能说的更有诚意点么……）"),
                f.t("【李云萧】", "（看来，得问问那个目击者了。）",() => pieces.Count)
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
