using System;
using Assets.Script.GameStruct;
using UnityEngine;

public class Slider_CharaVoice : MonoBehaviour
{
    public UISlider slider;
    public UILabel numlabel, helplabel;
    public SoundSettingUIManager uiManager;

    public void OnValueChange()
    {
        numlabel.text = (slider.value * 100).ToString();
        uiManager.SetVolume(slider.value);
    }

    void OnHover(bool ishover)
    {
        helplabel.text = ishover ? "设置角色语音的音量大小" : string.Empty;
    }
}
