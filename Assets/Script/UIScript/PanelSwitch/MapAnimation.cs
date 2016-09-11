using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.UIScript
{
    class MapAnimation : PanelAnimation
    {
        private GameObject timeContainer, charaContainer, functonContainer;
        private GameObject placebtnContainer, outterbtnContainer;

        public bool isout;

        public override void Init()
        {
            timeContainer = this.transform.Find("Time_Container").gameObject;
            charaContainer = this.transform.Find("CharaInfo_Container").gameObject;
            functonContainer = this.transform.Find("Function_Container").gameObject;

            placebtnContainer = this.transform.Find("PlaceButton_Container").gameObject;
            outterbtnContainer = this.transform.Find("PlaceButtonOut_Container").gameObject;

            base.Init();
        }

        public override IEnumerator CloseSequence(UIAnimationCallback callback)
        {
            StartCoroutine(ShowClose());
            float x = 1;
            while (x > 0)
            {
                x = Mathf.MoveTowards(x, 0, 1 / closeTime * Time.deltaTime);
                panel.alpha = x;
                yield return null;
            }
            //panel.gameObject.SetActive(false);
            callback();
        }

        public override IEnumerator OpenSequence(UIAnimationCallback callback)
        {
            float x = 0;
            while (x < 1)
            {
                x = Mathf.MoveTowards(x, 1, 1 / openTime * Time.deltaTime);
                panel.alpha = x;
                yield return null;
            }
            StartCoroutine(ShowOpen());
            callback();
        }

        #region 关闭时特效
        private IEnumerator ShowClose()
        {
            float timex, timey, charay, funcx;
            float t = 1;
            while (t > 0)
            {
                t = Mathf.MoveTowards(t, 0, 1 / closeTime * Time.deltaTime);
                timex = -820 + 180 * t;
                timey = 540 - 180 * t;
                charay = 410 - 100 * t;
                funcx = -690 + 100 * t;
                timeContainer.transform.localPosition = new Vector3(timex, timey);
                charaContainer.transform.localPosition = new Vector3(160, charay);
                functonContainer.transform.localPosition = new Vector3(funcx, 0);
                if (isout)
                {
                    outterbtnContainer.GetComponent<UIWidget>().alpha = t;
                }
                else
                {
                    placebtnContainer.GetComponent<UIWidget>().alpha = t;
                }
                yield return null;
            }
            if (isout)
            {
                outterbtnContainer.SetActive(false);
            }
            else
            {
                placebtnContainer.SetActive(false);
            }
        }
        #endregion

        #region 打开时特效
        private IEnumerator ShowOpen()
        {
            Hashtable gVars = DataManager.GetInstance().GetGameVars();
            DateTime date = (DateTime)gVars["日期"];
            int x = Convert.ToInt32(date.DayOfWeek);
            if (x == 6 || x == 7)
            {
                this.isout = true;
            }
            else
            {
                this.isout = false;
            }
            if (isout)
            {
                outterbtnContainer.SetActive(true);
                outterbtnContainer.GetComponent<UIWidget>().alpha = 0;
            }
            else
            {
                placebtnContainer.SetActive(true);
                placebtnContainer.GetComponent<UIWidget>().alpha = 0;
            }
            float timex, timey, charay, funcx;
            float t = 0;
            while (t < 1)
            {
                t = Mathf.MoveTowards(t, 1, 1 / openTime * Time.deltaTime);
                timex = -820 + 180 * t;
                timey = 540 - 180 * t;
                charay = 410 - 100 * t;
                funcx = -690 + 100 * t;
                timeContainer.transform.localPosition = new Vector3(timex, timey);
                charaContainer.transform.localPosition = new Vector3(160, charay);
                functonContainer.transform.localPosition = new Vector3(funcx, 0);
                if (isout)
                {
                    outterbtnContainer.GetComponent<UIWidget>().alpha = t;
                }
                else
                {
                    placebtnContainer.GetComponent<UIWidget>().alpha = t;
                }
                yield return null;
            }
        }
        #endregion
    }
}
