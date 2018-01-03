using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct;

/// <summary>
/// 侦探模式 可移动选项 按钮
/// </summary>
public class MoveButton : BasicButton
{
    public string place;
    private DetectUIManager uiManager;

    public void AssignUIManager(DetectUIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    protected override void Execute()
    {
        uiManager.MovePlace(place);
    }

}
