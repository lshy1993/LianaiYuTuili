using UnityEngine;
using System.Collections;

public class MenuReturnButton : BasicButton {

    public SystemUIManager uiManager;
    //public UILabel helplabel;

    protected override void OnClick()
    {
        base.OnClick();
        uiManager.Close();
    }

    //void OnHover(bool ishover)
    //{
    //    if (ishover)
    //    {
    //        helplabel.text = "返回菜单";
    //    }
    //    else
    //    {
    //        helplabel.text = string.Empty;
    //    }
    //}

}
