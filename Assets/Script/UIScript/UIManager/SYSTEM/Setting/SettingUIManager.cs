﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.GameStruct;

public class SettingUIManager : MonoBehaviour
{
    /// <summary>
    /// 用于控制 和切换旗下的4个设置模块
    /// </summary>
    public GameObject graphicCon, soundCon, textCon, sysCon;
    public GameObject graphicBtn, soundBtn, textBtn, sysBtn;
    public UILabel hintLabel;

    private Constants.Setting_Mode settingMode
    {
        get { return DataManager.GetInstance().configData.settingMode; }
        set { DataManager.GetInstance().configData.settingMode = value; }
    }
    
    private void OnEnable()
    {
        //打开时自动切换至图像？
        switch (settingMode)
        {
            case Constants.Setting_Mode.Graphic:
                SwitchTab("Graphic_Button");
                break;
            case Constants.Setting_Mode.Operate:
                SwitchTab("Operate_Button");
                break;
            case Constants.Setting_Mode.Sound:
                SwitchTab("Sound_Button");
                break;
            case Constants.Setting_Mode.Text:
                SwitchTab("Sound_Button");
                break;
            default:
                SwitchTab("Graphic_Button");
                break;
        }
        
    }

    /// <summary>
    /// 切换标签页
    /// </summary>
    /// <param name="target">点击的目标</param>
    public void SwitchTab(string target)
    {
        //原状态按钮开启
        switch (settingMode)
        {
            case Constants.Setting_Mode.Graphic:
                graphicBtn.GetComponent<UIButton>().enabled = true;
                graphicBtn.GetComponent<UIButton>().normalSprite = "UI/fun_back";
                graphicCon.SetActive(false);
                break;
            case Constants.Setting_Mode.Sound:
                soundBtn.GetComponent<UIButton>().enabled = true;
                soundBtn.GetComponent<UIButton>().normalSprite = "UI/fun_back";
                soundCon.SetActive(false);
                break;
            case Constants.Setting_Mode.Text:
                textBtn.GetComponent<UIButton>().enabled = true;
                textBtn.GetComponent<UIButton>().normalSprite = "UI/fun_back";
                textCon.SetActive(false);
                break;
            case Constants.Setting_Mode.Operate:
                sysBtn.GetComponent<UIButton>().enabled = true;
                sysBtn.GetComponent<UIButton>().normalSprite = "UI/fun_back";
                sysCon.SetActive(false);
                break;
        }
        if (target == "Graphic_Button") settingMode = Constants.Setting_Mode.Graphic;
        if (target == "Sound_Button") settingMode = Constants.Setting_Mode.Sound;
        if (target == "Text_Button") settingMode = Constants.Setting_Mode.Text;
        if (target == "Operate_Button") settingMode = Constants.Setting_Mode.Operate;
        //新状态按钮不可用
        switch (settingMode)
        {
            case Constants.Setting_Mode.Graphic:
                graphicBtn.GetComponent<UIButton>().enabled = false;
                graphicBtn.GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>("UI/fun_hover1");
                graphicCon.SetActive(true);
                break;
            case Constants.Setting_Mode.Sound:
                soundBtn.GetComponent<UIButton>().enabled = false;
                soundBtn.GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>("UI/fun_hover2");
                soundCon.SetActive(true);
                break;
            case Constants.Setting_Mode.Text:
                textBtn.GetComponent<UIButton>().enabled = false;
                textBtn.GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>("UI/fun_hover3");
                textCon.SetActive(true);
                break;
            case Constants.Setting_Mode.Operate:
                sysBtn.GetComponent<UIButton>().enabled = false;
                sysBtn.GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>("UI/fun_hover4");
                sysCon.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// 设置提示文字
    /// </summary>
    /// <param name="target"></param>
    public void SetHint(string target)
    {
        switch (target)
        {
            case "Graphic_Button":
                hintLabel.text = "游戏图像性能设置";
                break;
            case "Sound_Button":
                hintLabel.text = "游戏声音相关设置";
                break;
            case "Text_Button":
                hintLabel.text = "游戏文本相关设置";
                break;
            case "Operate_Button":
                hintLabel.text = "暂未开放";
                break;
            default:
                hintLabel.text = string.Empty;
                break;
        }
    }

}
