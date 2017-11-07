using UnityEngine;
using System.Collections;

public class MenuNoteButton : BasicButton
{
    public SystemUIManager uiManager;

    protected override void OnClick()
    {
        base.OnClick();
        uiManager.OpenNote();
    }

    protected override void OnHover(bool ishover)
    {
        base.OnHover(ishover);
        if (ishover)
        {
            uiManager.SetHelpInfo("打开电子学生手册");
        }
        else
        {
            uiManager.SetHelpInfo("");
        }
    }
}
