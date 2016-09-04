using UnityEngine;
using System.Collections;
using System;

public class Slider_BGMTime : MonoBehaviour
{
    public TitleManager manager;

    void OnHover()
    {
        manager.isdrag = true;
    }

    void OnValueChange(float value)
    {
        Debug.Log(value);
    }

}
