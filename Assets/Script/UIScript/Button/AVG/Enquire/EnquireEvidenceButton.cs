using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;

/// <summary>
/// 询问界面 证据按钮
/// </summary>
public class EnquireEvidenceButton : BasicButton
{
    private EnquireUIManager uiManager;

    private bool _currentOverState = false;
    public Evidence evidence;
    //private DetectNode detectNode;

    public void SetUIManager(EnquireUIManager manager)
    {
        this.uiManager = manager;
    }

    protected override void Hover(bool isOver)
    {
        if (_currentOverState == isOver)
            return;
        _currentOverState = isOver;
        uiManager.SetHint(isOver, evidence);
    }

    protected override void SE_Click()
    {

    }

    protected override void Execute()
    {
        uiManager.EnquirePresent(evidence);
    }
}
