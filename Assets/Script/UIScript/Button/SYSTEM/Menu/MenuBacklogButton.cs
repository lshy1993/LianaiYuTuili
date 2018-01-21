using UnityEngine;
using System.Collections;

public class MenuBacklogButton : BasicButton
{
    public SystemUIManager uiManager;

    protected override void Execute()
    {
        uiManager.fromButton = true;
        uiManager.OpenBacklog();
    }

    protected override void Hover(bool ishover)
    {
        uiManager.SetHelpInfo(ishover, "打开文字记录");
    }
}
