using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Assets.Script.GameStruct;

public class TitleManager : MonoBehaviour 
{

    public GameManager gm;
    public SystemManager sm;
    public UIWidget title, button, music, gallery, recollection, ending;
    public GameObject bg;
    public UILabel info, timelabel;
    public UIWidget large;
    public UI2DSprite largepic;
    public AudioSource bgm;
    public UISlider slider;
    public Constants.TITLE_STATUS status;

    private Dictionary<int, string> cgInfoTable, endingInfoTable;
    private List<bool> musicTable, cgTabel, endingTable, caseTable;

    void Awake()
    {
        //读入二周目数据表
        //musicTable = gVars();
        status = Constants.TITLE_STATUS.TITLE;
    }

    void Update()
    {
        int tnow = (int)bgm.time;
        int tall = (int)bgm.clip.length;
        TimeSpan nowts = new TimeSpan(0, 0, tnow);
        TimeSpan allts = new TimeSpan(0, 0, tall);
        timelabel.text = nowts.ToString() + "/" + allts.ToString();
    }

    #region 二周目数据初始化相关
    private void SetMusic()
    {
        //编辑器内 先设计好所有按钮 开启则显示 未开启则默认
        //存储【名称，是否开启】
        GameObject grid = transform.Find("Music_Container/NameButton_Grid").gameObject;
        for(int i = 0; i < musicTable.Count; i++)
        {
            UIButton btn = grid.transform.Find("Label" + i).gameObject.GetComponent<UIButton>();
            UILabel lb = grid.transform.Find("Label" + i).gameObject.GetComponent<UILabel>();
            btn.isEnabled = musicTable[i];
            if (musicTable[i])
            {
                lb.text = "??????";
            }
        }
    }

    private void SetGallery(int pagenum)
    {
        //编辑器内设计好位置 只显示一部分 内容根据下标要改变
        //默认未开启时 显示logo 且点击无效
        GameObject grid = transform.Find("Gallery_Container/Pic_Grid").gameObject;
        int first = (pagenum - 1) * 15;
        for(int i = 0; i < 15; i++)
        {
            UIButton btn = grid.transform.Find(i.ToString()).gameObject.GetComponent<UIButton>();
            if (cgTabel[i])
            {
                btn.normalSprite2D = (Sprite)Resources.Load("Logo");
            }
            else
            {
                btn.normalSprite2D = (Sprite)Resources.Load(cgInfoTable[first + i]);
            }
        }
    }

    private void SetRecollection()
    {
        //编辑器内 先设计好所有按钮位置 只需要开启即可
        GameObject grid = transform.Find("Recollection_Container/Case_Container").gameObject;
        for (int i = 0; i < caseTable.Count; i++)
        {
            grid.transform.Find("Case" + i + "_Button").gameObject.SetActive(caseTable[i]);
        }
    }

    private void SetEnding()
    {
        //编辑器内 先设计好所有的结局图标
        //需要设置图片 true的显示相应 false的为默认
        GameObject grid = transform.Find("Ending_Container/Achieve_Grid").gameObject;
        for (int i = 0; i < caseTable.Count; i++)
        {
            UIButton btn = grid.transform.Find("Achieve" + i).gameObject.GetComponent<UIButton>();
            if (caseTable[i])
            {
                btn.normalSprite2D = (Sprite)Resources.Load("AchieveIcon" + i);
            }
            else
            {
                btn.normalSprite2D = (Sprite)Resources.Load("star");
            }
        }
    }
    #endregion

    #region public Title相关按钮
    public void ClickStart()
    {
        //新游戏start
        if (Input.GetMouseButtonUp(1)) return;
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
        sm.transform.gameObject.SetActive(true);
        sm.GetComponent<UIPanel>().alpha = 1;
        sm.OpenLoad();
    }

    public void ClickSetting()
    {
        if (Input.GetMouseButtonUp(1)) return;
        sm.transform.gameObject.SetActive(true);
        sm.GetComponent<UIPanel>().alpha = 1;
        sm.OpenSetting();
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
        }
    }
    public void CloseMusic()
    {
        status = Constants.TITLE_STATUS.EXTRA;
        StartCoroutine(FadeOut(music));
        StartCoroutine(FadeIn(button));
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

    #region public Music操作按钮
    public void PlayMusicAt(string fileName)
    {
        bgm.clip = Resources.Load("Audio/" + fileName) as AudioClip;
        PlayMusic();
    }
    public void PlayMusic()
    {
        transform.Find("Music_Container/Control_Container/Play_Button").gameObject.SetActive(false);
        transform.Find("Music_Container/Control_Container/Pause_Button").gameObject.SetActive(true);
        bgm.Play();
    }

    public void PauseMusic()
    {
        transform.Find("Music_Container/Control_Container/Play_Button").gameObject.SetActive(true);
        transform.Find("Music_Container/Control_Container/Pause_Button").gameObject.SetActive(false);
        bgm.Pause();
    }

    public void SetMusicTime(float x)
    {
        bgm.time = x * bgm.clip.length;
    }

    public void PrevMusic()
    {
        //上一首
    }
    public void NextMusic()
    {
        //下一首
    }
    public void StopMusic()
    {
        transform.Find("Music_Container/Control_Container/Play_Button").gameObject.SetActive(true);
        transform.Find("Music_Container/Control_Container/Pause_Button").gameObject.SetActive(false);
        bgm.Stop();
    }
    #endregion

    #region public按钮Gallery操作
    public void OpenPicAt()
    {
        //查看图片
        StartCoroutine(FadeIn(large));
    }
    public void ClosePic()
    {
        //关闭图片
        StartCoroutine(FadeOut(large));
    }
    public void ChangeGroup(int num)
    {
        //按下数字键
        SetGallery(num);
    }
    #endregion

    #region public按钮Recollection操作
    public void ClickCase()
    {
        //按下case按钮
    }
    #endregion

    #region public按钮Ending操作
    public void ClickAchieveAt(string str)
    {
        //int x = System.Convert.ToInt32(str);
        info.text = "这是第" + str + "个成就！";
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
            x = Mathf.MoveTowards(x, 1, 1 / 0.5f * Time.deltaTime);
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
