using UnityEngine;
using System.Collections;

public class MenuExitButton : BasicButton
{
    public SystemUIManager uiManager;

    protected override void Execute()
    {
        uiManager.OpenExit();
    }

    protected override void Hover(bool ishover)
    {
        uiManager.SetHelpInfo(ishover, "退出游戏");
    }
}
