using UnityEngine;
using System.Collections;

public class MenuSaveButton : BasicButton
{
    public SystemUIManager uiManager;

    protected override void Execute()
    {
        uiManager.OpenSave();
    }

    protected override void Hover(bool ishover)
    {
        uiManager.SetHelpInfo(ishover, "储存当前游戏进度");
    }
}
