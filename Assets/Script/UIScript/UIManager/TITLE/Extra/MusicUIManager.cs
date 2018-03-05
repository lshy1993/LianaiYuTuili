using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.GameStruct;

/// <summary>
/// Extra 音乐鉴赏管理器
/// </summary>
public class MusicUIManager : MonoBehaviour
{
    public SoundManager sm;
    public AudioSource bgm;

    public UILabel timelabel;
    public UIProgressBar timeBar;
    public GameObject playBtn, pauseBtn;

    private Dictionary<int, bool> musicTable
    {
        get { return DataManager.GetInstance().multiData.musicTable; }
    }

    private void OnEnable()
    {
        TimeSpan nowts = new TimeSpan(0, 0, 0);
        TimeSpan allts = new TimeSpan(0, 0, 0);
        timelabel.text = nowts.ToString() + "/" + allts.ToString();
        timeBar.value = 0;
        SetMusic();
    }

    private void Update()
    {
        if (bgm.isPlaying)
        {
            //当前秒数
            int tnow = (int)bgm.time;
            //总秒
            int tall = (int)bgm.clip.length;
            TimeSpan nowts = new TimeSpan(0, 0, tnow);
            TimeSpan allts = new TimeSpan(0, 0, tall);
            timelabel.text = nowts.ToString() + "/" + allts.ToString();
            timeBar.value = (float)tnow / tall;
        }  
    }

    private void SetMusic()
    {
        //编辑器内 先设计好所有按钮 开启则显示 未开启则默认
        //存储【名称，是否开启】
        GameObject grid = transform.Find("NameButton_Grid").gameObject;
        for (int i = 0; i < grid.transform.childCount; i++)
        {
            GameObject go = grid.transform.GetChild(i).gameObject;
            MusicButton mb = go.GetComponent<MusicButton>();
            mb.uiManager = this;
            UIButton btn = go.GetComponent<UIButton>();
            
            
            
            //btn.isEnabled = musicTable[i];
            //if (musicTable[i])
            //{
            //    lb.text = "??????";
            //}
        }
    }

    public void PlayMusicAt(string fileName)
    {
        sm.SetBGM(fileName);
        playBtn.SetActive(false);
        pauseBtn.SetActive(true);
    }
    public void PlayMusic()
    {
        //继续播放
        sm.PlayBGM();
        playBtn.SetActive(false);
        pauseBtn.SetActive(true);
    }

    public void PauseMusic()
    {
        playBtn.SetActive(true);
        pauseBtn.SetActive(false);
        sm.PauseBGM();
    }

    public void SetMusicTime(float x)
    {
        bgm.time = x * bgm.clip.length;
    }

    public void PrevMusic()
    {
        //上一首
    }
    public void NextMusic()
    {
        //下一首
    }
    public void StopMusic()
    {
        playBtn.gameObject.SetActive(true);
        pauseBtn.SetActive(false);
        sm.StopBGM();
    }

}
