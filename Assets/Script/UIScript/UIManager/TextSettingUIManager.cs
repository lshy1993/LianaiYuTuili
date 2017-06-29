using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.GameStruct;

public class TextSettingUIManager : MonoBehaviour
{
    ///用于控制对话框的文字设置

    public UISlider diaBoxAlpha;
    public UILabel previewLabel, textLabel;
    public UI2DSprite previewBack, diaBack;

    private bool previewFlag = false;
    private bool aniFlag = false;
    private float currentTime = 0f;
    private float waitTime;

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

    private void OnEnable()
    {
        aniFlag = false;
        previewFlag = false;
        //初始化
        ResetText();
        ResetSpeed();
        //设置对话框透明度
        SetAlpha();
    }

    private void ResetTypewriter()
    {
        previewLabel.text = previewFlag ? "请调节合适的文字显示速度\r\n请调节合适的自动等待时间" : "恋爱与推理\r\nXianZhuo Soft ©COPYRIGHT";
        previewLabel.GetComponent<TypeWriter>().ResetToBeginning();
    }

    public void SetAlpha()
    {
        int alpha = DataManager.GetInstance().GetSystemVar<int>("diaboxAlpha");
        previewBack.alpha = alpha / 100f;
        diaBack.alpha = alpha / 100f;
    }

    public void ResetText()
    {
        //设置文字速度
        float textSpeed = DataManager.GetInstance().GetSystemVar<float>("textSpeed");
        previewLabel.GetComponent<TypeWriter>().charsPerSecond = textSpeed;
        textLabel.GetComponent<TypeWriter>().charsPerSecond = textSpeed;
        //重置打字机文字
        ResetTypewriter();
    }

    public void ResetSpeed()
    {
        waitTime = DataManager.GetInstance().GetSystemVar<float>("waitTime");
        //重置打字机文字
        ResetTypewriter();
    }

    //打字机完成后调用重置定时器
    public void SwitchText()
    {
        aniFlag = true;
    }

}
