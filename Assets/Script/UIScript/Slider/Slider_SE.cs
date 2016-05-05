using UnityEngine;
using System.Collections;

public class Slider_SE : MonoBehaviour {

    public AudioSource se;
    public UISlider slider;
    public UILabel numlabel, helplabel;

    public void OnValueChange()
    {
        se.volume = slider.value;
        numlabel.text = slider.value.ToString("00%");
    }

    void OnHover(bool ishover)
    {
        if (ishover)
        {
            helplabel.text = "设置效果音的大小";
        }
        else
        {
            helplabel.text = string.Empty;
        }
    }

}
