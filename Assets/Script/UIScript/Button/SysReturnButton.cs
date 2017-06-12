using UnityEngine;
using System.Collections;

public class SysReturnButton : MonoBehaviour {

    public SystemUIManager sm;
    //public UILabel helplabel;

    void OnClick()
    {
        sm.Close();
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
