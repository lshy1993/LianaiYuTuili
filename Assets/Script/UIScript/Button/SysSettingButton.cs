using UnityEngine;
using System.Collections;

public class SysSettingButton : MonoBehaviour
{
    public SystemUIManager uiManager;

    void OnClick()
    {
        if (Input.GetMouseButtonUp(1)) return;
        uiManager.OpenSetting();
    }

    void OnHover(bool ishover)
    {
        if (ishover)
        {
            uiManager.SetHelpInfo("设置游戏系统参数");
        }
        else
        {
            uiManager.SetHelpInfo("");
        }
    }
}
