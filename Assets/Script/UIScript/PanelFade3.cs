using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.UIScript
{
    /// <summary>
    /// 参见http://wiki.unity3d.com/index.php/FadeObjectInOut
    /// 实现一个可以执行异步操作的Fade
    /// </summary>
    public class PanelFade3 : MonoBehaviour
    {
        internal bool updating;
        internal float fadeSpeed;
        private bool close, open;
        private UIPanel panel;

        public void Close(float fadeOutTime)
        {
            this.fadeSpeed = 1 / fadeOutTime;

            this.close = true;
            this.open = false;

        }

        public void Open(float fadeInTime)
        {
            this.fadeSpeed = 1 / fadeInTime;
            panel.alpha = 0;


            this.open = true;
            this.close = false;
        }

        // publically editable speed
        public float fadeDelay = 0.0f;
        public float fadeTime = 0.5f;
        public bool fadeInOnStart = false;
        public bool fadeOutOnStart = false;
        private bool logInitialFadeSequence = false;




        // store colours
        private Color[] colors;

        // allow automatic fading on the start of the scene
        IEnumerator Start()
        {
            //yield return null; 
            yield return new WaitForSeconds(fadeDelay);

            if (fadeInOnStart)
            {
                logInitialFadeSequence = true;
                FadeIn();
            }

            if (fadeOutOnStart)
            {
                FadeOut(fadeTime);
            }
        }




        // check the alpha value of most opaque object
        float MaxAlpha()
        {
            float maxAlpha = 0.0f;
            Renderer[] rendererObjects = GetComponentsInChildren<Renderer>();
            foreach (Renderer item in rendererObjects)
            {
                maxAlpha = Mathf.Max(maxAlpha, item.material.color.a);
            }
            return maxAlpha;
        }

        // fade sequence
        IEnumerator FadeSequence(float fadingOutTime)
        {
            // log fading direction, then precalculate fading speed as a multiplier
            bool fadingOut = (fadingOutTime < 0.0f);
            float fadingOutSpeed = 1.0f / fadingOutTime;

            // grab all child objects
            Renderer[] rendererObjects = GetComponentsInChildren<Renderer>();
            if (colors == null)
            {
                //create a cache of colors if necessary
                colors = new Color[rendererObjects.Length];

                // store the original colours for all child objects
                for (int i = 0; i < rendererObjects.Length; i++)
                {
                    colors[i] = rendererObjects[i].material.color;
                }
            }

            // make all objects visible
            for (int i = 0; i < rendererObjects.Length; i++)
            {
                rendererObjects[i].enabled = true;
            }


            // get current max alpha
            float alphaValue = MaxAlpha();


            // This is a special case for objects that are set to fade in on start. 
            // it will treat them as alpha 0, despite them not being so. 
            if (logInitialFadeSequence && !fadingOut)
            {
                alphaValue = 0.0f;
                logInitialFadeSequence = false;
            }

            // iterate to change alpha value 
            while ((alphaValue >= 0.0f && fadingOut) || (alphaValue <= 1.0f && !fadingOut))
            {
                alphaValue += Time.deltaTime * fadingOutSpeed;

                for (int i = 0; i < rendererObjects.Length; i++)
                {
                    Color newColor = (colors != null ? colors[i] : rendererObjects[i].material.color);
                    newColor.a = Mathf.Min(newColor.a, alphaValue);
                    newColor.a = Mathf.Clamp(newColor.a, 0.0f, 1.0f);
                    rendererObjects[i].material.SetColor("_Color", newColor);
                }

                yield return null;
            }

            // turn objects off after fading out
            if (fadingOut)
            {
                for (int i = 0; i < rendererObjects.Length; i++)
                {
                    rendererObjects[i].enabled = false;
                }
            }


            Debug.Log("fade sequence end : " + fadingOut);

        }


        void FadeIn()
        {
            FadeIn(fadeTime);
        }

        void FadeOut()
        {
            FadeOut(fadeTime);
        }

        void FadeIn(float newFadeTime)
        {
            StopAllCoroutines();
            StartCoroutine("FadeSequence", newFadeTime);
        }

        void FadeOut(float newFadeTime)
        {
            StopAllCoroutines();
            StartCoroutine("FadeSequence", -newFadeTime);
        }

        //panel = this.transform.GetComponent<UIPanel>();
        //void Awake()
        //{
        //    updating = false;
        //    fadeSpeed = 1 / 0.5f;
        //    panel = this.GetComponentInParent<UIPanel>();
        //    Debug.Log("PanelFade2: " + panel.name);
        //}

        //void FixedUpdate()
        //{
        //    if (close)
        //    {
        //        updating = true;
        //        //Debug.Log(panel.name);
        //        panel.alpha = Mathf.MoveTowards(panel.alpha, 0, fadeSpeed * Time.fixedDeltaTime);

        //        if (panel.alpha < 0.0000001)
        //        {
        //            updating = false;
        //            close = false;
        //            panel.transform.gameObject.SetActive(false);
        //        }
        //    }

        //    if (open)
        //    {
        //        updating = true;

        //        panel.alpha = Mathf.MoveTowards(panel.alpha, 1, fadeSpeed * Time.fixedDeltaTime);

        //        if (panel.alpha > 0.9999999)
        //        {
        //            updating = false;
        //            open = false;
        //        }
        //    }
        //}
    }

}
