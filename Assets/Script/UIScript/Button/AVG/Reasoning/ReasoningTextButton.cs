using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;

public class ReasoningTextButton : MonoBehaviour
{
    private ReasoningUIManager uiManager;
    public int id;

    public void SetUIManager(ReasoningUIManager manager)
    {
        this.uiManager = manager;
    }

    void OnClick()
    {
        uiManager.JudgeText(id);
    }
}
