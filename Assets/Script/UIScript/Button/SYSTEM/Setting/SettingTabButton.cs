using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//设置界面 标签按钮通用脚本
public class SettingTabButton : BasicButton
{
    public SettingUIManager uiManager;

    protected override void Hover(bool ishover)
    {
        string str = ishover ? this.transform.name : string.Empty;
        uiManager.SetHint(str);
    }

    protected override void Execute()
    {
        string str = this.transform.name;
        uiManager.SwitchTab(str);
    }
}
