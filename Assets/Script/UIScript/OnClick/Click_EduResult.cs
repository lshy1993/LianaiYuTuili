using UnityEngine;
using System.Collections;

public class Click_EduResult : MonoBehaviour
{
    public EduUIManager uiManager;

    void OnClick()
    {
        uiManager.NextDay();
    }

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
}
