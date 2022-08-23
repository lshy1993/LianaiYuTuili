using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.UIScript
{
    public class AvgAnimation : PanelAnimation
    {
        //TODO：读档的预处理？

        //public override IEnumerator OpenSequence(UIAnimationCallback callback)
        //{
        //    return base.OpenSequence(() =>
        //    {
        //        transform.Find("Background_Panel").gameObject.SetActive(true);
        //        transform.Find("CharaGraph_Panel").gameObject.SetActive(true);
        //        callback();
        //    });
        //}

        public override IEnumerator CloseSequence(UIAnimationCallback callback)
        {
            return base.CloseSequence(() =>
            {
                //清除背景与前景
                transform.Find("Background_Panel/BackGround_Sprite").gameObject.GetComponent<UI2DSprite>().sprite2D = null;
                transform.Find("CharaGraph_Panel").gameObject.transform.DestroyChildren();
                //关闭其他panel
                transform.Find("Selection_Panel").gameObject.SetActive(false);

                callback();
            });
        }

    }
}
