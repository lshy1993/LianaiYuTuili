using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EduIconButton : MonoBehaviour {

    public EduUIManager uiManager;
    public int eduID;

    void OnHover(bool isHover)
    {
        if (isHover)
        {
            uiManager.SetInfoHelp(eduID);
        }
        else
        {
            uiManager.SetInfoHelp(-1);
        }

    }
}
