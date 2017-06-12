using UnityEngine;
using System.Collections;

public class SysSaveButton : MonoBehaviour
{
    public SystemUIManager uiManager;

    void OnClick()
    {
        if (Input.GetMouseButtonUp(1)) return;
        uiManager.OpenSave();
    }

    void OnHover(bool ishover)
    {
        if (ishover)
        {
            uiManager.SetHelpInfo("储存当前游戏进度");
        }
        else
        {
            uiManager.SetHelpInfo("");
        }
    }
}
