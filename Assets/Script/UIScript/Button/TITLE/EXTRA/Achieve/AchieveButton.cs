using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 成就小方格按钮
/// </summary>
public class AchieveButton : BasicButton
{
    public EndingUIManager uiManager;
    public int id;

    protected override void Execute()
    {
        uiManager.ClickAchieveAt(id);
    }

    protected override void SE_Click()
    {
        //base.SE_Click();
    }

    protected override void SE_Hover()
    {
        //base.SE_Hover();
    }
}
