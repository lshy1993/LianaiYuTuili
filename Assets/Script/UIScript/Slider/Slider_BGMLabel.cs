using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct;
using System;

public class Slider_BGMLabel : MonoBehaviour
{
    public UISlider slider;
    public UILabel numlabel, helplabel;

    public void OnValueChange()
    {
        int showBGMtime = Convert.ToInt16(slider.value * 31);
        if(showBGMtime == 31)
        {
            numlabel.text = "ON";
        }
        else if(showBGMtime == 0)
        {
            numlabel.text = "OFF";
        }
        else
        {
            numlabel.text = showBGMtime.ToString() + "s";
        }
        DataManager.GetInstance().configData.BGMTime = showBGMtime;
    }

    void OnHover(bool ishover)
    {
        helplabel.text = ishover ? "背景音乐切换时，显示乐曲名" : string.Empty;
    }
}
