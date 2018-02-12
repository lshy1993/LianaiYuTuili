using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButton : BasicButton
{
    public MusicUIManager uiManager;

    protected override void SE_Click()
    {
        //base.SE_Click();
    }

    protected override void SE_Hover()
    {
        //base.SE_Hover();
    }

    protected override void Execute()
    {
        uiManager.PlayMusicAt(transform.Find("Label").GetComponent<UILabel>().text);
    }
}
