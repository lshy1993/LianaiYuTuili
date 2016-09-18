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
        private GameObject backgroundContainer, timeContainer, charaContainer, selectionContainer, functionContainer;
        private UIWidget selectionWidget;

        public override void Init()
        {
            backgroundContainer = this.transform.Find("BackGround_Container").gameObject;
            timeContainer = this.transform.Find("Time_Container").gameObject;
            charaContainer = this.transform.Find("CharaInfo_Container").gameObject;
            selectionContainer = this.transform.Find("Selection_Container").gameObject;
            functionContainer = this.transform.Find("Function_Container").gameObject;
            selectionWidget = selectionContainer.GetComponent<UIWidget>();
            base.Init();
        }

        public override IEnumerator CloseSequence(UIAnimationCallback callback)
        {
            StartCoroutine(ShowClose());
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
            StartCoroutine(ShowOpen());
            callback();
        }

        private IEnumerator ShowClose()
        {
            float timey, charay, funx;
            float x = 1;
            while (x > 0)
            {
                x = Mathf.MoveTowards(x, 0, 1 / closeTime * Time.deltaTime);
                timey = 390 - 60 * x;
                charay = -435 + 150 * x;
                funx = -690 + 100 * x;
                timeContainer.transform.localPosition = new Vector3(160, timey);
                charaContainer.transform.localPosition = new Vector3(0, charay);
                functionContainer.transform.localPosition = new Vector3(funx, 0);
                selectionWidget.alpha = x;
                yield return null;
            }
            selectionContainer.SetActive(false);
        }

        private IEnumerator ShowOpen()
        {
            selectionContainer.SetActive(true);
            selectionWidget.alpha = 0;
            float timey, charay, funx;
            float x = 0;
            while (x < 1)
            {
                x = Mathf.MoveTowards(x, 1, 1 / openTime * Time.deltaTime);
                timey = 390 - 60 * x;
                charay = -435 + 150 * x;
                funx = -690 + 100 * x;
                timeContainer.transform.localPosition = new Vector3(160, timey);
                charaContainer.transform.localPosition = new Vector3(0, charay);
                functionContainer.transform.localPosition = new Vector3(funx, 0);
                selectionWidget.alpha = x;
                yield return null;
            }
        }
    }
}
