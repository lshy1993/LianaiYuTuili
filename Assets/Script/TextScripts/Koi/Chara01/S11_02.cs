using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class S11_02 : TextScript
    {
        public S11_02(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {            
            pieces = new List<Piece>()
            {
                f.OpenDialog(),
                f.t("李云萧", "你说什么？？"),
                f.t("喵星人", "好！就这么定了！"),
                f.t("李云萧", "[66ccff]（喵星人完全不给我任何逃脱的机会，就把我拉走了……）[-]"),
                f.FadeoutAll(),
                f.FadeinBackground("corridor"),
                f.TimeSwitch(manager.GetTodayText(),"教学楼2楼走廊"),
                f.OpenDialog(),
                f.t("喵星人", "还好要搬的东西并不是很多……"),
                f.t("李云萧", "哇，这是学校购买了什么东西？"),
                f.t("喵星人", "大概是给新生准备的手册吧。"),
                f.t("李云萧", "新生？安全讲座之类的吗？"),
                f.t("喵星人", "差不多，再过一阵子就会召开一年一次的新生大会了。"),
                f.t("李云萧", "嗯……"),
                f.t("喵星人", "对了，你也要去参加。"),
                f.t("李云萧", "欸？等、等一下！我也要去？"),
                f.t("喵星人", "因为本质上来说，你也是新生啊。"),
                f.t("喵星人", "时间的话，大概就是下周了，记得去啊！"),
                f.t("李云萧", "我可以不去吗？"),
                f.t("喵星人", "嗯，硬要说的话，可以。"),
                f.t("喵星人", "高一新生，由于刚入枫溪，所以是强制参加的，必须签到。"),                
                f.t("喵星人", "你嘛，不去也不会有人知道的。"),
                f.t("李云萧", "这样啊，我得考虑一下……"),
                f.t("李云萧", "（正在考虑要不要去参加的时候，走廊上的班级里渐渐变得喧闹起来。）"),
                f.t("喵星人", "哦，正好赶上下课了！"),
                f.t("李云萧", "嗯？那个是？"),
                //播放CG
                //苏 立绘
                f.t("？？？", "……"),
                //立绘消失
                f.t("李云萧", "（刚才那个女生……好像在哪里见过她……）"),                
                f.t("喵星人", "说到新生会，我刚升入这里的时候也是一样。"),
                f.t("李云萧", "……"),
                f.t("喵星人","喂，你在听我说话吗？怎么呆住了。"),
                f.t("李云萧","喵星人，刚才那个女生，你有印象吗？"),
                f.t("喵星人","刚才？高一的新生？我没有认识的熟人……"),
                f.t("喵星人","啊！我知道了，你是看上那个女生了？"),
                f.t("李云萧","噗——怎么可能。"),
                f.t("李云萧","我只是觉得，我好像在哪里见过她……"),
                f.t("喵星人","你的青梅竹马？"),
                f.t("李云萧","在我的记忆里并没有这么一号人物……"),
                f.t("喵星人","难不成是天降……春天要来了呢，好好加油！"),
                f.t("李云萧","已经是9月的秋天了……"),
                f.t("喵星人","好了，不开你玩笑了，赶紧把东西台上去吧。"),
                f.t("李云萧","OK"),

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
