using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class S21_02B : TextScript
    {
        public S21_02B(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //dic.Add("那就给我一张吧", "S2000_B");
                f.OpenDialog(),
                f.t("李云萧","既然你这么说了，我就加入吧。"),
                f.t("喵星人","太好了！从现在起，枫溪吹雪后援会的成员终于变为2人了！"),
                f.t("李云萧","等一下，2人?"),
                f.t("李云萧","这么说的话，这个所谓的粉丝团本来只有你一人？"),
                f.t("喵星人","是的，没错！"),
                f.t("李云萧","我去！那么照片呢？"),
                f.t("喵星人","给你！"),
                f.t("李云萧","（喵星人从课桌的最深处找出了一张照片。）"),
                //——CG 照片——
                f.t("李云萧","（照片上是两位女生的艺术照……）"),
                f.t("喵星人","好好保存啊！"),
                f.t("李云萧","不过，只能看看照片啊……"),
                f.t("喵星人","噗——你这么一说……我好像也没见过真人……"),
                f.t("喵星人","有生之年我一定要见吹吹一次！"),
                f.t("李云萧","话说这真的是什么什么偶像么？"),
                f.t("喵星人","高中生美少女偶像。"),
                f.t("李云萧","啊，对对对。"),
                f.t("喵星人","你居然不相信？"),
                f.t("李云萧","不是不相信，只是美少女组合什么的非常罕见……"),
                f.t("喵星人","不不不，她们现在是非常多学生的偶像啊！"),
                f.t("李云萧","（偶像？不会吧？）"),
                f.t("李云萧","你当这里是11区吗……"),
                f.t("喵星人","别这么说，你打开dilidili网看看，现在的人气可火了。"),
                f.t("喵星人","最早是无人问津的从翻唱动画歌曲开始，到现在已经是偶像组合。"),
                f.t("李云萧","真的假的。"),
                f.t("喵星人","那你等着，下回有什么消息，我一定告诉你！"),
                f.t("李云萧","可以啊，你可别临阵脱逃。"),
                f.t("喵星人","一言为定！"),
                f.FadeoutAll()
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEndTurnNode();
        }

    }
}
