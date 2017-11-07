using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;

public class EnquireEvidenceButton : MonoBehaviour
{
    private EnquireUIManager uiManager;

    public Evidence evidence;
    //private DetectNode detectNode;

    public void SetUIManager(EnquireUIManager manager)
    {
        this.uiManager = manager;
    }

    void OnHover(bool ishover)
    {
        uiManager.SetHint(ishover, evidence);
    }

    void OnClick()
    {
        Debug.Log("Igiari!");
        Debug.Log(uiManager == null);
        uiManager.EnquirePresent(evidence);
    }
}
