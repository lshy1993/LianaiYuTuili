using UnityEngine;
using System.Collections;

public class PhoneMomentButton : BasicButton
{
    public NoteUIManager uiManager;

    protected override void Execute()
    {
        uiManager.OpenMoment();
    }

    protected override void Hover(bool ishover)
    {
        uiManager.SetHelpInfo(ishover, "查看常春林");
    }
}
