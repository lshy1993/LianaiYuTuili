using UnityEngine;
using System.Collections;

public class SysContinueButton : MonoBehaviour
{
    public SystemUIManager uiManager;

    void OnClick()
    {
        uiManager.Close();
    }

    void OnHover(bool ishover)
    {
        if (ishover)
        {
            uiManager.SetHelpInfo("继续游戏");
        }
        else
        {
            uiManager.SetHelpInfo("");
        }
        
    }
}
