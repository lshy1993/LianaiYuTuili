using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ExtraMusicReturn: BasicButton
{
    public TitleUIManager uiManager;

    protected override void Execute()
    {
        uiManager.CloseMusic();
    }
}
