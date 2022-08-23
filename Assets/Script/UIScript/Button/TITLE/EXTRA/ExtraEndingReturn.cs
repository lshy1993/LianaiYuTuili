using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Extra子界面 关闭结局成就
/// </summary>
public class ExtraEndingReturn : BasicButton
{
    public TitleUIManager uiManger;

    protected override void Execute()
    {
        uiManger.CloseEnding();
    }
}