using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct;

public class ToggleAuto : MonoBehaviour
{
    public SoundManager sm;
    public DialogBoxUIManager uiManager;
    public Click_Next cn;
    public UIProgressBar autoBar;

    //是否开启了auto模式
    private bool isAuto
    {
        get { return DataManager.GetInstance().isAuto; }
        set { DataManager.GetInstance().isAuto = value; }
    }
    private bool isEffecting
    {
        get { return DataManager.GetInstance().isEffecting; }
    }

    private bool isCounting = false;

    private float currentTime = 0f;
    private float waitTime
    {
        get { return DataManager.GetInstance().configData.waitTime; }
    }

    private void Update()
    {
        if (!isAuto)
        {
            CancelAuto();
            return;
        }
        if (isCounting)
        {
            CountDown();
        }
        else
        {
            CheckFinish();
        }
    }

    //检测当前块是否结束
    private void CheckFinish()
    {
        if (isEffecting) return;
        if (!uiManager.IsTyping())
        {
            isCounting = true;
        }
    }

    //计时器
    private void CountDown()
    {
        //等待计时器数完
        if (currentTime < waitTime)
        {
            autoBar.gameObject.SetActive(true);
            currentTime += Time.deltaTime;
            autoBar.value = currentTime / waitTime;
        }
        else
        {
            autoBar.gameObject.SetActive(false);
            if (sm.IsVoicePlaying()) return;
            //计时器关闭且重置
            isCounting = false;
            currentTime = 0f;
            //调用Click
            cn.Execute();
        }
    }

    public void ResetTimer()
    {
        currentTime = 0f;
    }

    void OnClick()
    {
        //切换模式
        isAuto = !isAuto;
    }

    public void CancelAuto()
    {
        isAuto = false;
        //autoBar.gameObject.SetActive(false);
        currentTime = 0f;
        this.GetComponent<UIToggle>().value = false;
        autoBar.gameObject.SetActive(false);
    }
}
