using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.UIScript
{
    public class SpriteStatus
    {
        public float alpha;
        public Vector3 position;

        public SpriteStatus()
        {
            alpha = 1;
            position = ImageManager.MIDDLE;
        }

        public SpriteStatus(float alpha, Vector3 position)
        {
            this.alpha = alpha;
            this.position = position;
        }
    }


    public class ImageEffect
    {
        public static bool fast = false;
        public UI2DSprite aimUI;
        public SpriteStatus origin, final;
        public float time;
        public bool end, loop;
        public SingleUpdate update;
        public Action init, finish;

        public ImageEffect()
        {
            time = 1f;
            end = false;
            loop = false;
            init = Init;
            finish = Finish;
        }

        public void Init()
        {
            if (origin != null)
            {
                aimUI.alpha = origin.alpha;
                aimUI.transform.position = origin.position;
            }
        }

        public void Finish()
        {
            if (final != null)
            {
                aimUI.alpha = final.alpha;
                aimUI.transform.position = final.position;
            }
        }

        public IEnumerator Run(Action callback)
        {
            init();
            yield return null;
            float actualTime = fast ? 0.1f : time;
            if (time > 0)
            {
                for (float t = 0; t < time; t += Time.fixedDeltaTime)
                {
                    update(aimUI, actualTime, t);
                    yield return null;
                }
            }
            finish();
            callback();
        }

    }

    public delegate void SingleUpdate(UI2DSprite sprite, float time, float currentTime);
}
