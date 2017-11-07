using UnityEngine;
using System.Collections;

public class MenuLoadButton : BasicButton
{
    public SystemUIManager uiManager;

    protected override void OnClick()
    {
        base.OnClick();
        uiManager.OpenLoad();
    }

    protected override void OnHover(bool ishover)
    {
        base.OnHover(ishover);
        if (ishover)
        {
            uiManager.SetHelpInfo("读取游戏进度");
        }
        else
        {
            uiManager.SetHelpInfo("");
        }
        
    }

}
