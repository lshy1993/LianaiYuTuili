using UnityEngine;
using System.Collections;

public class PhoneEvidenceButton : BasicButton
{
    public NoteUIManager uiManager;
    
    protected override void Execute()
    {
        uiManager.OpenEvidence();
    }

    protected override void Hover(bool ishover)
    {
        uiManager.SetHelpInfo(ishover, "打开事件簿界面");
    }
}
