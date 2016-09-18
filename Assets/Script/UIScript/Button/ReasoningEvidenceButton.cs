﻿using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;

public class ReasoningEvidenceButton : MonoBehaviour
{
    private ReasoningUIManager uiManager;
    public Evidence current;

    public void SetUIManager(ReasoningUIManager manager)
    {
        this.uiManager = manager;
    }

    void OnHover()
    {
        uiManager.HoverEvidence(current);
    }

    void OnClick()
    {
        Debug.Log("接招!");
        uiManager.JudgeEvidence(current);
    }
}
