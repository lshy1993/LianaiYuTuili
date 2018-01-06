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

    public UIWidget title, extra, music, gallery, recollection, ending;
    public GameObject btnTable, largeCon;
    public GameObject bg;

    private Constants.TITLE_STATUS status;

    void Awake()
    {
        status = Constants.TITLE_STATUS.TITLE;
    }

    private void OnEnable()
    {
        //初始化时播放BGM
        gm.sm.SetBGM("Title");
    }

    public Constants.TITLE_STATUS GetStatus()
    {
        return status;
    }

    public void RightClick()
    {
        switch (status)
        {
            case Constants.TITLE_STATUS.EXTRA:
                RightClickReturn();
                break;
            case Constants.TITLE_STATUS.GALLERY:
                if (largeCon.activeSelf)
                {
                    gallery.GetComponent<GalleryUIManager>().ClosePic();
                }
                else
                {
                    CloseGallery();
                }
                break;
            case Constants.TITLE_STATUS.MUSIC:
                CloseMusic();
                break;
            case Constants.TITLE_STATUS.RECOLL:
                CloseRecollection();
                break;
            case Constants.TITLE_STATUS.ENDING:
                CloseEnding();
                break;
            default:
                break;
        }
    }

    #region public Title相关按钮
    public void ClickStart()
    {
        //新游戏start
        gm.sm.StopBGM();
        gm.NewGame();
    }

	public void ClickExtra()
    {
        //打开extra
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
        sysm.transform.gameObject.SetActive(true);
        sysm.GetComponent<UIPanel>().alpha = 1;
        sysm.OpenLoad(true);
    }

    public void ClickSetting()
    {
        sysm.transform.gameObject.SetActive(true);
        sysm.GetComponent<UIPanel>().alpha = 1;
        sysm.OpenSetting();
    }

    public void ClickExit()
    {
        Application.Quit();
    }
    #endregion

    #region public Extra开关
    public void OpenMusic()
    {
        if (Input.GetMouseButtonUp(0))
        {
            status = Constants.TITLE_STATUS.MUSIC;
            StartCoroutine(FadeOut(extra));
            StartCoroutine(FadeIn(music));
            gm.sm.StopBGM();
        }
    }
    public void CloseMusic()
    {
        status = Constants.TITLE_STATUS.EXTRA;
        StartCoroutine(FadeOut(music));
        StartCoroutine(FadeIn(extra));
        gm.sm.SetBGM("Title");
    }
    public void OpenGallery()
    {
        if (Input.GetMouseButtonUp(0))
        {
            status = Constants.TITLE_STATUS.GALLERY;
            StartCoroutine(FadeOut(extra));
            StartCoroutine(FadeIn(gallery));
        }
    }
    public void CloseGallery()
    {
        status = Constants.TITLE_STATUS.EXTRA;
        StartCoroutine(FadeOut(gallery));
        StartCoroutine(FadeIn(extra));
    }
    public void OpenRecollection()
    {
        if (Input.GetMouseButtonUp(0))
        {
            status = Constants.TITLE_STATUS.RECOLL;
            StartCoroutine(FadeOut(extra));
            StartCoroutine(FadeIn(recollection));
        }
    }
    public void CloseRecollection()
    {
        status = Constants.TITLE_STATUS.EXTRA;
        StartCoroutine(FadeOut(recollection));
        StartCoroutine(FadeIn(extra));
    }
    public void OpenEnding()
    {
        if (Input.GetMouseButtonUp(0))
        {
            status = Constants.TITLE_STATUS.ENDING;
            StartCoroutine(FadeOut(extra));
            StartCoroutine(FadeIn(ending));
        }
    }
    public void CloseEnding()
    {
        status = Constants.TITLE_STATUS.EXTRA;
        StartCoroutine(FadeOut(ending));
        StartCoroutine(FadeIn(extra));
    }
    #endregion

    private IEnumerator OpenExtra()
    {
        StartCoroutine(MoveBG(false));
        yield return StartCoroutine(FadeOut(title));
        StartCoroutine(FadeIn(extra));
    }
    private IEnumerator CloseExtra()
    {
        StartCoroutine(MoveBG(true));
        yield return StartCoroutine(FadeOut(extra));
        StartCoroutine(FadeIn(title));
    }

    private IEnumerator FadeIn(UIWidget target)
    {
        DataManager.GetInstance().blockRightClick = true;
        if (target == title) BlockBtn(false);
        target.transform.gameObject.SetActive(true);
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / 0.3f * Time.deltaTime);
            target.alpha = x;
            yield return null;
        }
        if (target == title) BlockBtn(true);
        DataManager.GetInstance().blockRightClick = false;
    }
    private IEnumerator FadeOut(UIWidget target)
    {
        if (target == title) BlockBtn(false);
        float x = 1;
        while (x > 0)
        {
            x = Mathf.MoveTowards(x, 0, 1 / 0.3f * Time.deltaTime);
            target.alpha = x;
            yield return null;
        }
        target.transform.gameObject.SetActive(false);
        if (target == title) BlockBtn(true);
    }

    private IEnumerator MoveBG(bool isback)
    {
        DataManager.GetInstance().blockRightClick = true;
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / 0.5f * Time.deltaTime);
            float y = isback ? 420 - 700 * (1 - x) : 420 - 700 * x;
            bg.transform.localPosition = new Vector3(0, y, 0);
            yield return null;
        }
        title.transform.gameObject.SetActive(isback);
        DataManager.GetInstance().blockRightClick = false;
    }

    private void BlockBtn(bool blocked)
    {
        for (int i = 0; i < btnTable.transform.childCount; i++)
        {
            btnTable.transform.GetChild(i).GetComponent<UIButton>().enabled = blocked;
        }
    }
}
