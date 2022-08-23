using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct;
using System;

public class Slider_ChapterLabel : MonoBehaviour
{
    public UISlider slider;
    public UILabel numlabel, helplabel;

    public void OnValueChange()
    {
        int time = Convert.ToInt16(slider.value * 31);
        if (time == 31)
        {
            numlabel.text = "ON";
        }
        else if (time == 0)
        {
            numlabel.text = "OFF";
        }
        else
        {
            numlabel.text = time.ToString() + "s";
        }
        DataManager.GetInstance().configData.chapterTime = time;
    }

    void OnHover(bool ishover)
    {
        helplabel.text = ishover ? "章节切换时，显示当前章节名" : string.Empty;
    }
}
