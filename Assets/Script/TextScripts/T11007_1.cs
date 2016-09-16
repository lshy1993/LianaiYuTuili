using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class T11007_1 : TextScript
    {
        public T11007_1(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "苏梦忆，询问是指？"),
                //——背景 助手侧——
                //——立绘 苏梦忆侧面——
                f.t("【苏梦忆】", "这你都忘记了？"),
                f.t("【苏梦忆】", "所谓询问，就是对证人的证词进行检查。"),
                f.t("【苏梦忆】", "证人有时候，会因为某些原因而说谎。"),
                f.t("【苏梦忆】", "就算证人没有撒谎，也会有记忆错误的时候。"),
                f.t("【苏梦忆】", "指出证词与事实的矛盾，寻找真相，就是你的工作。"),
                f.t("【李云萧】", "好专业……"),
                f.t("【苏梦忆】", "这些话是我刚进来的时候，你教给我的呢！"),
                f.t("【李云萧】", "这样啊……"),
                f.t("【苏梦忆】", "武器就是我们手上的调查记录。"),
                f.t("【苏梦忆】", "将证词里与事实不相符的地方，指给审判长看吧。"),
                f.t("【苏梦忆】", "选定有问题的证词后，点击“指证”按钮就会进入证据选择界面。"),
                f.t("【苏梦忆】", "选择能显示证词矛盾的证据，确认后，点击右上方的按钮进行指证。"),
                f.t("【苏梦忆】", "当然，也会出现指证错误的情况，那时就再看一遍证词，重新选择证物。"),
                f.t("【苏梦忆】", "但是要注意，绝对不能一直出错，不然审判长就会终止审判。"),
                f.t("【苏梦忆】", "到那个时候，审理就结束了，我们的委托人也就完了！"),
                f.t("【苏梦忆】", "所以，大胆询问、小心指证吧！"),
                f.t("【李云萧】", "知道了，我试试看。"),
                //——背景 法庭-审判长侧——
                //——立绘 审判长——
                f.t("【审判长】", "辩护人，请询问。",() => pieces.Count)
                /*
                这里要跳转【询问】
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
