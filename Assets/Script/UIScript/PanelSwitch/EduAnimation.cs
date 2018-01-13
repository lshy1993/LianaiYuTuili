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
        private GameObject backgroundContainer, timeContainer, charaContainer, selectionContainer, scheduleContainer;
        private GameObject buttonContainer, spriteContainer;
        public override void Init()
        {
            backgroundContainer = this.transform.Find("BackGround_Container").gameObject;
            timeContainer = this.transform.Find("Time_Container").gameObject;

            //charaContainer = this.transform.Find("CharaInfo_Container").gameObject;
            //selectionContainer = this.transform.Find("Selection_Container").gameObject;

            charaContainer = this.transform.Find("NewCharaInfo_Container").gameObject;
            selectionContainer = this.transform.Find("NewSelection_Container").gameObject;
            scheduleContainer = this.transform.Find("Schedule_Container").gameObject;

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
            float timey, charax, selectx;
            float x = 1;
            while (x > 0)
            {
                x = Mathf.MoveTowards(x, 0, 1 / closeTime * Time.deltaTime);
                timey = 400 - 70 * x;
                charax = 951 - 622 * x;
                selectx = -934 + 588 * x;

                timeContainer.transform.localPosition = new Vector3(160, timey);
                charaContainer.transform.localPosition = new Vector3(charax, 0);
                selectionContainer.transform.localPosition = new Vector3(selectx, 0);

                scheduleContainer.GetComponent<UIWidget>().alpha = x;
                //charaContainer.GetComponent<UIWidget>().alpha = x;
                //selectionContainer.GetComponent<UIWidget>().alpha = x;
                yield return null;
            }
            selectionContainer.SetActive(false);
        }

        private IEnumerator ShowOpen()
        {
            scheduleContainer.SetActive(true);
            charaContainer.SetActive(true);
            selectionContainer.SetActive(true);
            buttonContainer.SetActive(true);

            scheduleContainer.GetComponent<UIWidget>().alpha = 0;

            float timey, charax, selectx;
            float x = 0;
            while (x < 1)
            {
                x = Mathf.MoveTowards(x, 1, 1 / openTime * Time.deltaTime);
                timey = 400 - 70 * x;
                charax = 951 - 622 * x;
                selectx = -934 + 588 * x;

                timeContainer.transform.localPosition = new Vector3(160, timey);
                charaContainer.transform.localPosition = new Vector3(charax, 0);
                selectionContainer.transform.localPosition = new Vector3(selectx, 0);

                scheduleContainer.GetComponent<UIWidget>().alpha = x;
                //charaContainer.GetComponent<UIWidget>().alpha = x;
                //selectionContainer.GetComponent<UIWidget>().alpha = x;
                yield return null;
            }
        }
    }
}
