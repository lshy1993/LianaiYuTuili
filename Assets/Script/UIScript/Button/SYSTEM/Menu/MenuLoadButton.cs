using UnityEngine;
using System.Collections;

public class MenuLoadButton : BasicButton
{
    public SystemUIManager uiManager;

    protected override void Execute()
    {
        uiManager.OpenLoad();
    }

    protected override void Hover(bool ishover)
    {
        uiManager.SetHelpInfo(ishover, "读取游戏进度");
    }

}
