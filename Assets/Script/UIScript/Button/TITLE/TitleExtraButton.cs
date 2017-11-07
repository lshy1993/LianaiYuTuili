using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TitleExtraButton : BasicButton
{
    public TitleUIManager uiManager;

    protected override void OnClick()
    {
        base.OnClick();
        uiManager.ClickExtra();
    }
}
