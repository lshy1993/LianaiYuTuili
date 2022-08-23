using UnityEngine;
using System;
using Assets.Script.GameStruct;

public class Slider_DialogAlpha : MonoBehaviour
{
    public UISlider slider;
    public UILabel numlabel, helplabel;
    public TextSettingUIManager uiManager;

    public void OnValueChange()
    {
        int alpha = Convert.ToInt32(slider.value * 100);
        numlabel.text = alpha.ToString("0");
        DataManager.GetInstance().configData.diaboxAlpha = alpha;
        uiManager.SetAlpha();
    }

    void OnHover(bool ishover)
    {
        helplabel.text = ishover ? "设置对话框的透明度" : string.Empty;
    }

}
