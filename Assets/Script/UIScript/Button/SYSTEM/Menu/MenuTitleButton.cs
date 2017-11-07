using UnityEngine;
using System.Collections;

public class MenuTitleButton : BasicButton
{
    public SystemUIManager uiManager;

    protected override void OnClick()
    {
        base.OnClick();
        uiManager.OpenTitle();
    }

    protected override void OnHover(bool ishover)
    {
        base.OnHover(ishover);
        if (ishover)
        {
            uiManager.SetHelpInfo("返回游戏标题画面");
        }
        else
        {
            uiManager.SetHelpInfo("");
        }
    }
}
