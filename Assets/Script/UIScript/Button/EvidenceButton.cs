using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;


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
