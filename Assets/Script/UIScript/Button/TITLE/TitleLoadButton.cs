using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 标题界面按钮 读取游戏
/// </summary>
public class TitleLoadButton : BasicButton
{
    public TitleUIManager uiManager;

    protected override void Execute()
    {
        uiManager.ClickLoad();
    }
}
