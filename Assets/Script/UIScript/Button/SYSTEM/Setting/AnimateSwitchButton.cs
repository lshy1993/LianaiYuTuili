using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AnimateSwitchButton : BasicButton
{
    public UILabel hint;
    public GraphicSettingUIManager uiManager;

    protected override void OnHover(bool ishover)
    {
        base.OnHover(ishover);
        hint.text = ishover ? "设置【动画效果】是否开启" : string.Empty;
    }

    protected override void OnClick()
    {
        base.OnClick();
        uiManager.SwitchAnime();
    }
}
