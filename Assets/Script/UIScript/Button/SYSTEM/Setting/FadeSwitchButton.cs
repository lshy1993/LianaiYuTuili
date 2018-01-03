using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FadeSwitchButton : BasicButton
{
    public UILabel hint;
    public GraphicSettingUIManager uiManager;

    protected override void Hover(bool ishover)
    {
        hint.text = ishover ? "设置【画面效果】是否开启" : string.Empty;
    }

    protected override void Execute()
    {
        uiManager.SwitchFading();
    }
}
