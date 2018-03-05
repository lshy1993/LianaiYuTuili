using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 仅在LOGO阶段有效
/// </summary>
public class Click_SkipLogo : MonoBehaviour
{
    public LogoUIManager uiManager;

    void OnClick()
    {
        uiManager.Skip();
    }
}
