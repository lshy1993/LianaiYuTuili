using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;

public class EnquireEvidenceButton : MonoBehaviour
{
    private EnquireUIManager uiManager;

    public string evidence;
    //private DetectNode detectNode;

    public void SetUIManager(EnquireUIManager manager)
    {
        this.uiManager = manager;
    }

    void OnClick()
    {
        Debug.Log("Igiari!");
        Debug.Log(uiManager == null);
        uiManager.EnquirePresent(evidence);
    }
}
