using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhoneCalButton : BasicButton
{
    public NoteUIManager uiManager;

    protected override void Execute()
    {
        uiManager.OpenCalendar();
    }

    protected override void Hover(bool ishover)
    {
        uiManager.SetHelpInfo(ishover, "打开校历");
    }
}

