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
    private GameObject indexContainer, selfContainer, loveContainer, eviContainer, helpContainer;
    private Constants.NOTE_STATUS currentOpen;
    private UILabel introText;


    private void Awake()
    {
        indexContainer = transform.Find("Index_Container").gameObject;
        selfContainer = transform.Find("Info_Container").gameObject;
        loveContainer = transform.Find("Love_Container").gameObject;
        eviContainer = transform.Find("Evidence_Container").gameObject;
        helpContainer = transform.Find("App_Container").gameObject;
        introText = indexContainer.transform.Find("Help_Container/Help_Label").GetComponent<UILabel>();
    }

    private void OnEnable()
    {
        indexContainer.SetActive(true);
        currentOpen = Constants.NOTE_STATUS.INDEX;
        DataManager.GetInstance().BlockClick();
        DataManager.GetInstance().BlockBacklog();
    }

    private void OnDisable()
    {
        DataManager.GetInstance().UnblockClick();
        DataManager.GetInstance().UnblockBacklog();
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
        StartCoroutine(Fadein(0.2f));
    }

    private void CloseNote()
    {
        StartCoroutine(Fadeout(0.2f));
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

    public void OpenSelf()
    {
        //打开个人界面
        SwitchTo(Constants.NOTE_STATUS.SELF);
    }

    public void OpenLove()
    {
        //打开好感度u界面
        SwitchTo(Constants.NOTE_STATUS.LOVE);
    }

    public void OpenEvidence()
    {
        //打开证据列表
        SwitchTo(Constants.NOTE_STATUS.EVIDENCE);
    }

    public void OpenApp()
    {
        //打开帮助界面
        SwitchTo(Constants.NOTE_STATUS.APP);
    }

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
            case Constants.NOTE_STATUS.APP:
                helpContainer.SetActive(false);
                break;
        }
        //打开新界面
        switch (target)
        {
            case Constants.NOTE_STATUS.INDEX:
                indexContainer.SetActive(true);
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
            case Constants.NOTE_STATUS.APP:
                helpContainer.SetActive(true);
                break;
        }
        currentOpen = target;
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
