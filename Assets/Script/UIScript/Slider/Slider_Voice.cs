using UnityEngine;
using System.Collections;

public class Slider_Voice : MonoBehaviour {

    //AudioSource bgm
    public SoundManager sm;
    public UISlider slider;
    public UILabel numlabel, helplabel;

    public void OnValueChange()
    {
        sm.userVoiceVolume = slider.value;
        numlabel.text = slider.value.ToString("00%");
    }

    void OnHover(bool ishover)
    {
        helplabel.text = ishover ? "设置人物语音的大小" : string.Empty;
    }
}
