using UnityEngine;
using System.Collections;

public class MenuNoteButton : BasicButton
{
    public SystemUIManager uiManager;

    protected override void Execute()
    {
        uiManager.OpenNote();
    }

    protected override void Hover(bool ishover)
    {
        uiManager.SetHelpInfo(ishover, "打开电子学生手册");
    }
}
