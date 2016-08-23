using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.UIScript
{
    public class FreeFade : DetectFade
    {
        private float y;

        void OnEnable()
        {
            Open();
        }

        public override void Open(float fadein = 0.3f)
        {
            //功能按钮向上移动
            y = -410;
            open = true;
        }
        public override void Close(float fadeout = 0.3f)
        {
            //功能按钮向下隐藏
            y = -310;
            close = true;
        }

        new void FixedUpdate()
        {
            if (open && y < -310)
            {
                y = Mathf.MoveTowards(y, -310, 100 / 0.2f * Time.deltaTime);
                this.transform.localPosition = new Vector3(-350,y);
            }
            if (open && y == -310) open = false;
            if (close && y > -410)
            {
                y = Mathf.MoveTowards(y, -410, 100 / 0.2f * Time.deltaTime);
                this.transform.localPosition = new Vector3(-350, y);
            }
            if (close && y == -410) close = false;

        }

        
    }
}
