using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;

public class EduButton : MonoBehaviour
{
    private EduUIManager uiManager;
    public int eduID;

    public void SetUIManager(EduUIManager manager)
    {
        this.uiManager = manager;
    }

    void OnHover(bool isHover)
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

    void OnClick()
    {
        uiManager.Execute(eduID);
    }
}
