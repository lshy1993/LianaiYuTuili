using Assets.Script.UIScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public class SoundBuilder
    {
        static SoundManager soundManager;
        public static AudioSource currentBGM, currentSE;

        public static void Init(SoundManager sm)
        {
            soundManager = GameObject.Find("GameManager").GetComponent<SoundManager>();
            currentBGM = sm.currentBGM;
            currentSE = sm.currentSE;
        }

        //public static SoundEffect Parallel(List<SoundEffect> effects)
        //{
        //    SoundEffect effect = new SoundEffect();
        //    List<float> times = new List<float>();

        //    foreach (SoundEffect e in effects)
        //    {
        //        times.Add(e.time);
        //    }

        //    effect.time = times.Max();
        //    effect.loop = false;
        //    effect.init = () =>
        //    {
        //        foreach (SoundEffect e in effects)
        //        {
        //            e.init();
        //        }
        //    };

        //    effect.finish = () =>
        //    {
        //        foreach (SoundEffect e in effects)
        //        {
        //            e.finish();
        //        }
        //    };

        //    effect.update = (a, originvolume, time, nowtime) =>
        //    {
        //        foreach (SoundEffect e in effects)
        //        {
        //            if (nowtime < e.time)
        //                e.update(e.aimAudio, e.originvolume, e.time, nowtime);
        //        }
        //    };

        //    return effect;
        //}

        private SoundEffect soundEffect;

        public SoundBuilder Source(SoundEffect.SoundType mode)
        {
            soundEffect = new SoundEffect();
            soundEffect.target = mode;
            return this;
        }

        public SoundBuilder Clip(string fname)
        {
            soundEffect.clip = fname;
            return this;
        }

        public SoundBuilder Loop(bool loop)
        {
            soundEffect.loop = loop;
            return this;
        }

        public SoundBuilder TotalTime(float time)
        {
            soundEffect.time = time;
            return this;
        }

        public SoundBuilder Operater(SoundEffect.OperateType mode)
        {
            soundEffect.operate = mode;
            return this;
        }

        //public SoundBuilder OriginVol(float volume)
        //{
        //    soundEffect.originvolume = volume;
        //    return this;
        //}

        //public SoundBuilder Init(Action init)
        //{
        //    soundEffect.init = init;
        //    return this;
        //}

        //public SoundBuilder Finish(Action finish)
        //{
        //    soundEffect.finish = finish;
        //    return this;
        //}

        //public SoundBuilder AnimateUpdate(SingleSoundUpdate update)
        //{
        //    soundEffect.update = new SingleSoundUpdate(update);
        //    return this;
        //}

        public SoundEffect Get() { return soundEffect; }

        public static SoundEffect SetBGM(string fname, bool loop)
        {
            SoundBuilder builder = new SoundBuilder();
            SoundEffect e = builder.Source(SoundEffect.SoundType.BGM)
                .TotalTime(0)
                .Operater(SoundEffect.OperateType.Set)
                .Clip(fname)
                .Loop(loop)
                .Get();
            return e;
        }

        public static SoundEffect RemoveBGM()
        {
            SoundBuilder builder = new SoundBuilder();
            SoundEffect e = builder.Source(SoundEffect.SoundType.BGM)
                .TotalTime(0)
                .Operater(SoundEffect.OperateType.Set)
                .Clip("")
                .Get();
            return e;
        }

        public static SoundEffect FadeInBGM(float time)
        {
            SoundBuilder builder = new SoundBuilder();
            AudioSource aus = GameObject.Find("GameManager").GetComponent<SoundManager>().currentBGM;
            SoundEffect e = builder.Source(SoundEffect.SoundType.BGM)
                .TotalTime(time)
                .Operater(SoundEffect.OperateType.Fadein)
                .Get();
            return e;
        }

        public static SoundEffect FadeOutBGM(float time)
        {
            SoundBuilder builder = new SoundBuilder();
            SoundEffect e = builder.Source(SoundEffect.SoundType.BGM)
                .TotalTime(time)
                .Operater(SoundEffect.OperateType.Fadeout)
                .Get();
            return e;
        }

        public static SoundEffect PauseBGM()
        {
            SoundBuilder builder = new SoundBuilder();
            SoundEffect e = builder.Source(SoundEffect.SoundType.BGM)
                .TotalTime(0)
                .Operater(SoundEffect.OperateType.Pause)
                .Get();
            return e;
        }

        public static SoundEffect UnpauseBGM()
        {
            SoundBuilder builder = new SoundBuilder();
            SoundEffect e = builder.Source(SoundEffect.SoundType.BGM)
                .TotalTime(0)
                .Operater(SoundEffect.OperateType.Unpause)
                .Get();
            return e;
        }

        public static SoundEffect SetSE(string fname, bool loop)
        {
            SoundBuilder builder = new SoundBuilder();
            SoundEffect e = builder.Source(SoundEffect.SoundType.SE)
                .TotalTime(0)
                .Loop(loop)
                .Operater(SoundEffect.OperateType.Set)
                .Get();
            return e;
        }

        public static SoundEffect FadeInSE(float time)
        {
            SoundBuilder builder = new SoundBuilder();
            SoundEffect e = builder.Source(SoundEffect.SoundType.SE)
                .TotalTime(time)
                .Operater(SoundEffect.OperateType.Fadein)
                .Get();
            return e;
        }

        public static SoundEffect FadeOutSE(float time)
        {
            SoundBuilder builder = new SoundBuilder();
            SoundEffect e = builder.Source(SoundEffect.SoundType.SE)
                .TotalTime(time)
                .Operater(SoundEffect.OperateType.Fadeout)
                .Get();
            return e;
        }
    }
    
}
