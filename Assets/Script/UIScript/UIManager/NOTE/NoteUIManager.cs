using UnityEngine;
using System.Collections;
using System;
using Assets.Script.GameStruct.Model;
using System.Collections.Generic;
using Assets.Script.GameStruct;

/**
 * PhoneUIManager: 
 * 整个游戏只允许一个，作为PhonePanel的组件，不能被删除
 * 控制PhonePanel下的各物件并与之交互
 * 负责将数据转化为UI形式
 */
public class NoteUIManager : MonoBehaviour
{
    /// <summary>
    /// 各子窗口widget
    /// </summary>
    private GameObject indexContainer;
    private GameObject selfContainer;
    private GameObject loveContainer;
    private GameObject eviContainer;
    private GameObject calContainer;
    private GameObject tourContainer;
    private GameObject wikiContainer;

    private GameObject middleCon;
    private GameObject momentCon;
    private GameObject mailCon;

    /* 暂时弃用
    private GameObject appContainer;
    */

    private Constants.NOTE_STATUS currentOpen;
    public UILabel pcTime, gameDay, seedText, introText;


    private void Awake()
    {
        indexContainer = transform.Find("Index_Container").gameObject;
        selfContainer = transform.Find("Info_Container").gameObject;
        loveContainer = transform.Find("Love_Container").gameObject;
        eviContainer = transform.Find("Evidence_Container").gameObject;
        calContainer = transform.Find("Calendar_Container").gameObject;
        tourContainer = transform.Find("Tour_Container").gameObject;
        wikiContainer = transform.Find("Wiki_Container").gameObject;

        middleCon = indexContainer.transform.Find("Container/Middle_Container").gameObject;
        momentCon = indexContainer.transform.Find("Container/Moment_Container").gameObject;
        mailCon = indexContainer.transform.Find("Container/Mail_Container").gameObject;

    }

    private void OnEnable()
    {
        DataManager.GetInstance().BlockClick();
        DataManager.GetInstance().BlockBacklog();
        NoteInit();
    }

    private void OnDisable()
    {
        DataManager.GetInstance().UnblockClick();
        DataManager.GetInstance().UnblockBacklog();
    }

    /// <summary>
    /// 界面初始化
    /// </summary>
    private void NoteInit()
    {
        indexContainer.SetActive(true);
        //默认开启界面
        currentOpen = Constants.NOTE_STATUS.INDEX;
        //设置系统时间
        int hour = DateTime.Now.Hour;
        int minute = DateTime.Now.Minute;
        pcTime.text = DateTime.Now.ToString("HH:mm");
        //游戏时间
        DateTime gday = DataManager.GetInstance().GetToday();
        gameDay.text = gday.ToString("MM月dd日");
        //系统电量
        //系统网络
        //设置是否有new
        SetNewIcon();
    }

    private void SetNewIcon()
    {
        //TODO:如果资料有更新 则显示NEW图标
        //存在当前UIManager下 例如词条数等等
        //查询DataManager 对比是否有区别
        //有则显示New
    }

    public void OpenNote()
    {
        //StartCoroutine(Fadein(0.2f));
        StartCoroutine(RotateIn(0.2f));
    }

    private void CloseNote()
    {
        //StartCoroutine(Fadeout(0.2f));
        StartCoroutine(RotateOut(0.2f));
    }

    public void ReturnIndex()
    {
        if(currentOpen == Constants.NOTE_STATUS.INDEX)
        {
            CloseNote();
        }
        else
        {
            SwitchTo(Constants.NOTE_STATUS.INDEX);
        }
    }

    #region 子界面开启函数
    /// <summary>
    /// 打开个人信息界面
    /// </summary>
    public void OpenSelf()
    {
        SwitchTo(Constants.NOTE_STATUS.SELF);
    }

    /// <summary>
    /// 打开好感度u界面
    /// </summary>
    public void OpenLove()
    {
        SwitchTo(Constants.NOTE_STATUS.LOVE);
    }

    /// <summary>
    /// 打开证据列表
    /// </summary>
    public void OpenEvidence()
    {
        SwitchTo(Constants.NOTE_STATUS.EVIDENCE);
    }

    /// <summary>
    /// 打开日历
    /// </summary>
    public void OpenCalendar()
    {
        SwitchTo(Constants.NOTE_STATUS.CALENDAR);
    }

    /// <summary>
    /// 打开旅游资讯
    /// </summary>
    public void OpenTour()
    {
        SwitchTo(Constants.NOTE_STATUS.TOUR);
    }

    /// <summary>
    /// 打开帮助
    /// </summary>
    public void OpenWiki()
    {
        SwitchTo(Constants.NOTE_STATUS.WIKI);
    }

    /// <summary>
    /// 打开校内邮件
    /// </summary>
    public void OpenMail()
    {
        middleCon.SetActive(false);
        mailCon.SetActive(true);
        momentCon.SetActive(false);
    }

    /// <summary>
    /// 打开朋友圈
    /// </summary>
    public void OpenMoment()
    {
        middleCon.SetActive(false);
        mailCon.SetActive(false);
        momentCon.SetActive(true);
    }

    /// <summary>
    /// 查看应用
    /// </summary>
    public void OpenIndex()
    {
        middleCon.SetActive(true);
        mailCon.SetActive(false);
        momentCon.SetActive(false);
    }

    /* 暂时弃用APP界面
    public void OpenApp()
    {
        SwitchTo(Constants.NOTE_STATUS.APP);
    }
    */
    #endregion

    public void SetHelpInfo(bool ishover, string str)
    {
        introText.text = ishover ? str : "";
    }

    private void SwitchTo(Constants.NOTE_STATUS target)
    {
        //关闭旧界面
        switch (currentOpen)
        {
            case Constants.NOTE_STATUS.INDEX:
                indexContainer.SetActive(false);
                break;
            case Constants.NOTE_STATUS.SELF:
                selfContainer.SetActive(false);
                break;
            case Constants.NOTE_STATUS.LOVE:
                loveContainer.SetActive(false);
                break;
            case Constants.NOTE_STATUS.EVIDENCE:
                eviContainer.SetActive(false);
                break;
            case Constants.NOTE_STATUS.CALENDAR:
                calContainer.SetActive(false);
                break;
            case Constants.NOTE_STATUS.TOUR:
                tourContainer.SetActive(false);
                break;
            case Constants.NOTE_STATUS.WIKI:
                wikiContainer.SetActive(false);
                break;
            //case Constants.NOTE_STATUS.APP:
            //    appContainer.SetActive(false);
            //    break;
        }
        //打开新界面
        switch (target)
        {
            case Constants.NOTE_STATUS.INDEX:
                indexContainer.SetActive(true);
                middleCon.SetActive(true);
                break;
            case Constants.NOTE_STATUS.SELF:
                selfContainer.SetActive(true);
                break;
            case Constants.NOTE_STATUS.LOVE:
                loveContainer.SetActive(true);
                break;
            case Constants.NOTE_STATUS.EVIDENCE:
                eviContainer.SetActive(true);
                break;
            case Constants.NOTE_STATUS.CALENDAR:
                calContainer.SetActive(true);
                break;
            case Constants.NOTE_STATUS.TOUR:
                tourContainer.SetActive(true);
                break;
            case Constants.NOTE_STATUS.WIKI:
                wikiContainer.SetActive(true);
                break;
            //case Constants.NOTE_STATUS.APP:
            //    appContainer.SetActive(true);
            //    break;
        }
        currentOpen = target;
    }

    private IEnumerator RotateIn(float time)
    {
        yield return new WaitForSeconds(0.2f);
        transform.gameObject.SetActive(true);
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / time * Time.deltaTime);
            indexContainer.transform.localRotation = Quaternion.Euler(0, 0, -90 * (1 - t));
            yield return null;
        }
    }

    private IEnumerator RotateOut(float time)
    {
        transform.gameObject.SetActive(true);
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / time * Time.deltaTime);
            indexContainer.transform.localRotation = Quaternion.Euler(0, 0, -90 * t);
            yield return null;
        }
        transform.gameObject.SetActive(false);
    }

    private IEnumerator Fadein(float time)
    {
        UIPanel panel = transform.GetComponent<UIPanel>();
        float fmove = time == 0 ? 1 : 0;
        panel.alpha = fmove;
        transform.gameObject.SetActive(true);
        while (fmove < 1f)
        {
            fmove = Mathf.MoveTowards(fmove, 1f, Time.deltaTime / time);
            panel.alpha = fmove;
            yield return null;
        }
    }

    private IEnumerator Fadeout(float time)
    {
        UIPanel panel = transform.GetComponent<UIPanel>();
        float fmove = time == 0 ? 0 : 1;
        panel.alpha = fmove;
        while (fmove > 0)
        {
            fmove = Mathf.MoveTowards(fmove, 0, Time.deltaTime / time);
            panel.alpha = fmove;
            yield return null;
        }
        transform.gameObject.SetActive(false);
    }

    #region 废弃代码
    //public void MoveGrid(string tabname)
    //{
    //    if (tabname == "Card_Button")
    //    {
    //        SetCardInfo();
    //        StartCoroutine(StartMove(0));
    //    }
    //    if (tabname == "Friend_Button")
    //    {
    //        SetGirlInfo("");
    //        StartCoroutine(StartMove(700));
    //    }
    //    if (tabname == "Case_Button")
    //    {
    //        StartCoroutine(StartMove(1400));
    //    }
    //    if(tabname == "App_Button")
    //    {
    //        StartCoroutine(StartMove(2100));
    //    }
    //}

    //IEnumerator StartMove(float final)
    //{
    //    float start = grid.transform.localPosition.y;
    //    float y = start;
    //    while (y != final)
    //    {
    //        y = Mathf.MoveTowards(y, final, Mathf.Abs(final - start) / 0.2f * Time.deltaTime);
    //        grid.transform.localPosition = new Vector3(0, y, 0);
    //        yield return null;
    //    }
    //}
    #endregion

}
