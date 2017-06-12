using UnityEngine;
using System.Collections;

public class EduReturnButton : MonoBehaviour {

    public EduUIManager uiManager;

    void OnHover(bool isHover)
    {
        if (isHover)
        {
            uiManager.SetHelp(-3);
        }
        else
        {
            uiManager.SetHelp(-1);
        }
    }

    void OnClick()
    {
        uiManager.ReturnMap();
    }
}
