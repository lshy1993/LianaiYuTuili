using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AvatarSwitchButton : MonoBehaviour
{
    public UILabel hint;

    void OnHover(bool ishover)
    {
        hint.text = ishover ? "设置【人物头像与表情】是否开启" : "";
    }
}
