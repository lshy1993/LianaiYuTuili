using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;

public class ReasoningEvidenceButton : MonoBehaviour
{
    private ReasoningUIManager uiManager;
    public string evidence;

    public void SetUIManager(ReasoningUIManager manager)
    {
        this.uiManager = manager;
    }

    void OnHover()
    {
        uiManager.HoverEvidence(evidence);
    }

    void OnClick()
    {
        Debug.Log("接招!");
        //Debug.Log(uiManager == null);
        uiManager.JudgeEvidence(evidence);
    }
}
