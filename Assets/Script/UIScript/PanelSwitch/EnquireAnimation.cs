using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.UIScript
{
    class EnquireAnimation : PanelAnimation
    {
        private EnquireUIManager uiManager;
        private GameObject hpmpContainer, evidenceContainer, timeObject;
        private UILabel currentLabel;

        public override void Init()
        {
            uiManager = this.transform.GetComponent<EnquireUIManager>();
            hpmpContainer = this.transform.Find("HPMP_Container").gameObject;
            evidenceContainer = transform.Find("Scroll View").gameObject;
            timeObject = this.transform.Find("ProgressBack_Sprite").gameObject;
            currentLabel = transform.Find("CurrentText_Label").gameObject.GetComponent<UILabel>();
            base.Init();
        }

        public override IEnumerator CloseSequence(UIAnimationCallback callback)
        {
            panel.alpha = 1;
            float x = 1;
            float hpx, eviy, timex;
            while (x > 0)
            {
                x = Mathf.MoveTowards(x, 0, Time.deltaTime * 2);
                hpx = -800 + 300 * x;
                eviy = -440 + 150 * x;
                timex = 670 - 70 * x;
                hpmpContainer.transform.localPosition = new Vector3(hpx, 320, 0);
                evidenceContainer.transform.localPosition = new Vector3(0, eviy, 0);
                timeObject.transform.localPosition = new Vector3(timex, 80, 0);
                currentLabel.alpha = x;
                yield return null;
            }
            callback();
        }
        public override IEnumerator OpenSequence(UIAnimationCallback callback)
        {
            panel.alpha = 1;
            float x = 0;
            float hpx, eviy, timex;
            while (x < 1)
            {
                x = Mathf.MoveTowards(x, 1, Time.deltaTime * 2);
                hpx = -800 + 300 * x;
                eviy = -440 + 150 * x;
                timex = 670 - 70 * x;
                hpmpContainer.transform.localPosition = new Vector3(hpx, 320, 0);
                evidenceContainer.transform.localPosition = new Vector3(0, eviy, 0);
                timeObject.transform.localPosition = new Vector3(timex, 80, 0);
                yield return null;
            }
            //yield return new WaitForSeconds(0.5f);
            //uiManager.WheelStart();
            callback();
        }
    }
}
