using UnityEngine;
using System.Collections;

public class SysExitButton : MonoBehaviour
{
    public SystemUIManager uiManager;

    void OnClick()
    {
        uiManager.OpenExit();
    }

    void OnHover(bool ishover)
    {
        if (ishover)
        {
            uiManager.SetHelpInfo("退出游戏");
        }
        else
        {
            uiManager.SetHelpInfo("");
        }
    }
}
