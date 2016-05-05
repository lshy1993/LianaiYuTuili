using UnityEngine;
using System.Collections;

public class Slider_BGM : MonoBehaviour {

    public AudioSource bgm;
    public UISlider slider;
    public UILabel numlabel, helplabel;

    public void OnValueChange()
    {
        bgm.volume = slider.value;
        numlabel.text = slider.value.ToString("0%");
    }

    void OnHover(bool ishover)
    {
        if (ishover)
        {
            helplabel.text = "设置背景音乐的大小";
        }
        else
        {
            helplabel.text = string.Empty;
        }
    }

}
