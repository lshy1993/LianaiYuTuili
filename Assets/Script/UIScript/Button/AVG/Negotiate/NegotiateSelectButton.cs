using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegotiateSelectButton : BasicButton {
    private NegotiateUIManager uiManager;
    //入口设置
    public int entranceNo;

    public void SetUIManager(NegotiateUIManager manager)
    {
        this.uiManager = manager;
    }

    protected override void SE_Hover()
    {

    }

    protected override void SE_Click()
    {

    }

    protected override void Execute()
    {
        uiManager.Select(entranceNo);
    }
}
