using UnityEngine;
using System.Collections;

public class Click_SysBack : MonoBehaviour {

    public SystemManager sm;
    public UILabel helplabel;

    void OnClick()
    {
        sm.CloseSetting();
    }
    void OnHover(bool ishover)
    {
        if (ishover)
        {
            helplabel.text = "返回菜单";
        }
        else
        {
            helplabel.text = string.Empty;
        }
    }

}
