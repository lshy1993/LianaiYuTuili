using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectButton : BasicButton
{
    private SelectUIManager uiManager;
    private string text;

    protected override void Execute()
    {
        uiManager.Select(text);
    }

    public void SetUIManager(SelectUIManager ui)
    {
        this.uiManager = ui;
    }

    public void SetText(string text)
    {
        this.text = text;
    }
}
