using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;

public class PhoneEvidenceButton : MonoBehaviour
{
    private PhoneUIManager uiManager;
    public string name;

    public void SetUIManager(PhoneUIManager manager)
    {
        this.uiManager = manager;
    }

    void OnClick()
    {
        uiManager.EvidenceInfoFresh(name);
    }
}
