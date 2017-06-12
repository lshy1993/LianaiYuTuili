using UnityEngine;
using System.Collections;

public class SysTitleButton : MonoBehaviour
{
    public SystemUIManager uiManager;

    void OnClick()
    {
        uiManager.OpenTitle();
    }

    void OnHover(bool ishover)
    {
        if (ishover)
        {
            uiManager.SetHelpInfo("返回游戏标题画面");
        }
        else
        {
            uiManager.SetHelpInfo("");
        }
    }
}
