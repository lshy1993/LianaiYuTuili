using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 标题界面按钮 游戏设置
/// </summary>
public class TitleSettingButton : BasicButton
{
    public TitleUIManager uiManager;

    protected override void Execute()
    {
        uiManager.ClickSetting();
    }
}
