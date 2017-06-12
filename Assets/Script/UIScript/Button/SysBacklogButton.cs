using UnityEngine;
using System.Collections;

public class SysBacklogButton : MonoBehaviour
{
    public SystemUIManager uiManager;

    void OnClick()
    {
        if (Input.GetMouseButtonUp(1)) return;
        uiManager.OpenBacklog();
    }

    void OnHover(bool ishover)
    {
        if (ishover)
        {
            uiManager.SetHelpInfo("打开文字记录");
        }
        else
        {
            uiManager.SetHelpInfo("");
        }
    }
}
