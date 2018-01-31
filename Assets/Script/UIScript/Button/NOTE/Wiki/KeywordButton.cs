using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;

public class KeywordButton : BasicButton
{
    private WikiUIManager uiManager;
    public string current;

    public void SetUIManager(WikiUIManager manager)
    {
        this.uiManager = manager;
    }

    protected override void Execute()
    {
        uiManager.SetExplanByName(current);
    }
}
