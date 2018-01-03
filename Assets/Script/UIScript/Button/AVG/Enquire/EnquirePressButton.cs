using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;

/// <summary>
/// 询问界面 威慑按钮
/// </summary>
public class EnquirePressButton : BasicButton
{
    public EnquireUIManager uiManager;

    protected override void SE_Click()
    {
    }

    protected override void Execute()
    {
        //Debug.Log("Matta!");
        uiManager.EnquirePress();
    }

    protected override void OnHover(bool ishover)
    {
    }
}
