using UnityEngine;
using System.Collections;

public class EduReturnButton : BasicButton
{
    public EduUIManager uiManager;

    protected override void Hover(bool isHover)
    {
        if (isHover)
        {
            uiManager.SetHelp(-3);
        }
        else
        {
            uiManager.SetHelp(-1);
        }
    }

    protected override void Execute()
    {
        uiManager.ReturnMap();
    }

}
