using UnityEngine;
using System.Collections;

public class EduRelaxButton : BasicButton
{
    public EduUIManager uiManager;

    protected override void Hover(bool isHover)
    {
        if (isHover)
        {
            uiManager.SetHelp(-2);
        }
        else
        {
            uiManager.SetHelp(-1);
        }
    }

    protected override void Execute()
    {
        uiManager.RelaxExecute();
    }
}
