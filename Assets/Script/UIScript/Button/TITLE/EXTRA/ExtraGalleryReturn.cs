using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ExtraGalleryReturn : BasicButton
{
    public TitleUIManager uiManager;

    protected override void Execute()
    {
        uiManager.CloseGallery();
    }
}