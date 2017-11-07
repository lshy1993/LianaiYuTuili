using UnityEngine;
using System.Collections;

public class MenuBacklogButton : BasicButton
{
    public SystemUIManager uiManager;

    protected override void OnClick()
    {
        base.OnClick();
        //if (Input.GetMouseButtonUp(1)) return;
        uiManager.OpenBacklog();
    }

    protected override void OnHover(bool ishover)
    {
        base.OnHover(ishover);
        if (ishover)
        {
            uiManager.SetHelpInfo("打开文字记录");
        }
        else
        {
            uiManager.SetHelpInfo("");
        }
    }
}
