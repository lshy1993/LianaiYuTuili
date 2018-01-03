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

    public Evidence evidence;
    //private DetectNode detectNode;

    public void SetUIManager(EnquireUIManager manager)
    {
        this.uiManager = manager;
    }

    protected override void Hover(bool ishover)
    {
        uiManager.SetHint(ishover, evidence);
    }

    protected override void SE_Click()
    {

    }

    protected override void Execute()
    {
        //Debug.Log("Igiari!");
        //Debug.Log(uiManager == null);
        uiManager.EnquirePresent(evidence);
    }
}
