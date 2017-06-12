using UnityEngine;
using System.Collections;

public class SysLoadButton : MonoBehaviour
{
    public SystemUIManager uiManager;

    void OnClick()
    {
        if (Input.GetMouseButtonUp(1)) return;
        uiManager.OpenLoad();
    }

    void OnHover(bool ishover)
    {
        if (ishover)
        {
            uiManager.SetHelpInfo("读取游戏进度");
        }
        else
        {
            uiManager.SetHelpInfo("");
        }
        
    }

}
