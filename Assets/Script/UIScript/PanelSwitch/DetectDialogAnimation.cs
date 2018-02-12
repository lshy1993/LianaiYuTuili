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
        private int[] destinations;

        public override void Init()
        {
            base.Init();
        }

        private void InitPosition()
        {
            //默认按钮初始位置于边框外
            foreach (Transform child in this.transform)
            {
                child.localPosition = new Vector3(0, 410);
            }
            //计算按钮间隔
            int n = this.transform.childCount;
            int d = (670 - 50 * n) / (n + 1);
            destinations = new int[n];
            for (int i = 0; i < destinations.Length; i++)
            {
                //己算每个按钮的y值
                destinations[i] = 360 - ((i + 1) * d + i * 50);
            }
        }

        public override IEnumerator OpenSequence(UIAnimationCallback callback)
        {
            panel.alpha = 1;
            InitPosition();
            float showtime = 0.2f;
            while (!AllAriveFinialDest())
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    float y = Mathf.MoveTowards(transform.GetChild(i).localPosition.y, destinations[i], (360 - destinations[i]) / showtime * Time.deltaTime);
                    transform.GetChild(i).localPosition = new Vector3(0, y);
                }
                yield return null;
            }
            callback();
        }

        public override IEnumerator CloseSequence(UIAnimationCallback callback)
        {
            yield return null;
            callback();
            //return base.CloseSequence(callback);
        }

        private bool AllAriveFinialDest()
        {
            int n = transform.childCount - 1;
            return this.transform.GetChild(n).localPosition.y == destinations[n];
        }

    }
}
