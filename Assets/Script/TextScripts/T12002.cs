using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class T12002 : TextScript
    {
        public T12002(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps):base(gVars, lVars, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——背景 教室-李云萧视角——
                //——SE 下课铃——
                f.t("【老师】", "好了，停笔！交卷！"),
                f.t("【李云萧】", "（果然，还是好难……语文，我也不行……）"),
                //——立绘 喵星人——
                f.t("【喵星人】", "完了喵_(:з」∠)_"),
                f.t("【李云萧】", "呼——没这么严重吧？"),
                //——立绘 苏梦忆——
                f.t("【苏梦忆】", "李云萧，我们一起去食堂吧！"),
                f.t("【喵星人】", "(´；ω；`)"),
                f.t("【苏梦忆】", "喵星人他这是怎么了？"),
                f.t("【李云萧】", "大概是对人生绝望了。"),
                f.t("【喵星人】", "呜……(´；ω；`)"),
                f.t("【李云萧】", "别想了，都下课了。"),
                f.t("【喵星人】", "你们两个先去吧，别管我了，我还有点事情。"),
                f.t("【苏梦忆】", "诶，可是……"),
                f.t("【李云萧】", "苏梦忆，走吧。"),
                //——背景 教室走廊——
                f.t("【苏梦忆】", "喵星人他，到底干什么去了？"),
                f.t("【李云萧】", "不知道……但愿他别做什么傻事。"),
                f.t("【苏梦忆】", "别这么说，喵星人可是非常乐观的。"),
                f.t("【李云萧】", "哦？你和他从以前就认识了？"),
                f.t("【苏梦忆】", "我们初中就认识了，所以我很清楚，他绝不会犯傻的。"),
                f.t("【李云萧】", "是么……"),
                f.t("【苏梦忆】", "我们去暑假里刚刚开业的三楼吧！"),
                f.t("【李云萧】", "行。"),
                //——背景 食堂——
                f.t("【李云萧】", "（不愧是私立的重点学校，就连食堂也和其他地方不一样。）"),
                f.t("【李云萧】", "（另外，新开的三楼食堂，好像是类似西餐厅的配置。）"),
                f.t("【李云萧】", "（这还是学生食堂么……）"),
                f.t("【李云萧】", "你看，好像整个语文办公室的老师都来了。"),
                //——立绘 苏梦忆——
                f.t("【苏梦忆】", "我也好想去教师食堂……"),
                f.t("【李云萧】", "教师食堂原来也在三楼么？"),
                f.t("【苏梦忆】", "嗯……就在上来楼梯的右侧，我们现在是在另一侧的学生食堂。"),
                f.t("【李云萧】", "难怪总会在楼梯上遇到各种老师……"),
                f.t("【苏梦忆】", "暑假之前，我从来都没有上来过呢。"),
                f.t("【李云萧】", "（午饭的时间很快就结束了，只是没想到，我的担忧很快便成为了现实。）",() => pieces.Count)
                //——背景 消失——
                /*
                这里要不要时间切换
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
