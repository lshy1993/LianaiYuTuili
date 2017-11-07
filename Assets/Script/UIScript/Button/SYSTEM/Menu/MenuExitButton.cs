using UnityEngine;
using System.Collections;

public class MenuExitButton : BasicButton
{
    public SystemUIManager uiManager;

    protected override void OnClick()
    {
        base.OnClick();
        uiManager.OpenExit();
    }

    protected override void OnHover(bool ishover)
    {
        base.OnHover(ishover);
        if (ishover)
        {
            uiManager.SetHelpInfo("退出游戏");
        }
        else
        {
            uiManager.SetHelpInfo("");
        }
    }
}
