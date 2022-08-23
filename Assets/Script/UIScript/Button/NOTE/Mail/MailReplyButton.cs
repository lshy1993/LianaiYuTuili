using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailReplyButton : BasicButton {

    private MailUIManager uiManager;
    //当前选项的唯一编号
    private int id;

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
        uiManager.Reply(id);
    }

    public void SetUIManager(MailUIManager ui)
    {
        uiManager = ui;
    }
    public void SetChoiceID(int x)
    {
        this.id = x;
    }
}
