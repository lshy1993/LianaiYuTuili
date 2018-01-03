using UnityEngine;
using System.Collections;
using Assets.Script.UIScript;

/// <summary>
/// 对话框快捷按钮 打开菜单
/// </summary>
public class DialogMenuButton : BasicButton
{
    public GameObject sysPanel;

    protected override void Execute()
    {
        sysPanel.SetActive(true);
        sysPanel.GetComponent<SystemUIManager>().Open();
    }

    protected override void OnHover(bool ishover)
    {
    }
}
