using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.UIScript
{
    public class PhoneAnimation : MonoBehaviour
    {
        //打开手机
        public void OpenPhone()
        {
            //Debug.Log("open phone");
            StartCoroutine(Fadein(0.2f));
        }

        public void ClosePhone()
        {
            StartCoroutine(Fadeout(0.2f));
        }

        private IEnumerator Fadein(float time)
        {
            UIPanel panel =transform.GetComponent<UIPanel>();
            float fmove = time == 0 ? 1 : 0;
            panel.alpha = fmove;
            transform.gameObject.SetActive(true);
            while (fmove < 1f)
            {
                fmove = Mathf.MoveTowards(fmove, 1f, Time.deltaTime / time);
                panel.alpha = fmove;
                yield return null;
            }
        }
        
        IEnumerator Fadeout(float time)
        {
            UIPanel panel = transform.GetComponent<UIPanel>();
            float fmove = time == 0 ? 0 : 1;
            panel.alpha = fmove;
            while (fmove > 0)
            {
                fmove = Mathf.MoveTowards(fmove, 0, Time.deltaTime / time);
                panel.alpha = fmove;
                yield return null;
            }
            transform.gameObject.SetActive(false);
        }
    }
}
