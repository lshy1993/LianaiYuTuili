using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.UIScript;
using System;
using Assets.Script.GameStruct;

public class SoundManager : MonoBehaviour
{
    public AudioSource currentBGM;
    public AudioSource currentSE;
    public AudioSource currentVoice;

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

    private float memoryVolume;
    private Dictionary<string, AudioClip> auDic;
    private AudioSource aim;

    private void Start()
    {
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
        AudioClip ac = Resources.Load<AudioClip>("Audio/" + fileName);
        currentSE.clip = ac;
        currentSE.loop = loop;
        currentSE.Play();
    }
    public void StopSE()
    {
        currentSE.Stop();
    }
    public void SetVoice(string fileName)
    {
        AudioClip ac = Resources.Load<AudioClip>("Voice/" + fileName);
        currentVoice.clip = ac;
        currentVoice.Play();
    }
    public void StopVoice()
    {
        currentVoice.Stop();
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
                callback();
                break;
            default:
                StartCoroutine(Run(effect, callback));
                break;
        }
        
    }

    public void SaveSoundInfo()
    {
        string bgmName = currentBGM.clip == null ? "" : currentBGM.clip.name;
        string seName = currentSE.clip == null ? "" : currentSE.clip.name;
        string voiceName = currentVoice.clip == null ? "" : currentVoice.clip.name;
        DataManager.GetInstance().SetGameVar("BGM", bgmName);
        DataManager.GetInstance().SetGameVar("SE", seName);
        DataManager.GetInstance().SetGameVar("Voice", voiceName);
    }

    public void LoadSoundInfo()
    {
        string bgmName = DataManager.GetInstance().GetGameVar<string>("BGM");
        string seName = DataManager.GetInstance().GetGameVar<string>("SE");
        string voiceName = DataManager.GetInstance().GetGameVar<string>("Voice");
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
