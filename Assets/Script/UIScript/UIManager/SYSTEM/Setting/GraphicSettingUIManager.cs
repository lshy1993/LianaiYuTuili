using System;
using System.Collections.Generic;
using Assets.Script.GameStruct;
using UnityEngine;

public class GraphicSettingUIManager : MonoBehaviour
{
    public GameObject winBtn, fullBtn;
    public GameObject fadeOnBtn, fadeOffBtn;
    public GameObject animeOnBtn, animeOffBtn;
    public GameObject avatarOnBtn, avatarOffBtn;
    public GameObject topOnBtn, topOffBtn;
    public GameObject live2dOnBtn, live2dOffBtn;
    public UISlider bgmSld, chapterSld;

    private DataManager dm;

    private void OnEnable()
    {
        dm = DataManager.GetInstance();
        //设置画面大小
        SetTogglePressed(Screen.fullScreen ? fullBtn : winBtn);
        //设置画面效果
        bool flag = dm.configData.fadingSwitch;
        SetTogglePressed(flag ? fadeOnBtn : fadeOffBtn);
        //设置动画效果
        flag = dm.configData.animateSwitch;
        SetTogglePressed(flag ? animeOnBtn : animeOffBtn);
        //设置头像
        flag = dm.configData.avatarSwitch;
        SetTogglePressed(flag ? avatarOnBtn : avatarOffBtn);
        //总在最前
        topOnBtn.GetComponent<UIButton>().enabled = false;
        topOffBtn.GetComponent<UIButton>().enabled = false;
        //Live2D
        live2dOnBtn.GetComponent<UIButton>().enabled = false;
        live2dOffBtn.GetComponent<UIButton>().enabled = false;
        //设置标签显示
        int xx = dm.configData.BGMTime;
        bgmSld.value = xx / 31f;
        chapterSld.value = dm.configData.chapterTime / 31f;
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

    public void SwitchFullScreen()
    {
        //由于是切换状态，所以按钮与当前状态相反
        SetTogglePressed(Screen.fullScreen ? fullBtn : winBtn);
        SetToggleAvailable(Screen.fullScreen ? winBtn : fullBtn);
        Screen.SetResolution(1280, 720, !Screen.fullScreen);
    }

    public void SwitchFading()
    {
        bool flag = dm.configData.fadingSwitch;
        SetTogglePressed(flag ? fadeOffBtn : fadeOnBtn);
        SetToggleAvailable(flag ? fadeOnBtn : fadeOffBtn);
        dm.configData.fadingSwitch = !flag;
    }

    public void SwitchAnime()
    {
        bool flag = dm.configData.animateSwitch;
        SetTogglePressed(flag ? animeOffBtn : animeOnBtn);
        SetToggleAvailable(flag ? animeOnBtn : animeOffBtn);
        dm.configData.animateSwitch = !flag;
    }

    public void SwitchAvatar()
    {
        bool flag =dm.configData.avatarSwitch;
        SetTogglePressed(flag ? avatarOffBtn : avatarOnBtn);
        SetToggleAvailable(flag ? avatarOnBtn : avatarOffBtn);
        dm.configData.avatarSwitch = !flag;
    }
}
