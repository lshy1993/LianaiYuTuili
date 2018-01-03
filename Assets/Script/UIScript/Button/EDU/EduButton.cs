using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;

public class EduButton : BasicButton
{
    private EduUIManager uiManager;
    public int eduID;

    public void SetUIManager(EduUIManager manager)
    {
        this.uiManager = manager;
    }

    protected override void Hover(bool isHover)
    {
        if (isHover)
        {
            uiManager.SetHelp(eduID);
        }
        else
        {
            uiManager.SetHelp(-1);
        }

    }

    protected override void Execute()
    {
        uiManager.Execute(eduID);
    }
}
