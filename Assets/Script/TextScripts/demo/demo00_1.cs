using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo00_1 : TextScript
    {
        public demo00_1(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.FadeinBackground("classroom"),
                f.OpenDialog(),
                //——背景 后排视角教室——
                f.t("李云萧", "[66ccff]（将新的教材搬回教室后，我们把课本分发给了每一位同学。）[-]"),
                f.t("李云萧", "说起来，喵星人，我们的班主任呢？"),
                f.t("喵星人", "工作做完了，估计已经回家去了。"),
                f.t("李云萧", "这么早就回去了！？"),
                f.SetCharacterSprite(0,"ch3"),
                f.t("？？？", "各位！大家都拿到课本了吗？"),
                //——CG 苏梦忆讲台演讲——
                f.t("？？？", "剩下的男生都回来了，现在就开始新一轮的班委竞选。"),
                f.t("？？？", "每个人都可以参加竞选，上讲台进行简单的宣言……"),
                f.t("李云萧", "……"),
                f.ChangeCharacterSprite(0,"ch4"),
                f.t("喵星人", "在盯着看什么呢？"),
                f.t("李云萧", "没、没什么……"),
                f.t("喵星人", "啊~我懂了，原来是班长啊~"),
                f.t("李云萧", "班、班长？"),
                f.t("喵星人", "喏，站在讲台上讲话的就是了。"),
                f.t("喵星人", "怎么？被她迷住了？"),
                f.t("李云萧", "没……"),
                f.t("喵星人", "你都写在脸上了，好喵？"),
                f.t("李云萧", "[66ccff]（虽然，真的有种令人心静的清秀……）[-]"),
                f.t("喵星人", "但不得不承认，班长她确实漂亮。"),
                f.t("喵星人", "不过，我劝你一句，想追求她是基本不可能的。"),
                f.t("李云萧", "你在乱说什么……等等，为、为什么？"),
                f.t("喵星人", "嗯~你就当我什么也没说吧？"),
                f.t("李云萧", "喵星人，说起来，我旁边的这个座位是谁的？"),
                f.t("喵星人", "就是班长她的喵。"),
                f.t("李云萧", "不会吧……"),
                f.FadeoutAll(),
                //f.RemoveAll(),
                f.FadeinBackground("classroom"),
                f.OpenDialog(),
                f.t("李云萧", "[66ccff]（竞选投票结束了，现在正在统计票数……）[-]"),
                f.SetCharacterSprite(0,"ch4"),
                f.t("喵星人", "我们走吧。"),
                f.t("李云萧", "走？这不是还没结束吗？"),
                f.t("喵星人", "对于我们这些不参选的人来说，已经结束了。"),
                f.t("喵星人", "再说，班长她也说了解散了。"),
                f.t("李云萧", "是、是吗？我刚刚没仔细听。"),
                f.t("喵星人", "我还要带你逛校园喵！"),
                f.FadeoutAll(),
                //f.RemoveAll(),
                //——背景 校园地图——
                f.FadeinBackground("school"),
                //*这里进入地图说明 人物考虑用Q版
                f.OpenDialog(),
                f.t("喵星人", "先给你介绍一下我们的校园喵。"),
                f.t("喵星人", "这就是我们所在的[ff6600]1号教学楼[-]，整个高中部都在这里。"),
                f.t("喵星人", "然后与之相连的，则是……好麻烦！"),
                f.t("李云萧", "怎么不介绍了？"),
                f.t("喵星人", "因为字太多了，加上这是试玩版，没必要解释这么清楚。"),
                f.t("喵星人", "只要把鼠标移到按钮上，就能看到这个地点的详细介绍。"),
                f.t("喵星人", "好了！讲完了，我们去吧。"),
                f.t("李云萧", "……"),
                f.t("喵星人", "2号寝室楼，别搞错了。"),
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetMapNode();
        }

    }
}
