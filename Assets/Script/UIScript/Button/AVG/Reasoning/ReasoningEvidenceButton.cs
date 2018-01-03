using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;

/// <summary>
/// 自我推理界面 证据按钮
/// </summary>
public class ReasoningEvidenceButton : BasicButton
{
    private ReasoningUIManager uiManager;
    public Evidence current;

    public void SetUIManager(ReasoningUIManager manager)
    {
        this.uiManager = manager;
    }

    protected override void Hover(bool ishover)
    {
        uiManager.HoverEvidence(ishover, current.introduction);
    }

    protected override void Execute()
    {
        //Debug.Log("接招!");
        uiManager.JudgeEvidence(current);
    }
}
