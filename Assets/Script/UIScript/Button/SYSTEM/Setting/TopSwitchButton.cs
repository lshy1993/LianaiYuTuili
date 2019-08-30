using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TopSwitchButton : BasicButton
{
    public UILabel hint;
    public GraphicSettingUIManager uiManager;

    protected override void Hover(bool ishover)
    {
        hint.text = ishover ? "设置画面是否【处于最前】（该功能未实装）" : string.Empty;
    }

    protected override void Execute()
    {
        uiManager.SwitchTop();
    }
}
