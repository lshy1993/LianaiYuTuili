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

    private bool flag = false;
    public UISlider diaBoxAlpha;
    public UILabel previewLabel, textLabel;
    public UI2DSprite previewBack, diaBack;

    private void OnEnable()
    {
        //初始化 获取已经设置的速度
        //float textSpeed = DataManager.GetInstance().textSpeed;
        //previewLabel.GetComponent<TypeWriter>().charsPerSecond = textSpeed;
        //previewLabel.GetComponent<TypeWriter>().ResetToBeginning();
        ResetText();
        SetAlpha();
    }

    private void OnDisable()
    {
        //Debug.Log("disable");
        StopAllCoroutines();
    }

    public void SetAlpha()
    {
        int alpha = DataManager.GetInstance().GetSystemVar<int>("diaboxAlpha");
        previewBack.alpha = alpha / 100f;
        diaBack.alpha = alpha / 100f;
    }

    public void ResetText()
    {
        StopAllCoroutines();
        float textSpeed = DataManager.GetInstance().GetSystemVar<float>("textSpeed");
        previewLabel.text = flag ? "请调节合适的文字显示速度\r\n请调节合适的自动等待时间" : "恋爱与推理\r\nXianZhuo Soft ©COPYRIGHT";
        previewLabel.GetComponent<TypeWriter>().charsPerSecond = textSpeed;
        previewLabel.GetComponent<TypeWriter>().ResetToBeginning();
        textLabel.GetComponent<TypeWriter>().charsPerSecond = textSpeed;
    }

    private IEnumerator WaitAndSwitch()
    {
        float waitTime = DataManager.GetInstance().GetSystemVar<float>("waitTime");
        yield return new WaitForSeconds(waitTime);
        flag = !flag;
        previewLabel.text = flag ? "请调节合适的文字显示速度\r\n请调节合适的自动等待时间" : "恋爱与推理\r\nXianZhuo Soft ©COPYRIGHT";
        previewLabel.GetComponent<TypeWriter>().ResetToBeginning();
    }

    public void SwitchText()
    {
        StopAllCoroutines();
        StartCoroutine(WaitAndSwitch());
    }

}
