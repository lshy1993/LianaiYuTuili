using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneTourButton : BasicButton
{
    public NoteUIManager uiManager;

    protected override void Execute()
    {
        uiManager.OpenTour();
    }

    protected override void Hover(bool ishover)
    {
        uiManager.SetHelpInfo(ishover, "打开夕云游");
    }
}
