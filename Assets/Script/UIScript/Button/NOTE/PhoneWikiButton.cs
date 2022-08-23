using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneWikiButton : BasicButton
{
    public NoteUIManager uiManager;

    protected override void Execute()
    {
        uiManager.OpenWiki();
    }

    protected override void Hover(bool ishover)
    {
        uiManager.SetHelpInfo(ishover, "打开帮助页");
    }
}
