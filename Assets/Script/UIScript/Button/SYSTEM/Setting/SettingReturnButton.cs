using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingReturnButton : BasicButton
{
    public SystemUIManager uiManager;
    public UILabel label;

    protected override void Hover(bool ishover)
    {
        label.text = ishover ? "退出系统设置" : string.Empty;
    }

    protected override void Execute()
    {
        uiManager.Close();
    }
}
