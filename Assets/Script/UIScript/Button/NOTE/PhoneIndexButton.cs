using UnityEngine;
using System.Collections;

public class PhoneIndexButton : BasicButton
{

    public NoteUIManager uiManager;

    protected override void Execute()
    {
        uiManager.OpenIndex();
    }

    protected override void Hover(bool ishover)
    {
        uiManager.SetHelpInfo(ishover, "返回应用界面");
    }
}
