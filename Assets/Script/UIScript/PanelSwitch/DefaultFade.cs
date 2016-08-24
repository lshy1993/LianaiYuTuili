using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.UIScript
{
    public class DefaultFade : MonoBehaviour, PanelFadeInterface
    {

        public UIPanel panel;
        public float maxAlpha, minAlpha;
        public float closeTime, openTime;

        public void Close(UIAnimationCallback callback)
        {
            StopAllCoroutines();
            StartCoroutine(CloseSequence(callback));
        }

        public void Open(UIAnimationCallback callback)
        {
            StopAllCoroutines();
            StartCoroutine(OpenSequence(callback));
        }


        private IEnumerator CloseSequence(UIAnimationCallback callback)
        {
            Debug.Log("Close Panel:" + panel.name);
            float fadeSpeed = Math.Abs(maxAlpha - minAlpha) / closeTime;
            //panel.alpha = maxAlpha;
            //Debug.Log("Time.deltaTime" + Time.deltaTime);
            var t = DateTime.Now;
            while (panel.alpha > minAlpha)
            {
                //Debug.Log("CloseAlpha: " + panel.alpha);
                panel.alpha = Mathf.MoveTowards(panel.alpha, minAlpha, fadeSpeed * Time.fixedDeltaTime);

                yield return null;
            }
            //Debug.Log("Close Panel Finish alpha = " + panel.alpha);

            Debug.Log("Close Time:" + (t - DateTime.Now).TotalMilliseconds);

            callback();
        }

        private IEnumerator OpenSequence(UIAnimationCallback callback)
        {
            Debug.Log("Open Panel:" + panel.name);
            float fadeSpeed = Math.Abs(maxAlpha - minAlpha) / openTime;
            //panel.alpha = minAlpha;
            var t = DateTime.Now;
            while (panel.alpha < maxAlpha)
            {
                //Debug.Log("OpenAlpha: " + panel.alpha);
                panel.alpha = Mathf.MoveTowards(panel.alpha, maxAlpha, fadeSpeed * Time.fixedDeltaTime);

                yield return null;
            }

            Debug.Log("Open Time:" + (t - DateTime.Now).TotalMilliseconds);
            callback();
        }

        public void Open()
        {
            Open(() => { });
        }

        public void Close()
        {
            Close(() => { });
        }
    }
}
