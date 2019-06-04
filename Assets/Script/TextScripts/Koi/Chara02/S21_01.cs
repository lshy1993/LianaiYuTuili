using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class S21_01 : TextScript
    {
        public S21_01(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                //——背景 教室——
                f.FadeinBackground("classroom"),
                f.TimeSwitch(manager.GetTodayText(),"高二（3）班"),
                f.OpenDialog(),
                f.t("李云萧","[66ccff]（嗯，今天该干什么好呢……）[-]"),
                f.t("李云萧","[66ccff]（喵星人怎么还没有来……）[-]"),
                //——立绘 喵星人 疲惫——
                f.t("喵星人","……"),
                f.t("李云萧","[66ccff]（不知道从哪里跑回来的喵星人，一屁股坐在了自己的位置上）[-]"),
                f.t("李云萧","你去哪里了，怎么现在才回来？"),
                f.t("喵星人","秘密……"),
                f.t("陆菲菲","秘密？有什么消息吗"),
                //——立绘 陆菲菲 兴奋——
                f.t("李云萧","[66ccff]（突然，班长她出现在了喵星人的旁边。）[-]"),
                f.t("陆菲菲","先让我进去。"),
                f.t("喵星人","是是……"),
                f.t("李云萧","[66ccff]（喵星人起身让开了作为，好让班长进到自己的座位）[-]"),
                f.t("陆菲菲","好了，说说看，什么秘密？"),
                f.t("喵星人","秘密……没有什么秘密……"),
                f.t("陆菲菲","啊？那你刚才去哪了？"),
                f.t("喵星人","就是那个电子屏幕。"),
                f.t("李云萧","电子屏幕？"),
                f.t("喵星人","嗯，在走廊尽头设置有一台触屏电脑，学生可以用它查询些东西。"),
                f.t("陆菲菲","啊，你不会是用那个上网吧！"),
                f.t("喵星人","嘿嘿，猜对了。"),
                f.t("李云萧","等一下，既然那是台电脑，本来就可以使用吧。"),
                f.t("喵星人","不不不，哪里有这么好的事情。"),
                f.t("陆菲菲","那个电脑，一般只能浏览学校的主页，没有任何其他可以操作的地方。"),
                f.t("喵星人","一般来说是这样，但是那台东西的本体依旧是电脑。"),
                f.t("李云萧","然后呢？"),
                f.t("喵星人","只要知道有密码就行了。"),
                f.t("李云萧","原来如此。"),
                f.t("陆菲菲","欸？你什么时候知道密码的？"),
                f.t("喵星人","哈——那就是另一个秘密了！"),
                f.t("陆菲菲","我……"),
                f.t("李云萧","[66ccff]（班长她气的说不出话来了。）[-]"),
                f.t("李云萧","我猜大概是什么时候看到的吧。"),
                f.t("喵星人","李云萧，你怎么知道的？"),
                f.t("李云萧","猜的。"),
                f.t("喵星人","真的！太神奇了！"),
                f.t("喵星人","好！那么，你来猜一下我去干什么了。")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("这我怎么知道？", "S21_02");
            return nodeFactory.GetSelectNode(dic);
        }

    }
}
