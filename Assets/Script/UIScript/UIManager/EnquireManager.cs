using UnityEngine;
using System.Collections;
using System;
using System.Text.RegularExpressions;

public class EnquireManager : MonoBehaviour, IPanelManager {

    private GameObject root;
    private GameManager gm;
    private GameObject hpmpContainer, evidenceContainer, eqObject;
    private UIPanel eqPanel;
    private UILabel currentLabel;
    private UIProgressBar hpBar, mpBar, timeBar;

    private float[] voiceTime;
    private string[] testimony;

    // Use this for initialization
    void Start () {
        root = GameObject.Find("UI Root");
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        eqObject = transform.parent.gameObject;
        eqPanel = eqObject.GetComponent<UIPanel>();

        hpmpContainer = transform.Find("HPMP_Container").gameObject;
        evidenceContainer = transform.Find("Scroll View").gameObject;

        currentLabel = transform.Find("CurrentText_Label").gameObject.GetComponent<UILabel>();
        hpBar = transform.Find("HPMP_Container/HP_Sprite").gameObject.GetComponent<UIProgressBar>();
        mpBar = transform.Find("HPMP_Container/MP_Sprite").gameObject.GetComponent<UIProgressBar>();
        timeBar = transform.Find("ProgressBack_Sprite").gameObject.GetComponent<UIProgressBar>();
        Open();
    }

    public IEnumerator Open()
    {
        LoadText();
        yield return StartCoroutine(MainEnquire());
    }
    public IEnumerator Close()
    {
        // TODO
        yield return null;
    }
    void LoadText()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Text/test-enquire");
        if (textAsset == null)
            throw new Exception("File not found!");
        string source = textAsset.text;
        testimony = source.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        voiceTime = new float[testimony.Length];
    }
    IEnumerator MainEnquire()
    {
        eqPanel.alpha = 0;
        //场景淡入淡出
        yield return StartCoroutine(FadeIn());
        //载入UI
        yield return StartCoroutine(UISet());
        //台词轮盘
        for (int i = 0; i < testimony.Length; i++)
        {
            currentLabel.text = testimony[i];
            voiceTime[i] = 10f;
            //时间条移动
            StartCoroutine(TimePass(i));
            //证词移动
            yield return StartCoroutine(Moving(voiceTime[i]));
        }
    }
    IEnumerator Moving(float time)
    {
        float start = -640 - currentLabel.localSize.x / 2;
        float final = 640 + currentLabel.localSize.x / 2;
        float x = start;
        float t;//变速运动
        float y = UnityEngine.Random.Range(-200, 250);
        while (x < final)
        {
            if (x < -540 + currentLabel.localSize.x / 2 || x > 540 - currentLabel.localSize.x / 2) t = 0.5f;
            else t = time;
            x = Mathf.MoveTowards(x, final, (final - start) / t * Time.deltaTime);
            currentLabel.transform.localPosition = new Vector3(x, y, 0);
            yield return null;
        }
        currentLabel.transform.localPosition = new Vector3(start, 150, 0);
    }
    IEnumerator TimePass(int i)
    {
        float value = i;
        while (value < i + 1)
        {
            value = Mathf.MoveTowards(value, i + 1, Time.deltaTime * 10);
            timeBar.value = value / testimony.Length;
            yield return null;
        }
    }
    IEnumerator UISet()
    {
        float x = 0;
        float hpx, eviy, timex;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, Time.deltaTime * 2);
            hpx = -800 + 300 * x;
            eviy = -440 + 150 * x;
            timex = 670 - 70 * x;
            hpmpContainer.transform.localPosition = new Vector3(hpx, 320, 0);
            evidenceContainer.transform.localPosition = new Vector3(0, eviy, 0);
            timeBar.transform.localPosition = new Vector3(timex, 80, 0);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator FadeIn()
    {
        eqObject.SetActive(true);
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, Time.deltaTime);
            eqPanel.alpha = x;
            yield return null;
        }
    }
}
