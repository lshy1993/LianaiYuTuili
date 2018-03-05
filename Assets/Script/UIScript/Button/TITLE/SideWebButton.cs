using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 侧边存档文件夹按钮
/// </summary>
public class SideWebButton : BasicButton
{
    private string url = "http://liantui.moe";

    protected override void Execute()
    {
        Application.OpenURL(url);
    }
    
}
