using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.UIScript
{
    class EduAnimation : PanelAnimation
    {
        private GameObject backgroundContainer, timeContainer, charaContainer;
        private GameObject selectionContainer, scheduleContainer, bottomContainer;
        private GameObject buttonContainer, spriteContainer;

        private const int startY_time = 585;
        private const int Y_time = 90;
        private const int startX_cha = 1426;
        private const int X_cha = 933;
        private const int startX_slc = -1401;
        private const int X_slc = 882;
        private const int startY_hint = -580;
        private const int Y_hint = 80;

        public override void Init()
        {
            backgroundContainer = this.transform.Find("BackGround_Container").gameObject;
            timeContainer = this.transform.Find("Time_Container").gameObject;

            charaContainer = this.transform.Find("NewCharaInfo_Container").gameObject;
            selectionContainer = this.transform.Find("NewSelection_Container").gameObject;
            scheduleContainer = this.transform.Find("Schedule_Container").gameObject;
            bottomContainer = this.transform.Find("BottomHint_Container").gameObject;

            spriteContainer = selectionContainer.transform.Find("QSprite_Container").gameObject;
            buttonContainer = selectionContainer.transform.Find("Button_Container").gameObject;

            base.Init();
        }

        public override IEnumerator CloseSequence(UIAnimationCallback callback)
        {
            yield return StartCoroutine(ShowClose());
            float x = 1;
            while (x > 0)
            {
                x = Mathf.MoveTowards(x, 0, 1 / closeTime * Time.deltaTime);
                backgroundContainer.GetComponent<UIWidget>().alpha = x;
                yield return null;
            }
            backgroundContainer.SetActive(false);
            callback();
        }

        public override IEnumerator OpenSequence(UIAnimationCallback callback)
        {
            panel.alpha = 1;
            backgroundContainer.SetActive(true);
            backgroundContainer.GetComponent<UIWidget>().alpha = 0;
            float x = 0;
            while (x < 1)
            {
                x = Mathf.MoveTowards(x, 1, 1 / openTime * Time.deltaTime);
                backgroundContainer.GetComponent<UIWidget>().alpha = x;
                yield return null;
            }
            yield return StartCoroutine(ShowOpen());
            callback();
        }

        private IEnumerator ShowClose()
        {
            spriteContainer.SetActive(false);
            float timey, charax, selectx, hinty;
            float t = 1;
            while (t > 0)
            {
                t = Mathf.MoveTowards(t, 0, 1 / closeTime * Time.deltaTime);
                timey = startY_time - Y_time * t;
                charax = startX_cha - X_cha * t;
                selectx = startX_slc + X_slc * t;
                hinty = startY_hint + Y_hint * t;

                timeContainer.transform.localPosition = new Vector3(390, timey);
                charaContainer.transform.localPosition = new Vector3(charax, 0);
                selectionContainer.transform.localPosition = new Vector3(selectx, 0);
                bottomContainer.transform.localPosition = new Vector3(0, hinty);

                scheduleContainer.GetComponent<UIWidget>().alpha = t;
                yield return null;
            }
            selectionContainer.SetActive(false);
            charaContainer.SetActive(false);
            scheduleContainer.SetActive(false);
        }

        private IEnumerator ShowOpen()
        {
            scheduleContainer.SetActive(true);
            charaContainer.SetActive(true);
            selectionContainer.SetActive(true);
            buttonContainer.SetActive(true);

            scheduleContainer.GetComponent<UIWidget>().alpha = 0;

            float timey, charax, selectx;
            float t = 0;
            while (t < 1)
            {
                t = Mathf.MoveTowards(t, 1, 1 / openTime * Time.deltaTime);
                timey = startY_time - Y_time * t;
                charax = startX_cha - X_cha * t;
                selectx = startX_slc + X_slc * t;

                timeContainer.transform.localPosition = new Vector3(390, timey);
                charaContainer.transform.localPosition = new Vector3(charax, 0);
                selectionContainer.transform.localPosition = new Vector3(selectx, 0);

                scheduleContainer.GetComponent<UIWidget>().alpha = t;
                yield return null;
            }
        }
    }
}
