using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;

public class TourButton : MonoBehaviour
{
    private AppTourUIManager uiManager;
    public string current;

    public void SetUIManager(AppTourUIManager manager)
    {
        this.uiManager = manager;
    }

    void OnClick()
    {
        uiManager.SetPlaceByName(current);
    }
}
