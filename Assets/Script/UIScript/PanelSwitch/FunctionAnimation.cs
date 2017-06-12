using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.UIScript
{
    public class FunctionAnimation : PanelAnimation
    {
        private int origin = -385;
        private int final = -335;

        public UIButton move, dialog, invest;

        public override void BeforeClose()
        {
            move.enabled = false;
            dialog.enabled = false;
            invest.enabled = false;
        }

        public override IEnumerator CloseSequence(UIAnimationCallback callback)
        {
            //        y = Mathf.MoveTowards(y, -310, 100 / 0.2f * Time.deltaTime);
            //        this.transform.localPosition = new Vector3(-350,y);
            //return base.CloseSequence(callback);
            panel.alpha = 1;
            float y = transform.localPosition.y;
            float x = transform.localPosition.x;
            while (y > origin)
            {
                y = Mathf.MoveTowards(y, origin, Math.Abs(origin - final) / closeTime * Time.fixedDeltaTime);
                this.transform.localPosition = new Vector3(x, y);
                yield return null;
            }
            callback();
        }

        public override IEnumerator OpenSequence(UIAnimationCallback callback)
        {
            //Debug.Log("panel == null?" + panel == null + "alpha?" + panel.alpha);
            //UIWidget wi = (UIWidget)panel;
            panel.alpha = 1;
            float y = transform.localPosition.y;
            float x = transform.localPosition.x;
            while (y < final)
            {
                y = Mathf.MoveTowards(y, final, Math.Abs(origin - final) / openTime * Time.fixedDeltaTime);
                this.transform.localPosition = new Vector3(x, y);
                yield return null;
            }
            move.enabled = true;
            dialog.enabled = true;
            invest.enabled = true;
            callback();
        }
        //private float y;

        ////void OnEnable()
        ////{
        ////    Open();
        ////}

        //public override void Open(float fadein = 0.3f)
        //{
        //    //功能按钮向上移动
        //    y = -410;
        //    open = true;
        //}
        //public override void Close(float fadeout = 0.3f)
        //{
        //    //功能按钮向下隐藏
        //    y = -310;
        //    close = true;
        //}

        //new void FixedUpdate()
        //{
        //    if (open && y < -310)
        //    {
        //        y = Mathf.MoveTowards(y, -310, 100 / 0.2f * Time.deltaTime);
        //        this.transform.localPosition = new Vector3(-350,y);
        //    }
        //    if (open && y == -310) open = false;
        //    if (close && y > -410)
        //    {
        //        y = Mathf.MoveTowards(y, -410, 100 / 0.2f * Time.deltaTime);
        //        this.transform.localPosition = new Vector3(-350, y);
        //    }
        //    if (close && y == -410) close = false;

        //}


    }
}
