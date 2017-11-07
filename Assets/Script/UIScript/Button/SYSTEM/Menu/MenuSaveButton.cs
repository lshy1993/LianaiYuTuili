using UnityEngine;
using System.Collections;

public class MenuSaveButton : BasicButton
{
    public SystemUIManager uiManager;

    protected override void OnClick()
    {
        base.OnClick();
        uiManager.OpenSave();
    }

    protected override void OnHover(bool ishover)
    {
        base.OnHover(ishover);
        if (ishover)
        {
            uiManager.SetHelpInfo("储存当前游戏进度");
        }
        else
        {
            uiManager.SetHelpInfo("");
        }
    }
}
