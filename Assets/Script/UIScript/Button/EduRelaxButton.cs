using UnityEngine;
using System.Collections;

public class EduRelaxButton : MonoBehaviour {

    public EduUIManager uiManager;

    void OnHover(bool isHover)
    {
        if (isHover)
        {
            uiManager.SetHelp(-2);
        }
        else
        {
            uiManager.SetHelp(-1);
        }
    }

    void OnClick()
    {
        uiManager.RelaxExecute();
    }
}
