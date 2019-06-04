using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class T13_003 : TextScript
    {
        public T13_003(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //——背景 走廊活动教室——
                f.t("柳萱", "如何？"),
                f.t("李云萧", "事件的大致，我已经清楚了，但是还缺少线索。"),
                f.t("柳萱", "这件事就全权委托给推理社的两位了。"),
                f.t("柳萱", "这个教室一直保持着早上刚来时候的样子，没有任何人动过。"),
                f.t("柳萱", "接下来我有点事情，请随意调查吧。"),
                f.t("苏梦忆", "我们一定会找回书法的。"),
                //立绘切出
                f.t("苏梦忆", "李云萧，话虽然这么说，可是如果被拿到校外就找不回来了。"),
                f.t("李云萧", "我想这是不可能的。"),
                f.t("苏梦忆", "为什么？虽然我们学校是封闭的，但也不排出有人员混进来的可能。"),
                f.t("李云萧", "因为打不开那个“保险桌”。"),
                f.t("李云萧", "要打开“保险桌”，钥匙和密码缺一不可。"),
                f.t("李云萧", "钥匙虽然是任何人都可以拿的，但是若不知道它是做什么的，就没有意义。"),
                f.t("李云萧", "然后，密码的话，知道它的人只有社长和副社长。"),
                f.t("苏梦忆", "这么看来，会不会是副社长她？"),
                f.t("李云萧", "有这个可能，但是还不够。"),
                f.t("苏梦忆", "那么，我们开始吧，调查。"),
                f.t("李云萧", "要调查的有这间教室，还有就是……"),
                f.t("李云萧", "副社长陈雨涵。"),
                /*
                这里跳转调查
                */
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.FindTextScript("T11002");
        }

    }
}
