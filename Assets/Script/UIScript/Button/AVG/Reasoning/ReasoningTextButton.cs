using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;

/// <summary>
/// 自我推理界面 文字按钮
/// </summary>
public class ReasoningTextButton : BasicButton
{
    private ReasoningUIManager uiManager;
    public int id;

    public void SetUIManager(ReasoningUIManager manager)
    {
        this.uiManager = manager;
    }

    protected override void Execute()
    {
        uiManager.JudgeText(id);
    }
}
