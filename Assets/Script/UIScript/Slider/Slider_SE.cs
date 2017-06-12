using UnityEngine;
using System.Collections;

public class Slider_SE : MonoBehaviour {

    //AudioSource bgm
    public SoundManager sm;
    public UISlider slider;
    public UILabel numlabel, helplabel;

    public void OnValueChange()
    {
        sm.userSeVolume = slider.value;
        numlabel.text = slider.value.ToString("00%");
    }

    void OnHover(bool ishover)
    {
        helplabel.text = ishover ? "设置效果音的大小" : string.Empty;
    }

}
