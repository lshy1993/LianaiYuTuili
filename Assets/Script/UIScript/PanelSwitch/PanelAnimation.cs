using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.UIScript
{
    public class PanelAnimation : MonoBehaviour
    {
        public float maxAlpha = 1, minAlpha = 0;
        public float closeTime = 0.5f, openTime = 0.5f;
        protected UIRect panel;

        public virtual void Init()
        {
            panel = transform.GetComponent<UIRect>();
        }

        public virtual void BeforeClose() {}

        public virtual IEnumerator CloseSequence(UIAnimationCallback callback)
        {
            //Debug.Log(Time.time + " Close Panel:" + panel.name);
            panel.alpha = maxAlpha;
            float fadeSpeed = Math.Abs(maxAlpha - minAlpha) / closeTime;
            while (panel.alpha > minAlpha)
            {
                panel.alpha = Mathf.MoveTowards(panel.alpha, minAlpha, fadeSpeed * Time.fixedDeltaTime);

                yield return null;
            }

            callback();

        }

        public virtual IEnumerator OpenSequence(UIAnimationCallback callback)
        {
            //Debug.Log(Time.time + " Open Panel:" + panel.name);
            panel.alpha = minAlpha;
            float fadeSpeed = Math.Abs(maxAlpha - minAlpha) / openTime;
            while (panel.alpha < maxAlpha)
            {
                panel.alpha = Mathf.MoveTowards(panel.alpha, maxAlpha, fadeSpeed * Time.fixedDeltaTime);

                yield return null;
            }
            
            callback();
        }
    }
}
