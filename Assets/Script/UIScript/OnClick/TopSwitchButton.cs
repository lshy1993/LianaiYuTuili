using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TopSwitchButton : MonoBehaviour
{
    public UILabel hint;

    void OnHover(bool ishover)
    {
        hint.text = ishover ? "设置画面是否【处于最前】（该功能未实装）" : string.Empty;
    }
}
