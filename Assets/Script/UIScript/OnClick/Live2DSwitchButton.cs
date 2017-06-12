using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Live2DSwitchButton : MonoBehaviour
{
    public UILabel hint;
    void OnHover(bool ishover)
    {
        hint.text = ishover ? "设置【Live2D】是否开启（该功能未实装）" : string.Empty;
    }

}
