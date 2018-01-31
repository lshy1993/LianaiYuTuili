using UnityEngine;
using System.Collections;

public class PhoneMailButton : BasicButton
{
    public NoteUIManager uiManager;

    protected override void Execute()
    {
        uiManager.OpenMail();
    }

    protected override void Hover(bool ishover)
    {
        uiManager.SetHelpInfo(ishover, "查看校内邮件");
    }
}
