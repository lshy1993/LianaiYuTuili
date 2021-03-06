﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;

/// <summary>
/// 系统界面管理器
/// 0.菜单
/// 1.SL
/// 2.设置
/// 3.文字记录
/// 4.警告窗口
/// </summary>
public class SystemUIManager : MonoBehaviour
{
    private SaveLoadUIManager slm;

    public PanelSwitch ps;
    public GameObject butContainer, saveloadContainer, settingContainer, backlogContainer;
    public GameObject warningContainer;
    public UIButton saveBtn, loadBtn;

    public DialogBoxUIManager dbum;
    public UILabel sysIndexInfo;

    public string currentContainer;
    private Constants.WarningMode currentMode;

    /// <summary>
    /// 是否关闭了Dialog
    /// </summary>
    public bool quickOpen;

    /// <summary>
    /// 是否从菜单按钮进入
    /// </summary>
    public bool fromButton;

    private void Awake()
    {
        slm = saveloadContainer.GetComponent<SaveLoadUIManager>();
        //生成存档界面表格
        slm.saveTable.transform.DestroyChildren();
        for (int i = 1; i <= 6; i++)
        {
            GameObject go = Resources.Load("Prefab/Saving_Box") as GameObject;
            go = NGUITools.AddChild(slm.saveTable, go);
            go.name = "Saving_Box" + i;
            go.transform.Find("Save_Sprite").GetComponent<SaveLoadButton>().id = i;
            go.transform.Find("Save_Sprite").GetComponent<SaveLoadButton>().uiManager = slm;
            go.transform.Find("Delete_Button").GetComponent<SaveDeleteButton>().id = i;
            go.transform.Find("Delete_Button").GetComponent<SaveDeleteButton>().uiManager = slm;
            int num = i;
            EventDelegate.Add(go.transform.Find("Info_Label").GetComponent<UIInput>().onChange, delegate () { slm.ChangeSaveInfo(num); });
        }
    }

    private void OnEnable()
    {
        DataManager.GetInstance().BlockClick();
        bool slBlock = DataManager.GetInstance().IsSaveLoadBlocked();
        saveBtn.enabled = !slBlock;
        loadBtn.enabled = !slBlock;
    }

    private void OnDisable()
    {
        DataManager.GetInstance().UnblockClick();
        //DataManager.GetInstance().UnblockBacklog();
    }

    public void Open()
    {
        //1.打开面板
        DataManager.GetInstance().BlockBacklog();
        StartCoroutine(FadeInP(0.2f));
        StartCoroutine(FadeIn(butContainer, 0.3f));
    }

    public void SetHelpInfo(bool ishover, string str)
    {
        sysIndexInfo.text = ishover ? str : "";
    }

    /// <summary>
    /// 打开Note
    /// </summary>
    public void OpenNote()
    {
        Close();
    }

    /// <summary>
    /// 关闭总界面
    /// </summary>
    public void Close()
    {
        StartCoroutine(FadeOutP(() => { }, 0.3f));
        //如果跨级开启 则恢复原界面
        if (quickOpen) dbum.ShowWindow();
        quickOpen = false;
    }

    /// <summary>
    /// 【按钮】打开设置界面
    /// </summary>
    public void OpenSetting()
    {
        Debug.Log("Open Setting");
        //淡出菜单
        if (!fromButton)
        {
            butContainer.SetActive(false);
            StartCoroutine(FadeIn(settingContainer));
        }
        else
        {
            //淡入设置
            StartCoroutine(FadeOutWithCallback(butContainer, () => { StartCoroutine(FadeIn(settingContainer, 0.3f)); }));
        }
        fromButton = false;
    }

    /// <summary>
    /// 【按钮】打开文字记录
    /// </summary>
    public void OpenBacklog()
    {
        Debug.Log("Open Backlog");
        DataManager.GetInstance().UnblockBacklog();
        if (!fromButton)
        {
            butContainer.SetActive(false);
            StartCoroutine(FadeIn(backlogContainer));
        }else
        {
            StartCoroutine(FadeOutWithCallback(butContainer, () => { StartCoroutine(FadeIn(backlogContainer)); }));
        }
        fromButton = false;
    }

    /// <summary>
    /// 【按钮】打开储存进度
    /// </summary>
    public void OpenSave()
    {
        Debug.Log("Open Save");
        butContainer.SetActive(false);
        slm.SetSaveMode();
        StartCoroutine(FadeIn(saveloadContainer, 0.3f));
    }

    /// <summary>
    /// 【按钮】打开读取进度
    /// </summary>
    public void OpenLoad(bool fromTitle = false)
    {
        if (fromTitle) butContainer.SetActive(false);
        else StartCoroutine(FadeOut(butContainer, 0.3f));
        slm.SetLoadMode(fromTitle);
        StartCoroutine(FadeIn(saveloadContainer, 0.3f));
    }

    /// <summary>
    /// 【按钮】返回标题画面
    /// </summary>
    public void OpenTitle()
    {
        if (Input.GetMouseButtonUp(1)) return;
        OpenWarning(Constants.WarningMode.Title);
    }

    /// <summary>
    /// 【按钮】退出游戏
    /// </summary>
    public void OpenExit()
    {
        if (Input.GetMouseButtonUp(1)) return;
        OpenWarning(Constants.WarningMode.Quit);
    }

    /// <summary>
    /// 【界面】打开警告框
    /// </summary>
    /// <param name="arg">警告框类型</param>
    public void OpenWarning(Constants.WarningMode arg)
    {
        //解除警告按钮锁定
        //warnBlockCon.SetActive(false);
        warningContainer.transform.Find("Block_Container").gameObject.SetActive(false);
        warningContainer.GetComponent<UIWidget>().alpha = 1;

        string showMessage = "";
        currentMode = arg;
        switch (arg)
        {
            case Constants.WarningMode.Title:
                showMessage = "返回标题画面吗";
                break;
            case Constants.WarningMode.Quit:
                showMessage = "退出游戏吗";
                break;
            case Constants.WarningMode.Save:
                showMessage = "覆盖这个存档吗";
                break;
            case Constants.WarningMode.Load:
                showMessage = "读取这个存档吗";
                break;
            case Constants.WarningMode.Delete:
                showMessage = "删除这个存档吗";
                break;
            default:
                break;
        }
        warningContainer.transform.Find("Warning_Label").GetComponent<UILabel>().text = showMessage;
        warningContainer.SetActive(true);
    }

    /// <summary>
    /// 【按钮】点击警告框的确认
    /// </summary>
    public void WarningComfirm()
    {
        //锁定警告框
        warningContainer.transform.Find("Block_Container").gameObject.SetActive(true);
        switch (currentMode)
        {
            case Constants.WarningMode.Title:
                //StartCoroutine(FadeOut(warningContainer));
                warningContainer.SetActive(false);
                StartCoroutine(FadeOutP(() => { ps.SwitchTo_VerifyIterative("Title_Panel"); }));

                break;
            case Constants.WarningMode.Quit:
                Application.Quit();
                break;
            case Constants.WarningMode.Save:
                slm.SaveData();
                warningContainer.SetActive(false);
                break;
            case Constants.WarningMode.Load:
                slm.LoadData();
                break;
            case Constants.WarningMode.Delete:
                slm.DeleteData();
                warningContainer.SetActive(false);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 【按钮】点击警告框的取消
    /// </summary>
    public void WarningCancel()
    {
        if (Input.GetMouseButtonUp(1)) return;
        warningContainer.SetActive(false);
    }

    private IEnumerator FadeInP(float time = 0.5f)
    {
        UIPanel panel = transform.GetComponent<UIPanel>();
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / time * Time.deltaTime);
            panel.alpha = x;
            yield return null;
        }
    }

    //淡出整个system panel
    private IEnumerator FadeOutP(Action callback, float time = 0.5f)
    {
        Debug.Log("fadeoutp");
        UIPanel panel = transform.GetComponent<UIPanel>();
        float x = 1;
        while (x > 0)
        {
            x = Mathf.MoveTowards(x, 0, 1 / time * Time.deltaTime);
            panel.alpha = x;
            yield return null;
        }

        settingContainer.SetActive(false);
        saveloadContainer.SetActive(false);
        backlogContainer.SetActive(false);
        butContainer.SetActive(false);
        warningContainer.SetActive(false);
        transform.gameObject.SetActive(false);
        callback();
    }

    private IEnumerator FadeIn(GameObject target, float time = 0.3f)
    {
        target.SetActive(true);
        currentContainer = target.name;
        if (target.transform.Find("Block_Container") != null) target.transform.Find("Block_Container").gameObject.SetActive(true);
        UIWidget widget = target.GetComponent<UIWidget>();
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / time * Time.deltaTime);
            widget.alpha = x;
            yield return null;
        }
        if (target.transform.Find("Block_Container") != null) target.transform.Find("Block_Container").gameObject.SetActive(false);
    }

    private IEnumerator FadeOut(GameObject target, float time = 0.3f)
    {
        if (target.transform.Find("Block_Container") != null) target.transform.Find("Block_Container").gameObject.SetActive(true);
        UIWidget widget = target.GetComponent<UIWidget>();
        float x = 1;
        while (x > 0)
        {
            x = Mathf.MoveTowards(x, 0, 1 / time * Time.deltaTime);
            widget.alpha = x;
            yield return null;
        }
        currentContainer = string.Empty;
        target.SetActive(false);
        //if (final != null) final.SetActive(true);
    }

    private IEnumerator FadeOutWithCallback(GameObject target, Action callback, float time = 0.3f)
    {
        if (target.transform.Find("Block_Container") != null) target.transform.Find("Block_Container").gameObject.SetActive(true);
        float x = 1;
        while (x > 0)
        {
            x = Mathf.MoveTowards(x, 0, 1 / time * Time.deltaTime);
            target.GetComponent<UIWidget>().alpha = x;
            yield return null;
        }
        currentContainer = string.Empty;
        target.SetActive(false);
        callback();
    }

}
