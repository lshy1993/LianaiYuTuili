using UnityEngine;
using System.Collections;

public class MenuReturnButton : BasicButton {

    public SystemUIManager uiManager;
    //public UILabel helplabel;

    protected override void Execute()
    {
        uiManager.Close();
    }

}
