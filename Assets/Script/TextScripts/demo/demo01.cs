using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo01 : TextScript
    {
        public demo01(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("? ? ?","李云萧！"),
                f.t("李云萧","[66ccff]（突然的喊声，将我从神游中拉了回来。）[-]"),
                f.SetBackground("corridor"),
                f.t("李云萧","！！！"),
                f.FadeInCharacterSprite(0,"su00_2"),
                f.SpriteVibration(-1),
                f.t("苏梦忆","李云萧，不好了！","","voice_test"),
                f.t("李云萧","是苏梦忆啊？怎么了，什么不好了？"),
                f.SpriteVibration(0),
                f.t("苏梦忆","喵星人被抓了！","","voice_test2"),
                f.t("李云萧","喵星人被抓了？什么意思？"),
                f.t("苏梦忆","喵星人好像去偷试卷，但被人抓住了。"),
                f.t("李云萧","偷试卷？你没搞错？再怎么说这也太……"),
                f.t("苏梦忆","是真的！总之你快点和我走啦！"),
                f.t("李云萧","[66ccff]（说完，她一把抓起我的手，拽着我走向走廊另一侧。）[-]"),
                f.FadeoutAllChara(),
                f.t("李云萧","喂，你等……等一……"),
                f.CloseDialog(),
                f.FadeoutAll()
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.FindTextScript("demo01_1");
        }

    }
}
