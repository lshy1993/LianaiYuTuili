using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.UIScript
{
    public class SpriteState
    {
        public float alpha;
        //{
        //    set
        //    {
        //        if (value < 0)
        //        {
        //            alpha = 0;
        //        }
        //        else if (value > 1)
        //        {
        //            alpha = 1;
        //        }
        //        else
        //        {
        //            alpha = value;
        //        }
        //    }

        //    get { return alpha; }
        //}
        public Vector3 position;

        public SpriteState()
        {
            alpha = 1;
            position = ImageManager.MIDDLE;
        }

        public SpriteState(float alpha, Vector3 position)
        {
            this.alpha = alpha;
            this.position = position;
        }
    }


    public class ImageEffect
    {
        public static bool fast = false;
        public UI2DSprite aimUI;
        public SpriteState origin, final;
        public float time;
        public bool end, loop;
        public SingleUpdate update;
        public Action init, finish;

        //public UIAnimationCallback callback;

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

        //public IEnumerator Run(UIAnimationCallback callback)
        //{
        //    //Init();
        //    //float t = 0;
        //    //while (true)
        //    //{
        //    //    t += Time.fixedDeltaTime;
        //    //    if (!loop && t > time) break;
        //    //    yield return update(aimUI, time);
        //    //}
        //    //Finish();
        //    //callback();
        //}

        //public void Exec(UIAnimationCallback callback)
        //{
        //    StartCoroutine(Run(callback));
        //}

        //public void Exec()
        //{
        //    StartCoroutine(Run(() => { }));
        //}

    }

    public delegate void SingleUpdate(UI2DSprite sprite, float time, float currentTime);

    public delegate IEnumerator Animate(UI2DSprite sprite, UIAnimationCallback callback);
}
//public static IEnumerator FadeIn(UI2DSprite sprite, float time)
//{
//    if (sprite.alpha < 1)
//        sprite.alpha = Mathf.MoveTowards(sprite.alpha, 1, 1 / time * Time.fixedDeltaTime);
//    yield return null;
//}

//public static IEnumerator FadeOut(UI2DSprite sprite, float time)
//{
//    if (sprite.alpha > 0)
//        sprite.alpha = Mathf.MoveTowards(sprite.alpha, 0, 1 / time * Time.fixedDeltaTime);
//    yield return null;
//}

//public static AnimateUpdate Move(Vector3 move)
//{
//    Func<UI2DSprite, float, IEnumerator> func = (sprite, time) =>
//    {
//        float deltaRatio = Time.fixedDeltaTime / time;
//        Vector3 delta = new Vector3(move.x * deltaRatio, move.y * deltaRatio);
//        sprite.transform.position += delta;
//        return null;
//    };
//    return new AnimateUpdate(func);
// }


