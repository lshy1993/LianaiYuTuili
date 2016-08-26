using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.UIScript
{
    public class DialogBoxAnimation : PanelAnimation
    {
        public override void BeforeClose()
        {
            //base.BeforeClose();
            transform.Find("Click_Container").gameObject.SetActive(false);
        }
        public override IEnumerator OpenSequence(UIAnimationCallback callback)
        {
            //transform.Find("Click_Container").gameObject.SetActive(false);
            return base.OpenSequence(() =>
            {
                transform.Find("Click_Container").gameObject.SetActive(true);

                callback();
            });
        }

        //public override IEnumerator CloseSequence(UIAnimationCallback callback)
        //{
        //    transform.Find("Click_Container").gameObject.SetActive(false);
        //    return base.CloseSequence(callback);
        //}
    }
}
