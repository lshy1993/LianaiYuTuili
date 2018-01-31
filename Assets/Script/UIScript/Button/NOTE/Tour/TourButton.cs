using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;

public class TourButton : BasicButton
{
    private TourUIManager uiManager;
    public string current;

    public void SetUIManager(TourUIManager manager)
    {
        this.uiManager = manager;
    }

    protected override void Execute()
    {
        uiManager.SetPlaceByName(current);
    }
}
