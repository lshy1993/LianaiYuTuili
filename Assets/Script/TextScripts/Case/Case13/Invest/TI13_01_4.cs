using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class TI13_01_4 : TextScript
    {
        public TI13_01_4(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }

        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                //*调查->保险桌主面板
                f.t("李云萧", "这就是所谓的“保险桌”了。"),
                f.t("苏梦忆", "看起来真的和普通桌子没什么区别。"),
                f.t("李云萧", "在桌板被打开后，就能看到保险箱的密码面板了。"),
                f.t("李云萧", "保险箱的面板采用的是抽屉式的，成功解锁后就可以向下打开了。"),
                f.t("苏梦忆", "李云萧，那个是密码按钮按钮吧？"),
                f.t("李云萧", "没错，非常常见的电子锁。"),
                f.t("苏梦忆", "那右边的那个，就是钥匙孔了吧？"),
                f.t("李云萧", "不过，钥匙孔外侧的记号是什么？"),
                f.t("苏梦忆", "你是说这3个吗？从左到右分别是“ON”、“OFF”、“ON”。"),
                f.t("李云萧", "如果ON表示打开的意思的话，为什么有两个呢？"),
                f.t("苏梦忆", "大概是向左转或向右转都可以的意思吧。"),
                //证据-钥匙孔
                f.t("李云萧", "这么做有什么意义吗……")
                /*
                这里要跳回现场调查
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
