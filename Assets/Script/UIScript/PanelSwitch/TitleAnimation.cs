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
        private GameObject startBtn, loadBtn, settingBtn, extraBtn, exitBtn;

        public override void Init()
        {
            startBtn = this.transform.Find("Title_Container/Button_Table/Start_Button").gameObject;
            loadBtn = this.transform.Find("Title_Container/Button_Table/Load_Button").gameObject;
            settingBtn = this.transform.Find("Title_Container/Button_Table/Setting_Button").gameObject;
            extraBtn = this.transform.Find("Title_Container/Button_Table/Extra_Button").gameObject;
            exitBtn = this.transform.Find("Title_Container/Button_Table/Exit_Button").gameObject;
            base.Init();
        }

        public override IEnumerator CloseSequence(UIAnimationCallback callback)
        {
            startBtn.GetComponent<UIButton>().enabled = false;
            loadBtn.GetComponent<UIButton>().enabled = false;
            settingBtn.GetComponent<UIButton>().enabled = false;
            extraBtn.GetComponent<UIButton>().enabled = false;
            exitBtn.GetComponent<UIButton>().enabled = false;
            yield return new WaitForSeconds(1f);
            panel.alpha = 1;
            float x = 1;
            while (x > 0)
            {
                x = Mathf.MoveTowards(x, 0, 1 / closeTime * Time.deltaTime);
                panel.alpha = x;
                Debug.Log(x);
                yield return null;
            }
            callback();
        }

        public override IEnumerator OpenSequence(UIAnimationCallback callback)
        {
            return base.OpenSequence(()=> {
                startBtn.GetComponent<UIButton>().enabled = true;
                loadBtn.GetComponent<UIButton>().enabled = true;
                settingBtn.GetComponent<UIButton>().enabled = true;
                extraBtn.GetComponent<UIButton>().enabled = true;
                exitBtn.GetComponent<UIButton>().enabled = true;
                callback();
            });
            
        }
    }
}
