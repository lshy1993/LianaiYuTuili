﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ScreenSizeButton : MonoBehaviour
{
    public UILabel hint;

    void OnHover(bool ishover)
    {
        hint.text = ishover ? "设置以【窗口】或者【全屏】模式运行" : string.Empty;
    }
}
