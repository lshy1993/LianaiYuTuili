using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailIconButton : BasicButton
{
    private MailUIManager uiManager;
    private string charaName;

    protected override void SE_Click()
    {
        //base.SE_Click();
    }

    protected override void SE_Hover()
    {
        base.SE_Hover();
    }

    protected override void Execute()
    {
        uiManager.SetMessage(charaName);
    }

    public void SetUIManager(MailUIManager ui)
    {
        this.uiManager = ui;
    }

    public void SetName(string chara)
    {
        this.charaName = chara;
    }
}
