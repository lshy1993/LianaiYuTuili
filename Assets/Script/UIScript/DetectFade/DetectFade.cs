using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.UIScript
{
    public class DetectFade : MonoBehaviour
    {
        internal bool updating;
        internal float fadeSpeed;
        private bool close, open;
        private UIWidget container;

        void Awake()
        {
            updating = false;
            fadeSpeed = 1 / 0.5f;
            container = this.GetComponentInParent<UIWidget>();
        }

        public virtual void Close(float fadeOutTime = 0.3f)
        {
            this.fadeSpeed = 1 / fadeOutTime;

            this.close = true;
            this.open = false;
        }

        public virtual void Open(float fadeInTime = 0.3f)
        {
            this.fadeSpeed = 1 / fadeInTime;
            container.alpha = 0;

            this.open = true;
            this.close = false;
        }

        public virtual void FixedUpdate()
        {
            if (close)
            {
                updating = true;
                container.alpha = Mathf.MoveTowards(container.alpha, 0, fadeSpeed * Time.fixedDeltaTime);

                if (container.alpha < 0.0000001)
                {
                    updating = false;
                    close = false;
                    container.transform.gameObject.SetActive(false);
                }
            }

            if (open)
            {
                updating = true;

                container.alpha = Mathf.MoveTowards(container.alpha, 1, fadeSpeed * Time.fixedDeltaTime);

                if (container.alpha > 0.9999999)
                {
                    updating = false;
                    open = false;
                }
            }
        }
    }

}
