using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.GameStruct;

public class SoundSettingUIManager : MonoBehaviour
{
    ///用于控制游戏声音的设置
    public GameObject charaGrid;
    public GameObject onBtn, offBtn, testBtn;
    public GameObject volumeSlider;
    private int defaultNum;

    private void OnEnable()
    {
        //初始化
        defaultNum = DataManager.GetInstance().systemData.defaultCharaNum;
        //读取相应的数据 并挂到组件上
        SetCharaButton(defaultNum);
    }

    private void SetRadioPressed(GameObject target)
    {
        target.GetComponent<UIButton>().normalSprite2D = target.GetComponent<UIButton>().hoverSprite2D;
        target.GetComponent<UIButton>().enabled = false;
    }
    private void SetRadioAvailable(GameObject target)
    {
        target.GetComponent<UIButton>().normalSprite2D = Resources.Load<Sprite>("UI/fun_back");
        target.GetComponent<UIButton>().enabled = true;
    }

    /// <summary>
    /// 将目标设置为 已按下
    /// </summary>
    /// <param name="target"></param>
    private void SetTogglePressed(GameObject target)
    {
        target.GetComponent<UIButton>().enabled = false;
        target.GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>("UI/switch_on");
    }

    /// <summary>
    /// 将目标设置为 可以按下
    /// </summary>
    /// <param name="target"></param>
    private void SetToggleAvailable(GameObject target)
    {
        target.GetComponent<UIButton>().enabled = true;
        target.GetComponent<UIButton>().normalSprite2D = Resources.Load<Sprite>("UI/switch");
    }

    private void SetCharaButton(int x)
    {
        SetRadioPressed(charaGrid.transform.GetChild(x).gameObject);
        //对应组件
        float volume = DataManager.GetInstance().systemData.charaVoiceVolume[x];
        bool flag = DataManager.GetInstance().systemData.charaVoice[x];
        SetToggleAvailable(flag ? offBtn : onBtn);
        SetTogglePressed(flag ? onBtn : offBtn);
        volumeSlider.SetActive(flag);
        volumeSlider.GetComponent<UISlider>().value = volume;
        testBtn.SetActive(flag);
        //volumeSlider.GetComponent<UISlider>().enabled = flag;
        //volumeSlider.GetComponent<UIButton>().enabled = flag;
        //
        //testBtn.GetComponent<UIButton>().enabled = flag;
    }

    public void SwitchChara(string str)
    {
        //切换角色：更改设置的索引？
        SetRadioAvailable(charaGrid.transform.GetChild(defaultNum).gameObject);
        switch (str)
        {
            case "Li_Button":
                defaultNum = 0;
                break;
            case "Su_Button":
                defaultNum = 1;
                break;
            case "Miao_Button":
                defaultNum = 2;
                break;
            case "Xi_Button":
                defaultNum = 3;
                break;
        }
        SetCharaButton(defaultNum);
    }

    public void MicTest()
    {
        //播放测试语音
        Debug.Log(defaultNum);
    }

    public void SwitchVoice()
    {
        //是否开启语音
        bool[] flag = DataManager.GetInstance().systemData.charaVoice;
        flag[defaultNum] = !flag[defaultNum];
        DataManager.GetInstance().systemData.charaVoice = flag;
        SetCharaButton(defaultNum);
    }

    public void SetVolume(float volume)
    {
        DataManager.GetInstance().systemData.charaVoiceVolume[defaultNum] = volume;
    }
}
