using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 标题界面按钮 特典
/// </summary>
public class TitleExtraButton : BasicButton
{
    public TitleUIManager uiManager;

    protected override void Execute()
    {
        uiManager.ClickExtra();
    }
}
