using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.UIScript;
using System;
using Assets.Script.GameStruct;

public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// BGM音源
    /// </summary>
    public AudioSource currentBGM;
    /// <summary>
    /// 效果音源
    /// </summary>
    public AudioSource currentSE;
    /// <summary>
    /// 语音音源
    /// </summary>
    public AudioSource currentVoice;
    /// <summary>
    /// 系统效果音
    /// </summary>
    public AudioSource systemSE;

    public SideLabelUIManager sideLabel;

    [HideInInspector]
    public float userBgmVolume
    {
        get { return currentBGM.volume; }
        set { currentBGM.volume = value; }
    }
    [HideInInspector]
    public float userSeVolume
    {
        get {return currentSE.volume; }
        set { currentSE.volume = value; }
    }
    [HideInInspector]
    public float userVoiceVolume
    {
        get { return currentVoice.volume; }
        set { currentVoice.volume = value; }
    }
    [HideInInspector]
    public float userSysSEVolume
    {
        get { return systemSE.volume; }
        set { systemSE.volume = value; }
    }


    private float memoryVolume;
    private Dictionary<string, AudioClip> auDic;
    private AudioSource aim;

    private DataManager dm;

    private void Start()
    {
        dm = DataManager.GetInstance();
        memoryVolume = userBgmVolume;
    }

    public void SetAuDic(Dictionary<string, AudioClip> auDic)
    {
        this.auDic = auDic;
    }

    public void SetBGM(string fileName, bool loop = true)
    {
        if (string.IsNullOrEmpty(fileName)) return;
        //AudioClip ac = Resources.Load<AudioClip>("Audio/" + fileName);
        AudioClip ac= null;
        if (auDic.ContainsKey(fileName)) ac = auDic[fileName];
        currentBGM.volume = memoryVolume;
        //判断是否存在播放的内容 
        if (currentBGM.clip != null)
        {
            if(currentBGM.clip.name != fileName)
            {
                //播放内容不同
                currentBGM.clip = ac;
                currentBGM.loop = loop;
                currentBGM.Play();
            }
            else if(!currentBGM.isPlaying)
            {
                //相同但当前乐曲处于暂停
                currentBGM.Play();
            }
        }
        else
        {
            //不存在的情况
            currentBGM.clip = ac;
            currentBGM.loop = loop;
            currentBGM.Play();
        }
    }
    public void PauseBGM()
    {
        currentBGM.Pause();
    }
    public void PlayBGM()
    {
        currentBGM.UnPause();
    }
    //public void UnPauseBGM()
    //{
    //    currentBGM.UnPause();
    //}
    public void StopBGM()
    {
        currentBGM.Stop();
        currentBGM.clip = null;
    }

    public void SetSE(string fileName, bool loop = false)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            if (currentSE.isPlaying) return;
            currentSE.clip = null;
        }
        else
        {
            AudioClip ac = Resources.Load<AudioClip>("Audio/" + fileName);
            currentSE.clip = ac;
            currentSE.loop = loop;
            currentSE.Play();
        }
    }
    public void StopSE()
    {
        currentSE.Stop();
    }

    public void SetVoice(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            //若还在播放则忽视
            if (currentVoice.isPlaying) return;
            currentVoice.clip = null;
        }
        else
        {
            AudioClip ac = Resources.Load<AudioClip>("Voice/" + fileName);
            currentVoice.clip = ac;
            currentVoice.Play();
        }
    }
    public void StopVoice()
    {
        currentVoice.Stop();
    }

    /// <summary>
    /// 播放系统音效
    /// </summary>
    public void SetSystemSE(string fileName)
    {
        if (string.IsNullOrEmpty(fileName)) return;
        AudioClip ac = Resources.Load<AudioClip>("Audio/" + fileName);
        systemSE.clip = ac;
        systemSE.Play();
    }


    public void RunEffect(SoundEffect effect, Action callback)
    {
        //决定操作源
        switch (effect.target)
        {
            case SoundEffect.SoundType.BGM:
                aim = currentBGM;
                break;
            case SoundEffect.SoundType.SE:
                aim = currentSE;
                break;
            case SoundEffect.SoundType.Voice:
                aim = currentVoice;
                break;
            default:
                break;
        }
        switch (effect.operate)
        {
            case SoundEffect.OperateType.Pause:
                aim.Pause();
                callback();
                break;
            case SoundEffect.OperateType.Unpause:
                aim.UnPause();
                callback();
                break;
            case SoundEffect.OperateType.Stop:
                aim.Stop();
                callback();
                break;
            case SoundEffect.OperateType.Set:
                SetBGM(effect.clip, effect.loop);
                sideLabel.ShowBGM(currentBGM.clip.name);
                callback();
                break;
            default:
                StartCoroutine(Run(effect, callback));
                break;
        }
        
    }

    public void SaveSoundInfo()
    {
        /* demo1.20 改动
        string bgmName = currentBGM.clip == null ? "" : currentBGM.clip.name;
        string seName = currentSE.clip == null ? "" : currentSE.clip.name;
        string voiceName = currentVoice.clip == null ? "" : currentVoice.clip.name;
        DataManager.GetInstance().SetGameVar("BGM", bgmName);
        DataManager.GetInstance().SetGameVar("SE", seName);
        DataManager.GetInstance().SetGameVar("Voice", voiceName);
        */
        string bgmName = currentBGM.clip == null ? "" : currentBGM.clip.name;
        string seName = currentSE.clip == null ? "" : currentSE.clip.name;
        string voiceName = currentVoice.clip == null ? "" : currentVoice.clip.name;
        dm.gameData.BGM = bgmName;
        dm.gameData.SE = seName;
        dm.gameData.Voice = voiceName;
    }

    public void LoadSoundInfo()
    {
        /* demo1.20 改动
        string bgmName = DataManager.GetInstance().GetGameVar<string>("BGM");
        string seName = DataManager.GetInstance().GetGameVar<string>("SE");
        string voiceName = DataManager.GetInstance().GetGameVar<string>("Voice");
        */
        string bgmName = dm.gameData.BGM;
        string seName = dm.gameData.SE;
        string voiceName = dm.gameData.Voice;
        SetBGM(bgmName);
        SetSE(seName);
        SetVoice(voiceName);
    }

    private IEnumerator Run(SoundEffect effect, Action callback)
    {
        if (effect.operate == SoundEffect.OperateType.Fadeout) memoryVolume = aim.volume;
        float tweenFrom = effect.operate == SoundEffect.OperateType.Fadein ? 0 : aim.volume;
        float tweenFinal = effect.operate == SoundEffect.OperateType.Fadein ? memoryVolume : 0;
        for (float t = 0; t <= effect.time; t += Time.fixedDeltaTime)
        {
            aim.volume = Mathf.MoveTowards(aim.volume, tweenFinal, Math.Abs(tweenFinal - tweenFrom) / effect.time * Time.fixedDeltaTime);
            yield return null;
        }
        callback();
    }

}
