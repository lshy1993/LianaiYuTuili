using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.UIScript
{
    public class SoundEffect
    {
        public static bool fast = false;

        public AudioSource aimAudio;
        public string clip;

        public float time, origin, final;
        public bool loop, needrem;

        public enum SoundType { BGM, SE, Voice };
        public SoundType target;

        public enum OperateType { Fadeout, Fadein, Pause, Unpause, Stop, Set };
        public OperateType operate;

        //public SingleSoundUpdate update;
        //public Action init, finish;

        public SoundEffect()
        {
            time = 1f;
            loop = false;
            needrem = false;
            //init = Init;
            //finish = Finish;
        }

        //    public void Init()
        //    {
        //        //记忆当前音量，防止变化影响玩家设定
        //        originvolume = aimAudio.volume;
        //    }

        //    public void Finish()
        //    {
        //        //恢复至当前音量
        //        aimAudio.volume = originvolume;
        //    }

        //    public IEnumerator Run(Action callback)
        //    {
        //        init();
        //        yield return null;
        //        float actualTime = fast ? 0f : time;
        //        if (time > 0)
        //        {
        //            for (float t = 0; t < time; t += Time.fixedDeltaTime)
        //            {
        //                update(aimAudio, originvolume, actualTime, t);
        //                yield return null;
        //            }
        //        }
        //        finish();
        //        callback();
        //    }

        //}

        //public delegate void SingleSoundUpdate(AudioSource audio, float currentVolume, float time, float currentTime);
    }
}
