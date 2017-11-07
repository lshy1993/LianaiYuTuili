using UnityEngine;
using System.Collections;

public class MenuContinueButton : BasicButton
{
    public SystemUIManager uiManager;

    protected override void OnClick()
    {
        base.OnClick();
        uiManager.Close();
    }

    protected override void OnHover(bool ishover)
    {
        base.OnHover(ishover);
        if (ishover)
        {
            uiManager.SetHelpInfo("继续游戏");
        }
        else
        {
            uiManager.SetHelpInfo("");
        }
        
    }
}
