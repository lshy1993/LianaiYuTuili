using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class LogoUIManager : MonoBehaviour
{
    //private GameObject root;
    private GameObject animationContainer, baseContainer, clickCon;
    private GameObject logo1, logo2, logo3;

    private const float fadeInTime = 0.5f;
    private const float fadeOutTime = 0.5f;

    public SoundManager sm;
    public PanelSwitch ps;

    private bool loadfinish;
    private int currentStep;

    void Awake()
    {
        //root = GameObject.Find("UI Root");
        baseContainer = this.transform.Find("Base_Container").gameObject;
        logo1 = this.transform.Find("Logo1_Container").gameObject;
        logo2 = this.transform.Find("Logo2_Container").gameObject;
        logo3 = this.transform.Find("Logo3_Container").gameObject;
        clickCon = this.transform.Find("Click_Container").gameObject;
        loadfinish = false;
    }

    void OnEnable()
    {
        //Debug.Log("Start!");
        StartCoroutine(OpenAnimate(0));
    }

    /// <summary>
    /// 跳过
    /// </summary>
    public void Skip()
    {
        if (currentStep == 0) return;
        if (currentStep == 2) return;
        if (currentStep == 4) return;
        StopAllCoroutines();
        StartCoroutine(SkipAnimate(currentStep));
    }

    private IEnumerator SkipAnimate(int step)
    {
        Debug.Log("step:" + step);
        switch (step)
        {
            case 1:
                currentStep = 2;
                yield return StartCoroutine(FadeOutLogo(logo1, true));
                StartCoroutine(OpenAnimate(3));
                break;
            case 3:
                currentStep = 4;
                yield return StartCoroutine(FadeOutLogo(logo2, true));
                StartCoroutine(OpenAnimate(7));
                break;
            case 5:
                yield return StartCoroutine(FadeOutLogo(logo3, true));
                StartCoroutine(OpenAnimate(6));
                break;
        }
    }

    private IEnumerator OpenAnimate(int step)
    {
        currentStep = step;
        switch (step)
        {
            case 0:
                //运行加载？
                DataLoad();
                yield return StartCoroutine(LoadText());
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(OpenAnimate(1));
                break;
            case 1:
                //淡入游戏制作组Logo
                yield return StartCoroutine(FadeInLogo(logo1));
                yield return new WaitForSeconds(1.5f);
                StartCoroutine(OpenAnimate(2));
                break;
            case 2:
                //淡出
                yield return StartCoroutine(FadeOutLogo(logo1));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(OpenAnimate(3));
                break;
            case 3:
                //淡入学校Logo
                yield return StartCoroutine(FadeInLogo(logo2));
                yield return new WaitForSeconds(1.5f);
                StartCoroutine(OpenAnimate(4));
                break;
            case 4:
                //淡出
                yield return StartCoroutine(FadeOutLogo(logo2));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(OpenAnimate(7));
                break;
            case 5:
                //淡入其他组Logo
                //yield return StartCoroutine(FadeInLogo(logo3));
                //yield return new WaitForSeconds(1f);
                StartCoroutine(OpenAnimate(6));
                break;
            case 6:
                //淡出
                //yield return StartCoroutine(FadeOutLogo(logo3));
                //yield return new WaitForSeconds(0.5f);
                StartCoroutine(OpenAnimate(7));
                break;
            case 7:
                StopAllCoroutines();
                clickCon.SetActive(false);
                //切换至标题画面
                ps.SwitchTo_VerifyIterative("Title_Panel");
                //this.transform.gameObject.SetActive(false);
                break;

        }


    }

    private IEnumerator FadeInLogo(GameObject target)
    {
        target.SetActive(true);
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / fadeInTime * Time.deltaTime);
            target.GetComponent<UIWidget>().alpha = t;
            yield return null;
        }
    }

    private IEnumerator FadeOutLogo(GameObject target, bool fast=false)
    {
        float t = 1;
        float tall = fast ? 0.2f : fadeOutTime;
        while (t > 0)
        {
            t = Mathf.MoveTowards(t, 0, 1 / tall * Time.deltaTime);
            target.GetComponent<UIWidget>().alpha = t;
            yield return null;
        }
        target.SetActive(false);
    }

    private void DataLoad()
    {
        //string[] filenames = { "BREAK", "HEAT UP", "HOPE VS DESPAIR", "Title", "Map","FirstSeason" };
        //AudioClip[] acs = Resources.LoadAll<AudioClip>("Audio");

        //Dictionary<string, AudioClip> auDic = new Dictionary<string, AudioClip>();

        //foreach (AudioClip ac in acs)
        //{
            //AudioClip ac = Resources.Load<AudioClip>("Audio/" + str);
           // auDic.Add(ac.name, ac);
        //}
        //sm.SetAuDic(auDic);
        loadfinish = true;
    }

    private IEnumerator LoadText()
    {
        baseContainer.SetActive(true);
        UILabel lb = baseContainer.transform.Find("Loading_Label").GetComponent<UILabel>();
        lb.text = "读取中";
        while (!loadfinish)
        {
            if (lb.text == "读取中...") lb.text = "读取中";
            lb.text += ".";
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);
        baseContainer.SetActive(false);
        //ps.SwitchTo_VerifyIterative("Title_Panel");
    }
}
