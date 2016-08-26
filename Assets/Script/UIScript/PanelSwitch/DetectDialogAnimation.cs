using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.UIScript
{
    public class DetectDialogAnimation : PanelAnimation
    {
        private List<GameObject> dialogButtons;
        private int[] destinations;

        public override void Init()
        {
            base.Init();
        }

        //private bool animate = false;

        public void setDialogBtns(List<GameObject> dialogButtons)
        {
            this.dialogButtons = dialogButtons;
        }

        //    public override void Open(float fadein = 0.3f)
        //    {
        //        base.Open(fadein);
        //        InitPosition();
        //        animate = true;
        //    }

        private void InitPosition()
        {
            foreach (GameObject btn in dialogButtons)
            {
                btn.transform.localPosition = new Vector3(0, 410);
            }

            int n = dialogButtons.Count;
            int d = (670 - 50 * n) / (n + 1);
            destinations = new int[dialogButtons.Count];
            for (int i = 0; i < destinations.Length; i++)
            {
                destinations[i] = 360 - ((i + 1) * d + i * 50);
            }
        }

        public override IEnumerator OpenSequence(UIAnimationCallback callback)
        {
            panel.alpha = 1;
            InitPosition();
            //foreach (GameObject obj in dialogButtons)
            //{
            //    obj.GetComponent<UI2DSprite>().alpha = 1;
            //}
            float showtime = 0.2f;
            while (!AllAriveFinialDest())
            {
                for (int i = 0; i < dialogButtons.Count; i++)
                {
                    float y = Mathf.MoveTowards(dialogButtons[i].transform.localPosition.y, destinations[i], (360 - destinations[i]) / showtime * Time.fixedDeltaTime);
                    dialogButtons[i].transform.localPosition = new Vector3(0, y);
                }
                yield return null;
            }
            callback();
        }

        private bool AllAriveFinialDest()
        {
            //for (int i = 0; i < dialogButtons.Count; i++)
            //{
                if ((int)dialogButtons[dialogButtons.Count - 1].transform.localPosition.y == (int)destinations[dialogButtons.Count - 1]) return true;
            //}

            return false;
        }

        //    new void FixedUpdate()
        //    {
        //        base.FixedUpdate();
        //        if (animate)
        //        {
        //            for (int i = 0; i < dialogButtons.Count; i++)
        //            {
        //                float y = Mathf.MoveTowards(dialogButtons[i].transform.localPosition.y, destinations[i], (360 - destinations[i]) / 0.3f * Time.fixedDeltaTime);
        //                dialogButtons[i].transform.localPosition = new Vector3(0, y);
        //            }
        //        }

        //    }

    }
}
