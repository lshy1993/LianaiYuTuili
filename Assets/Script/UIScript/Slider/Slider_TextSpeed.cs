using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct;
using System;

public class Slider_TextSpeed : MonoBehaviour
{
    public UISlider slider;
    public UILabel numlabel, helplabel;
    public TextSettingUIManager uiManager;

    private bool clicked = false;

    public void OnValueChange()
    {
        float textSpeed = 20 + Convert.ToInt32(slider.value * 50);
        numlabel.text = (slider.value * 100).ToString("0");
        DataManager.GetInstance().configData.textSpeed = textSpeed;
        clicked = true;
    }

    private void Update()
    {
        if (clicked && Input.GetMouseButtonUp(0))
        {
            uiManager.SetTextSpeed();
            clicked = false;
        }
    }

    void OnHover(bool ishover)
    {
        helplabel.text = ishover ? "设置文本的显示速度" : string.Empty;
    }

}
