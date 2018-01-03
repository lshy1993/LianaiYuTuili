using UnityEngine;
using System.Collections;

public class MenuTitleButton : BasicButton
{
    public SystemUIManager uiManager;

    protected override void Execute()
    {
        uiManager.OpenTitle();
    }

    protected override void Hover(bool ishover)
    {
        uiManager.SetHelpInfo(ishover, "返回游戏标题画面");
    }
}
