using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo17 : TextScript
    {
        public demo17(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(0),
                f.ChangeCharacterSprite(0,"ch3"),
                f.t("苏梦忆", "看你的眼神，你发现了什么吗？"),
                f.t("李云萧", "嗯，我大致上明白事情的经过了。"),
                f.t("李云萧", "首先，窗户被打碎的时间应该是12点整，这一点由项茂可以证明。"),
                f.t("苏梦忆", "咦？可是，叶婷明明说她11点50分进的教室。"),
                f.t("李云萧", "没错，喵星人进办公室的时候，窗户就已经碎了。"),
                f.t("李云萧", "也就是说，他是在12点之后才进去的。"),
                f.t("李云萧", "那么，叶婷是不可能在10分钟前见到喵星人的。"),
                f.t("李云萧", "她在说谎。"),
                f.t("苏梦忆", "但是，还有另一个人也看到了喵星人吗？"),
                f.t("李云萧", "戚海超？虽然他也是目击者之一，但是，他并没有说过时间。"),
                f.t("李云萧", "另外，你知道是谁打破了这扇窗的吗？"),
                f.t("苏梦忆", "不知道……啊！难道是？"),
                f.t("李云萧", "刚才项茂说过，红队的守门员发球时，碰碎了窗户。"),
                f.t("李云萧", "这个是今天的对阵表，戚海超就是红队的守门员。"),
                f.t("苏梦忆", "但，这不能改变他看到喵星人的事实啊？又有什么意义呢？"),
                f.t("李云萧", "有意义，最直接的就是目击时间的改变。"),
                f.t("李云萧", "我原以为他也是在11点50分看到喵星人的，但是……"),
                f.t("李云萧", "考虑到从操场回到教学楼的时间，再从楼梯跑到办公室。"),
                f.t("李云萧", "你还记得我们走过来花了多久吗？"),
                f.t("苏梦忆", "5分钟吧？"),
                f.t("苏梦忆", "啊！我懂了！反过来的话，也要5分钟。"),
                f.t("李云萧", "从操场过来，再一口气上4楼，怎么看也要5分钟。"),
                f.t("李云萧", "所以，他碰到的叶婷的时间，至少在12点05分后。"),
                f.t("李云萧", "但是，明明是12点05分的事情，她为什么要说是11点50分呢？"),
                f.t("苏梦忆", "会不会只是单纯的记错了？"),
                f.t("李云萧", "不会的。"),
                f.t("李云萧", "叶婷可是说，看了挂钟上的时间是11点50分。"),
                f.t("李云萧", "刚才我确认过，办公室的时间没有走慢。"),
                f.t("苏梦忆", "可是为什么她要这么说呢？"),
                f.t("李云萧", "回去吧，是时候去结束误会了！"),
                f.ChangeBackground("office"),
                f.SetCharacterSprite(0,"ch4"),
                f.t("喵星人", "李云萧，你们可算回来了！"),
                f.t("李云萧", "咦？戚海超他人呢，怎么就你们两个？"),
                f.ChangeCharacterSprite(0,"ch5"),
                f.t("叶婷", "他说他一会有急事，就先走了。"),
                f.t("李云萧", "这样啊……"),
                f.t("李云萧", "对了，叶婷同学，能再让我听一遍你的证词吗？"),
                f.t("叶婷", "怎、怎么，不是没有问题了吗？还、还来啊？"),
                f.t("李云萧", "那个，我刚出去了一趟，有点忘记了。"),
                f.t("李云萧", "（她为什么会弄错时间，一定要搞清楚。）"),
                f.t("叶婷", "好吧，你听好了。")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEnquireNode("demoZ03");
        }

    }
}
