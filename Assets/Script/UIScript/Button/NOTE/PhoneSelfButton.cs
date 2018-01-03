using UnityEngine;
using System.Collections;

public class PhoneSelfButton : BasicButton
{
    public NoteUIManager uiManager;

    protected override void Execute()
    {
        uiManager.OpenSelf();
    }

    protected override void Hover(bool ishover)
    {
        uiManager.SetHelpInfo(ishover, "打开学生信息页");
    }
}
