using UnityEngine;
using System.Collections;

public class PhoneAppButton : BasicButton
{
    public NoteUIManager uiManger;

    protected override void Execute()
    {
        uiManger.OpenIndex();
    }

    protected override void Hover(bool ishover)
    {
        uiManger.SetHelpInfo(ishover, "打开应用界面");
    }
}
