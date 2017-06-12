using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AnimateSwitchButton : MonoBehaviour
{
    public UILabel hint;

    void OnHover(bool ishover)
    {
        hint.text = ishover ? "设置【动画效果】是否开启" : string.Empty;
    }
}
