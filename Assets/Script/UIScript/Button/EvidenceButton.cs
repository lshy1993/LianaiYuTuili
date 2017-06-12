using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;

public class EvidenceButton : MonoBehaviour
{
    private EvidenceUIManager uiManager;
    public Evidence current;

    public void SetUIManager(EvidenceUIManager manager)
    {
        this.uiManager = manager;
    }

    void OnClick()
    {
        uiManager.EvidenceInfoFresh(current);
    }
}
