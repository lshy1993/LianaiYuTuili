using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class S11_01 : TextScript
    {
        public S11_01(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                f.FadeinBackground("classroom"),
                f.TimeSwitch(manager.GetTodayText(),"高二（3）班"),
                f.OpenDialog(),
                f.t("李云萧", "[66ccff]（忙碌的早晨，大家已经开始陆陆续续地来到了教室。）[-]"),
                f.t("李云萧", "那么我也差不多先开始看……"),
                f.t("喵星人", "哟，早！"),
                f.t("李云萧", "早啊……我看你一副还睡迷糊的样子？"),
                f.t("喵星人", "还是……有点……想睡……ZZZZZZ！"),
                f.t("李云萧", "别在这种地方站着睡着啊！"),
                f.t("喵星人","你怎么样，习惯了吗？枫溪的作息生活？"),
                f.t("李云萧","已经适应了！再说我看过去是那种毫无适应能力的人嘛？"),
                f.t("喵星人","哈哈哈，是吗，那太好了！"),
                f.t("李云萧","怎么了？你这一副可疑的笑脸？"),
                f.t("喵星人","不不不，这完全是对室友的关心。"),
                f.t("喵星人","我记得，你之前应该不是住学校的吧？"),
                f.t("李云萧","是的，以前都是放学后自己回家的。"),
                f.t("喵星人","寄宿制的枫溪自然是不同的喵，我很担心同为室友的你的生活能力。"),
                f.t("李云萧","我有这么不堪吗？"),
                f.t("喵星人","只是保险，以防万一。"),
                f.t("陆菲菲", "喵星人！！老师找你！"),
                f.t("喵星人", "！！！"),
                f.t("喵星人", "哦！！！"),
                f.t("李云萧", "哇，垂死病中惊坐起！！"),
                f.t("喵星人", "我马上就去！"),
                f.t("李云萧", "走好不送。"),
                f.t("陆菲菲", "嘻嘻嘻……"),
                f.t("李云萧", "你在笑什么？"),
                f.t("陆菲菲", "哈哈哈！！"),
                f.t("李云萧", "不会，你是在骗他吧？"),
                f.t("陆菲菲", "是……是的，哈哈哈！！他、他那副着急的样子太好笑了！！"),
                f.t("李云萧", "哎……"),
                f.t("李云萧", "[66ccff]（过了一会，喵星人回来了……）[-]"),
                f.t("喵星人", "哈——哈——"),
                f.t("李云萧", "怎么了？？一脸狰狞？"),
                f.t("李云萧", "[66ccff]（不会是想报复班长吧，我可得拦住他。）[-]"),
                f.t("陆菲菲", "那、那个个个怎么了？？"),
                f.t("李云萧", "[66ccff]（班长说话都已经开始颤抖了……）[-]"),
                f.t("陆菲菲", "喵星人，对、对不——"),
                f.t("喵星人", "刚才路上遇到老师，被骂了一通……"),
                f.t("陆菲菲", "起，我不是……啊？"),
                f.t("喵星人", "多亏了你，我瞬间清醒了！"),
                f.t("陆菲菲", "哦……哦。"),                
                f.t("喵星人", "对了，李云萧！帮我个忙！"),
                f.t("李云萧", "什么忙？大清早的。"),
                f.t("喵星人", "刚才物理老师让我帮忙去抬教材，你陪我去吧！")
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("什么？", "S11_02");
            return nodeFactory.GetSelectNode(dic);
        }

    }
}
