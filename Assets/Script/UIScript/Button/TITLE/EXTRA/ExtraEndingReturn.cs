using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ExtraEndingReturn : BasicButton
{
    public TitleUIManager uiManger;

    protected override void Execute()
    {
        uiManger.CloseEnding();
    }
}