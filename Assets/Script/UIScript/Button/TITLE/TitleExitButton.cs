using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TitleExitButton : BasicButton
{
    public TitleUIManager uiManager;

    protected override void OnClick()
    {
        base.OnClick();
        uiManager.ClickExit();
    }
}
