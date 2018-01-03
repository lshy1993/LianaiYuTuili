using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.UIScript
{
    class TitleAnimation : PanelAnimation
    {
        private GameObject btnTable;
        private GameObject backSprite, titleLabel, copyLabel, versionLabel;

        public override void Init()
        {
            btnTable = this.transform.Find("Title_Container/Button_Table").gameObject;

            backSprite = this.transform.Find("Back_Sprite").gameObject;
            titleLabel = this.transform.Find("Title_Container/TitleText_Label").gameObject;
            copyLabel = this.transform.Find("Title_Container/CopyRight_Label").gameObject;
            versionLabel = this.transform.Find("Title_Container/Version_Label").gameObject;
            base.Init();
        }

        public override void BeforeClose()
        {
            //base.BeforeClose();
            BlockBtn(false);
        }

        public override IEnumerator CloseSequence(UIAnimationCallback callback)
        {
            //yield return new WaitForSeconds(1f);
            //BlockBtn(false);
            //panel.alpha = 1;
            //float x = 1;
            //while (x > 0)
            //{
            //    x = Mathf.MoveTowards(x, 0, 1 / closeTime * Time.deltaTime);
            //    panel.alpha = x;
            //    Debug.Log(x);
            //    yield return null;
            //}
            //callback();

            return base.CloseSequence(() => { callback(); });
        }

        public override IEnumerator OpenSequence(UIAnimationCallback callback)
        {
            //backSprite.GetComponent<UIRect>().alpha = 0;
            //backSprite.SetActive(true);
            btnTable.SetActive(false);
            titleLabel.GetComponent<UIRect>().alpha = 0;
            copyLabel.SetActive(false);
            versionLabel.SetActive(false);

            return base.OpenSequence(() =>
            {
                StartCoroutine(ShowText());
                StartCoroutine(ShowBtn());
                callback();
            });


            //panel.alpha = 1;

            //float x = 0;
            //while (x < 1)
            //{
            //    x = Mathf.MoveTowards(x, 1, 1 / openTime * Time.deltaTime);
            //    backSprite.GetComponent<UIRect>().alpha = x;
            //    yield return null;
            //}
            //StartCoroutine(ShowText());
            //StartCoroutine(ShowBtn());
            //callback();
        }

        private IEnumerator ShowText()
        {
            float x = 0;
            while (x < 1)
            {
                x = Mathf.MoveTowards(x, 1, 1 / 0.5f * Time.deltaTime);
                titleLabel.GetComponent<UIRect>().alpha = x;
                yield return null;
            }
            copyLabel.SetActive(true);
            versionLabel.SetActive(true);
        }

        private IEnumerator ShowBtn()
        {
            BlockBtn(false);
            btnTable.SetActive(true);
            for (int i = 0; i < btnTable.transform.childCount; i++)
            {
                btnTable.transform.GetChild(i).GetComponent<UIRect>().alpha = 0;
            }
            for (int i =  0; i <btnTable.transform.childCount; i++)
            {
                float x = 0;
                while (x < 1)
                {
                    x = Mathf.MoveTowards(x, 1, 1 / 0.2f * Time.deltaTime);
                    btnTable.transform.GetChild(i).GetComponent<UIRect>().alpha = x;
                    yield return null;
                }
            }
            BlockBtn(true);
        }

        private void BlockBtn(bool blocked)
        {
            for (int i = 0; i < btnTable.transform.childCount; i++)
            {
                btnTable.transform.GetChild(i).GetComponent<UIButton>().enabled = blocked;
            }
        }

    }
}
