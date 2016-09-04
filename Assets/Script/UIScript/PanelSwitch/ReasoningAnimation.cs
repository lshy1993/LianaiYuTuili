using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.UIScript
{
    class ReasoningAnimation : PanelAnimation
    {
        private EnquireUIManager uiManager;
        private GameObject backgroundContainer, questionContainer, questionLabel;
        private GameObject textContainer, evidenceContainer;
        //private TypewriterEffect te;

        public override void Init()
        {
            uiManager = this.transform.GetComponent<EnquireUIManager>();
            backgroundContainer = this.transform.Find("BackGround_Container").gameObject;
            questionContainer = this.transform.Find("Question_Container").gameObject;
            questionLabel = this.transform.Find("Question_Container/Question_Label").gameObject;

            //te = questionLabel.GetComponent<TypewriterEffect>();

            textContainer = this.transform.Find("TextSelect_Container").gameObject;
            evidenceContainer = this.transform.Find("EvidenceSelect_Container").gameObject;

            base.Init();
        }

        public override IEnumerator CloseSequence(UIAnimationCallback callback)
        {
            float x = 1;
            while (x > 0)
            {
                x = Mathf.MoveTowards(x, 0, 1 / closeTime * Time.deltaTime);
                panel.alpha = x;
                yield return null;
            }
            backgroundContainer.SetActive(false);
            questionContainer.SetActive(false);
            
            questionLabel.SetActive(false);

            textContainer.SetActive(false);
            evidenceContainer.SetActive(false);

            callback();
        }

        public override IEnumerator OpenSequence(UIAnimationCallback callback)
        {
            panel.alpha = 1;
            backgroundContainer.SetActive(true);
            backgroundContainer.GetComponent<UIWidget>().alpha = 0;
            questionContainer.SetActive(true);
            questionContainer.GetComponent<UIWidget>().alpha = 0;
            float x = 0;
            while (x < 1)
            {
                x = Mathf.MoveTowards(x, 1, 1 / openTime * Time.fixedDeltaTime);
                backgroundContainer.GetComponent<UIWidget>().alpha = x;
                questionContainer.GetComponent<UIWidget>().alpha = x;
                yield return null;
            }
            questionLabel.SetActive(true);
            callback();
        }
    }
}
