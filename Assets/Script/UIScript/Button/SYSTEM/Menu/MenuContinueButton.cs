using UnityEngine;
using System.Collections;

public class MenuContinueButton : BasicButton
{
    public SystemUIManager uiManager;

    protected override void Execute()
    {
        uiManager.Close();
    }

    protected override void Hover(bool ishover)
    {
        uiManager.SetHelpInfo(ishover, "继续游戏");
    }
}
