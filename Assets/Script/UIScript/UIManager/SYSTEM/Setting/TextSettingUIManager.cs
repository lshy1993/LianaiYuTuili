using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.GameStruct;

///用于控制对话框的文字设置
public class TextSettingUIManager : MonoBehaviour
{
    public UISlider diaBoxAlpha, textSpeed, autoSpeed;
    public UILabel previewLabel, textLabel;
    public UI2DSprite previewBack, diaBack;

    private bool previewFlag = false;
    private bool aniFlag = false;
    private float currentTime = 0f;

    private int alpha
    {
        get { return DataManager.GetInstance().configData.diaboxAlpha; }
    }
    private float tsp
    {
        get { return DataManager.GetInstance().configData.textSpeed; }
    }
    private float waitTime
    {
        get { return DataManager.GetInstance().configData.waitTime; }
    }

    private void Awake()
    {
        ResetTextSpeed();
        ResetAutoSpeed();
        ResetAlpha();
    }

    private void Update()
    {
        if (aniFlag)
        {
            //等待计时器
            if (currentTime < waitTime)
            {
                currentTime += Time.deltaTime;
                //Debug.Log(currentTime);
            }
            else
            {
                //计时器关闭且重置
                currentTime = 0f;
                aniFlag = false;
                previewFlag = !previewFlag;
                ResetTypewriter();
            }
        }

    }

    //打开此界面时的初始化
    private void OnEnable()
    {
        aniFlag = false;
        previewFlag = false;
        SetTextSpeed();
        SetAutoSpeed();
        SetAlpha();
    }

    private void ResetTypewriter()
    {
        previewLabel.text = previewFlag ? "请调节合适的文字显示速度\r\n请调节合适的自动等待时间" : "恋爱与推理\r\nXianZhuo Soft ©COPYRIGHT";
        previewLabel.GetComponent<TypeWriter>().ResetToBeginning();
    }

    private void ResetAlpha()
    {
        diaBoxAlpha.value = (float)alpha / 100;
    }

    private void ResetTextSpeed()
    {
        textSpeed.value = (tsp - 20) / 50;
    }

    private void ResetAutoSpeed()
    {
        autoSpeed.value = (5 - waitTime) / 5;
    }

    //##################以下为public方法#####################

    //设置【对话框】透明度
    public void SetAlpha()
    {
        previewBack.alpha = alpha / 100f;
        diaBack.alpha = alpha / 100f;
    }

    //设置【预览框/对话框】的文字显示速度
    public void SetTextSpeed()
    {
        previewLabel.GetComponent<TypeWriter>().charsPerSecond = tsp;
        textLabel.GetComponent<TypeWriter>().charsPerSecond = tsp;
        ResetTypewriter();
    }

    //设置【预览框】切换速度
    public void SetAutoSpeed()
    {
        ResetTypewriter();
    }

    //打字机完成后调用重置定时器
    public void SwitchText()
    {
        aniFlag = true;
    }

}
