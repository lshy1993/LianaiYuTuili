using UnityEngine;
using System.Collections;
using Assets.Script.UIScript;

/// <summary>
/// 对话框快捷按钮 打开菜单
/// </summary>
public class DialogMenuButton : BasicButton
{
    public PanelSwitch panelSwitch;

    protected override void Execute()
    {
        panelSwitch.OpenMenu();
    }

    protected override void OnHover(bool ishover)
    {
    }
}
