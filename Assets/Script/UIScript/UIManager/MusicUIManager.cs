using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.GameStruct;

public class MusicUIManager : MonoBehaviour
{
    public SoundManager sm;
    public AudioSource bgm;

    public UILabel timelabel;
    public UISlider slider;

    private List<bool> musicTable;

    private void OnEnable()
    {
        musicTable = (List<bool>)DataPool.GetInstance().GetSystemVar("音乐表");
    }

    private void Update()
    {
        int tnow = (int)bgm.time;
        int tall = 0;
        if (bgm.clip != null) tall = (int)bgm.clip.length;
        TimeSpan nowts = new TimeSpan(0, 0, tnow);
        TimeSpan allts = new TimeSpan(0, 0, tall);
        timelabel.text = nowts.ToString() + "/" + allts.ToString();
    }

    private void SetMusic()
    {
        //编辑器内 先设计好所有按钮 开启则显示 未开启则默认
        //存储【名称，是否开启】
        GameObject grid = transform.Find("NameButton_Grid").gameObject;
        for (int i = 0; i < musicTable.Count; i++)
        {
            UIButton btn = grid.transform.Find("Label" + i).gameObject.GetComponent<UIButton>();
            UILabel lb = grid.transform.Find("Label" + i).gameObject.GetComponent<UILabel>();
            btn.isEnabled = musicTable[i];
            if (musicTable[i])
            {
                lb.text = "??????";
            }
        }
    }

    public void PlayMusicAt(string fileName)
    {
        sm.SetBGM(fileName);
        transform.Find("Control_Container/Play_Button").gameObject.SetActive(false);
        transform.Find("Control_Container/Pause_Button").gameObject.SetActive(true);
    }
    public void PlayMusic()
    {
        //继续播放
        sm.PlayBGM();
        transform.Find("Control_Container/Play_Button").gameObject.SetActive(false);
        transform.Find("Control_Container/Pause_Button").gameObject.SetActive(true);
    }

    public void PauseMusic()
    {
        transform.Find("Control_Container/Play_Button").gameObject.SetActive(true);
        transform.Find("Control_Container/Pause_Button").gameObject.SetActive(false);
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
        transform.Find("Control_Container/Play_Button").gameObject.SetActive(true);
        transform.Find("Control_Container/Pause_Button").gameObject.SetActive(false);
        sm.StopBGM();
    }

}
