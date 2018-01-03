using UnityEngine;
using System.Collections;

/// <summary>
/// 作为游戏内所有按钮的基类
/// </summary>
public class BasicButton : MonoBehaviour
{
    protected SoundManager sm;

    private void Awake()
    {
        sm = GameObject.Find("GameManager").GetComponent<SoundManager>();
    }

    /// <summary>
    /// 按钮悬停效果触发
    /// </summary>
    /// <param name="ishover"></param>
    protected virtual void OnHover(bool ishover)
    {
        if (!GetComponent<UIButton>().enabled) return;
        if (UICamera.currentTouchID == -2 || UICamera.currentTouchID == -3) return;
        if (ishover)
        {
            //if (UICamera.currentTouchID == -2 || UICamera.currentTouchID == -3) return;
            SE_Hover();
        }
        Hover(ishover);
    }

    /// <summary>
    /// 按钮点击的音效触发
    /// </summary>
    protected virtual void OnClick()
    {
        if (!GetComponent<UIButton>().enabled) return;
        if (UICamera.currentTouchID != -1) return;
        SE_Click();
        Execute();
    }

    /// <summary>
    /// 修改默认悬停音效
    /// </summary>
    protected virtual void SE_Hover()
    {
        sm.SetSystemSE("SE_hover");
    }

    /// <summary>
    /// 修改默认点击音效
    /// </summary>
    protected virtual void SE_Click()
    {
        sm.SetSystemSE("SE_confirm");
    }

    /// <summary>
    /// 悬停触发内容
    /// </summary>
    protected virtual void Hover(bool ishover)
    {

    }

    /// <summary>
    /// 点击触发内容
    /// </summary>
    protected virtual void Execute()
    {

    }
}
