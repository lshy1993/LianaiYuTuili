﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AvatarSwitchButton : BasicButton
{
    public UILabel hint;
    public GraphicSettingUIManager uiManager;

    protected override void OnHover(bool ishover)
    {
        base.OnHover(ishover);
        hint.text = ishover ? "设置【人物头像与表情】是否开启" : "";
    }

    protected override void OnClick()
    {
        base.OnClick();
        uiManager.SwitchAvatar();
    }
}
