using UnityEngine;
using System.Collections;

public class MenuSettingButton : BasicButton
{
    public SystemUIManager uiManager;

    protected override void Execute()
    {
        uiManager.fromButton = true;
        uiManager.OpenSetting();
    }

    protected override void Hover(bool ishover)
    {
        uiManager.SetHelpInfo(ishover, "设置游戏系统参数");
    }
}
