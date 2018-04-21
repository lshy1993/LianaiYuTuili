using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class T11001 : TextScript
    {
        public T11001(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "……嗯……嗯……"),
                f.t("李云萧", "……嗯……好困……"),
                f.FadeinBackground("hss"),
                f.FadeoutBackground(),
                f.t("李云萧", "嗯……嗯……"),
                f.t("李云萧", "[66ccff]（是谁开的灯……我还没睡够呢……）[-]"),
                f.FadeinBackground("hss"),
                f.FadeoutBackground(),
                f.t("李云萧", "……嗯？"),
                f.FadeinBackground("white"),
                f.t("李云萧", "！！"),
                f.t("李云萧", "[66ccff]（好刺眼！嗯？）[-]"),
                f.TransBackground("hss"),
                f.t("李云萧", "这、这是哪？"),
                //——立绘 正装（西装）苏梦忆？——
                f.SetCharacterSprite(0,"ch1"),
                f.t("？？？", "快醒醒！快醒醒！"),
                f.t("李云萧", "你、你是谁！？"),
                f.t("？？？", "你在说什么梦话呢？赶快醒醒！"),
                f.WindowVibration(0.1f),
                //——镜头震动——
                f.t("李云萧", "哇！别晃我了！"),
                f.t("？？？", "马上就要开庭审理了！"),
                f.t("李云萧", "什么？开庭？"),
                f.t("？？？", "你是不是睡迷糊了？"),
                f.t("李云萧", "是有点没睡醒，但、但是这是哪？"),
                f.t("？？？", "这里？这里是被告人候审室啊。"),
                f.t("李云萧", "不对，我怎么会在这里？还有，你是谁，为什在这里？"),
                f.t("？？？", "喂喂，别开玩笑，你当然是来辩护的啦！"),
                f.t("？？？", "我是你的助手，自然要跟过来啊。"),
                f.t("李云萧", "辩护？替谁啊？再说，我还是学生不是律师。"),
                f.t("李云萧", "还有，你什么时候是我的助手了？"),
                f.t("？？？", "你是刚刚进入凌理律师事务所的新人律师。"),
                f.t("李云萧", "[66ccff]（嗯？律师？这是什么情况？）[-]"),
                f.t("？？？", "将来一定会成为著名的大律师，姐姐是这么说的。"),
                f.t("？？？", "当然，我从心底也是这么想的！"),
                f.t("李云萧", "等、等会！你、你有姐姐？"),
                f.t("？？？", "是啊，昨天你和姐姐她还见过面呢。"),
                f.t("李云萧", "昨、昨天？等一下……昨天我明明才刚到学校报道啊？"),
                f.t("？？？", "你怎么连这个都忘记了！李云萧！快给我点醒过来！"),
                f.t("李云萧", "痛！"),
                f.t("？？？", "怎么样，清醒一点了吗？！"),
                f.t("李云萧", "[66ccff]（脸上传来了剧烈的疼痛……）[-]"),
                f.t("李云萧", "嗯，嗯……"),
                f.t("李云萧", "[66ccff]（虽然很痛，可是我一点也记不起来了……）[-]"),
                f.t("李云萧", "[66ccff]（难道说，是我失忆了？我失去转学后的全部记忆？）[-]"),
                f.t("李云萧", "那个……现在……是哪一年？"),
                f.t("法警", "时间快到了，请辩护方尽快入庭！"),
                f.t("？？？", "好了，赶紧进去！"),
                f.t("李云萧", "哇……你别推我啊……"),
                f.FadeoutAllPic(),
                f.t("李云萧", "[66ccff]（于是，带着一堆疑问，本该在学校里过着高中生活的我……）[-]"),
                f.t("李云萧", "[66ccff]（就这样，被眼前这个自称我助手的人，拖了进去……）[-]"),
                f.CloseDialog()
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.FindTextScript("T11002");
        }

    }
}
