using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class T12001 : TextScript
    {
        public T12001(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps):base(gVars, lVars, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——背景 教室-李云萧视角——
                f.t("【李云萧】", "（转入这所学校已经2周了，但是……）"),
                f.t("【女生】", "喂喂，告诉我，你原先学校的校服是这样的吗？"),
                f.t("【李云萧】", "是的。（早知道就不穿这一件了……）"),
                f.t("【女生】", "和我们的完全不一样额诶。"),
                f.t("【女生】", "是啊，好少见的款式。"),
                f.t("【李云萧】", "（早上还是会有不少人围过来……）"),
                f.t("【女生】", "诶，那你的初中是？"),
                f.t("【李云萧】", "我的初中，是……"),
                f.t("【女生】", "那、那——"),
                //——立绘 喵星人——
                f.t("【喵星人】", "嘿！你们有什么问题来问我吧喵！我可是知道李云萧的全部事情喵！"),
                f.t("【女生】", "是真的吗？那我问问你……"),
                f.t("【李云萧】", "喵星人，你什么时候这么了解我了？"),
                f.t("【喵星人】", "嘿嘿！"),
                //——背景变黑——
                f.t("【李云萧】", "这家伙的名字叫苗星任，因为谐音的关系，叫他喵星人。"),
                f.t("【李云萧】", "喵星人知道学校的任何事情（自称），被大家叫作华欣的数据库。"),
                f.t("【李云萧】", "两周前，我第一次来到这里的时候，也是他第一个过来……"),
                //——立绘 苏梦忆——
                f.t("【苏梦忆】", "好啦，别闹了！"),
                f.t("【苏梦忆】", "今天要进行语文的单元测试，你们知道了吗？"),
                //——背景变黑——
                f.t("【李云萧】", "她的名字叫苏梦忆，是坐在我前面的女生。"),
                f.t("【李云萧】", "之前和她就有过一面之缘，只是没有想到，我会被分配到这个班。"),
                //——立绘 喵星人——
                f.t("【喵星人】", "你说什么喵？"),
                f.t("【苏梦忆】", "单元测试，上午最后一节课。"),
                f.t("【喵星人】", "我怎么没有听说过？"),
                f.t("【李云萧】", "上课你肯定没听。"),
                f.t("【喵星人】", "怎么连你也知道？"),
                f.t("【李云萧】", "（顺带一提，喵星人的语文成绩貌似不是那么好。）"),
                f.t("【喵星人】", "李云萧，快救我喵！"),
                f.t("【李云萧】", "我才刚转过来，第一次参加这里的考试……"),
                f.t("【喵星人】", "苏梦忆，求你了喵！"),
                f.t("【苏梦忆】", "不行！考试这么重要的事情，我不能帮你。"),
                f.t("【喵星人】", "别介样！/(ㄒoㄒ)/~~",() => pieces.Count)
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
