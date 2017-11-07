using UnityEngine;
using System.Collections;

public class MenuSettingButton : BasicButton
{
    public SystemUIManager uiManager;

    protected override void OnClick()
    {
        base.OnClick();
        uiManager.OpenSetting();
    }

    protected override void OnHover(bool ishover)
    {
        base.OnHover(ishover);
        if (ishover)
        {
            uiManager.SetHelpInfo("设置游戏系统参数");
        }
        else
        {
            uiManager.SetHelpInfo("");
        }
    }
}
