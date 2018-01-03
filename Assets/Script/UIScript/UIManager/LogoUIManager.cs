using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class LogoUIManager : MonoBehaviour
{
    //private GameObject root;
    private GameObject logoContainer, animationContainer, baseContainer;
    private GameObject logoG, logoS, logoT;
    private float fadein = 0.5f, fadeout = 0.5f;

    public SoundManager sm;
    public PanelSwitch ps;

    private bool loadfinish;

    void Awake()
    {
        //root = GameObject.Find("UI Root");
        baseContainer = this.transform.Find("Base_Container").gameObject;
        logoG = this.transform.Find("LogoG_Container").gameObject;
        logoT = this.transform.Find("LogoT_Container").gameObject;
        logoS = this.transform.Find("LogoS_Container").gameObject;
        //logoContainer = this.transform.Find("Logo_Container").gameObject;
        loadfinish = false;
    }

    void OnEnable()
    {
        //Debug.Log("Start!");
        //StartCoroutine(FadeInLogo());
        StartCoroutine(OpenAnimate());
    }


    private IEnumerator OpenAnimate()
    {
        //运行加载？
        StartCoroutine(LoadText());
        DataLoad();
        yield return new WaitForSeconds(0.5f);
        //淡入游戏制作组Logo
        yield return StartCoroutine(FadeInLogo(logoG));
        yield return new WaitForSeconds(1.5f);
        //淡出
        yield return StartCoroutine(FadeOutLogo(logoG));
        yield return new WaitForSeconds(0.5f);
        //淡入学校Logo
        yield return StartCoroutine(FadeInLogo(logoS));
        yield return new WaitForSeconds(1.5f);
        //淡出
        yield return StartCoroutine(FadeOutLogo(logoS));
        yield return new WaitForSeconds(0.5f);
        //淡入其他组Logo
        //yield return StartCoroutine(FadeInLogo(logoT));
        //yield return new WaitForSeconds(1f);
        //淡出
        //yield return StartCoroutine(FadeOutLogo(logoT));
        //yield return new WaitForSeconds(0.5f);
        StopAllCoroutines();
        //切换至标题画面
        ps.SwitchTo_VerifyIterative("Title_Panel");
    }

    private IEnumerator FadeInLogo(GameObject target)
    {
        target.SetActive(true);
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / fadein * Time.fixedDeltaTime);
            target.GetComponent<UIWidget>().alpha = t;
            yield return null;
        }
    }

    private IEnumerator FadeOutLogo(GameObject target)
    {
        float t = 1;
        while (t > 0)
        {
            t = Mathf.MoveTowards(t, 0, 1 / fadein * Time.fixedDeltaTime);
            target.GetComponent<UIWidget>().alpha = t;
            yield return null;
        }
        target.SetActive(false);
    }

    private void DataLoad()
    {
        string[] filenames = { "BREAK", "HEAT UP", "HOPE VS DESPAIR", "Title", "Map","FirstSeason" };
        Dictionary<string, AudioClip> auDic = new Dictionary<string, AudioClip>();

        foreach (string str in filenames)
        {
            AudioClip ac = Resources.Load<AudioClip>("Audio/" + str);
            auDic.Add(ac.name, ac);
        }
        //AudioClip[] acs = Resources.LoadAll<AudioClip>("Audio/");
        //AudioClip ac = Resources.Load<AudioClip>("Audio/HEAT UP");
        sm.SetAuDic(auDic);
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
