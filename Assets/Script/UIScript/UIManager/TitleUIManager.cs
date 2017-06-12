using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Assets.Script.GameStruct;

public class TitleUIManager : MonoBehaviour 
{
    public GameManager gm;
    public SystemUIManager sysm;
    //public SoundManager sm;

    public UIWidget title, button, music, gallery, recollection, ending;
    public GameObject bg;

    public Constants.TITLE_STATUS status;

    void Awake()
    {
        status = Constants.TITLE_STATUS.TITLE;
    }

    private void OnEnable()
    {
        //初始化时播放BGM
        gm.sm.SetBGM("Title");
    }

    #region public Title相关按钮
    public void ClickStart()
    {
        //新游戏start
        if (Input.GetMouseButtonUp(1)) return;
        gm.sm.StopBGM();
        gm.NewGame();
    }

	public void ClickExtra()
    {
        //打开extra
        if (Input.GetMouseButtonUp(1)) return;
        status = Constants.TITLE_STATUS.EXTRA;
        StartCoroutine(OpenExtra());
    }

    public void RightClickReturn()
    {
        status = Constants.TITLE_STATUS.TITLE;
        StartCoroutine(CloseExtra());
    }

    public void ClickLoad()
    {
        //设计：title不动 sys淡入
        if (Input.GetMouseButtonUp(1)) return;
        sysm.transform.gameObject.SetActive(true);
        sysm.GetComponent<UIPanel>().alpha = 1;
        sysm.OpenLoad(true);
    }

    public void ClickSetting()
    {
        if (Input.GetMouseButtonUp(1)) return;
        sysm.transform.gameObject.SetActive(true);
        sysm.GetComponent<UIPanel>().alpha = 1;
        sysm.OpenSetting();
    }

    public void ClickExit()
    {
        if (Input.GetMouseButtonUp(1)) return;
        Application.Quit();
    }
    #endregion

    #region public Extra开关
    public void OpenMusic()
    {
        if (Input.GetMouseButtonUp(0))
        {
            status = Constants.TITLE_STATUS.MUSIC;
            StartCoroutine(FadeOut(button));
            StartCoroutine(FadeIn(music));
            gm.sm.StopBGM();
        }
    }
    public void CloseMusic()
    {
        status = Constants.TITLE_STATUS.EXTRA;
        StartCoroutine(FadeOut(music));
        StartCoroutine(FadeIn(button));
        gm.sm.SetBGM("Title");
    }
    public void OpenGallery()
    {
        if (Input.GetMouseButtonUp(0))
        {
            status = Constants.TITLE_STATUS.GALLERY;
            StartCoroutine(FadeOut(button));
            StartCoroutine(FadeIn(gallery));
        }
    }
    public void CloseGallery()
    {
        status = Constants.TITLE_STATUS.EXTRA;
        StartCoroutine(FadeOut(gallery));
        StartCoroutine(FadeIn(button));
    }
    public void OpenRecollection()
    {
        if (Input.GetMouseButtonUp(0))
        {
            status = Constants.TITLE_STATUS.RECOLL;
            StartCoroutine(FadeOut(button));
            StartCoroutine(FadeIn(recollection));
        }
    }
    public void CloseRecollection()
    {
        status = Constants.TITLE_STATUS.EXTRA;
        StartCoroutine(FadeOut(recollection));
        StartCoroutine(FadeIn(button));
    }
    public void OpenEnding()
    {
        if (Input.GetMouseButtonUp(0))
        {
            status = Constants.TITLE_STATUS.ENDING;
            StartCoroutine(FadeOut(button));
            StartCoroutine(FadeIn(ending));
        }
    }
    public void CloseEnding()
    {
        status = Constants.TITLE_STATUS.EXTRA;
        StartCoroutine(FadeOut(ending));
        StartCoroutine(FadeIn(button));
    }
    #endregion

    private IEnumerator OpenExtra()
    {
        StartCoroutine(MoveBG(false));
        yield return StartCoroutine(FadeOut(title));
        StartCoroutine(FadeIn(button));
    }
    private IEnumerator CloseExtra()
    {
        StartCoroutine(MoveBG(true));
        yield return StartCoroutine(FadeOut(button));
        StartCoroutine(FadeIn(title));
    }

    private IEnumerator FadeIn(UIWidget target)
    {
        target.transform.gameObject.SetActive(true);
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / 0.3f * Time.deltaTime);
            target.alpha = x;
            yield return null;
        }
    }
    private IEnumerator FadeOut(UIWidget target)
    {
        float x = 1;
        while (x > 0)
        {
            x = Mathf.MoveTowards(x, 0, 1 / 0.3f * Time.deltaTime);
            target.alpha = x;
            yield return null;
        }
        target.transform.gameObject.SetActive(false);
    }

    private IEnumerator MoveBG(bool isback)
    {
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / 0.5f * Time.deltaTime);
            float y = isback ? 420 - 700 * (1 - x) : 420 - 700 * x;
            bg.transform.localPosition = new Vector3(0, y, 0);
            yield return null;
        }
        title.transform.gameObject.SetActive(isback);
    }
}
