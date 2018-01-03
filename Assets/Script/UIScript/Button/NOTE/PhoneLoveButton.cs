using UnityEngine;
using System.Collections;

public class PhoneLoveButton : BasicButton
{
    public NoteUIManager uiManager;

    protected override void Execute()
    {
        uiManager.OpenLove();
    }

    protected override void Hover(bool ishover)
    {
        uiManager.SetHelpInfo(ishover,"打开联系人界面");
    }
}