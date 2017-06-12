using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct;
using System;

public class Slider_AutoSpeed : MonoBehaviour
{
    public UISlider slider;
    public UILabel numlabel, helplabel;

    public void OnValueChange()
    {
        float speed = 5 - slider.value * 5;
        int i = (int)(speed * 100);
        speed = (float)(i * 1.0) / 100;
        numlabel.text = (slider.value * 100).ToString();
        DataManager.GetInstance().SetSystemVar("waitTime", speed);
    }

    void OnHover(bool ishover)
    {
        helplabel.text = ishover ? "自动模式下，文本的等待的时间" : string.Empty;
    }

}
