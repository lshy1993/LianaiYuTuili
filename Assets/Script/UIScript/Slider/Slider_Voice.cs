using UnityEngine;
using System.Collections;

public class Slider_Voice : MonoBehaviour {

    public AudioSource voice;
    public UISlider slider;
    public UILabel numlabel, helplabel;

    public void OnValueChange()
    {
        voice.volume = slider.value;
        numlabel.text = slider.value.ToString("00%");
    }

    void OnHover(bool ishover)
    {
        if (ishover)
        {
            helplabel.text = "设置人物语音的大小";
        }
        else
        {
            helplabel.text = string.Empty;
        }
    }
}
