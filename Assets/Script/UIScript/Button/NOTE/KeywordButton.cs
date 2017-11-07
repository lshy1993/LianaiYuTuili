using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;

public class KeywordButton : MonoBehaviour
{
    private AppHelpUIManager uiManager;
    public string current;

    public void SetUIManager(AppHelpUIManager manager)
    {
        this.uiManager = manager;
    }

    void OnClick()
    {
        uiManager.SetExplanByName(current);
    }
}
