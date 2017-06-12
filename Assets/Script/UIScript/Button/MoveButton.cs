using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct;

public class MoveButton : MonoBehaviour
{
    public string place;
    private DetectUIManager uiManager;

    public void AssignUIManager(DetectUIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    void OnClick()
    {
        uiManager.MovePlace(place);
    }

}
