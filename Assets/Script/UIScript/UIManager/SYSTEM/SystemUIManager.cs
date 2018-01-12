using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;

public class SystemUIManager : MonoBehaviour
{
    private SLUIManager slm;

    public PanelSwitch ps;
    public GameObject butContainer, saveloadContainer, settingContainer, backlogContainer, warningContainer;
    public DialogBoxUIManager dbum;
    public UILabel sysIndexInfo;

    private Constants.WarningMode currentMode;

    public bool fromAVG;

    private void Awake()
    {
        slm = saveloadContainer.GetComponent<SLUIManager>();
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
        }
    }

    private void OnEnable()
    {
        DataManager.GetInstance().BlockClick();
    }

    private void OnDisable()
    {
        DataManager.GetInstance().UnblockClick();
        DataManager.GetInstance().UnblockBacklog();
    }

    public void Open()
    {
        //1.预截图
        StartCoroutine(ScreenShot());
        //2.打开面板
        DataManager.GetInstance().BlockBacklog();
        StartCoroutine(FadeInP(0.2f));
        StartCoroutine(FadeIn(butContainer, 0.3f));
    }

    private IEnumerator ScreenShot()
    {
        //开始截图
        yield return new WaitForEndOfFrame();
        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        TextureScale.Bilinear(tex, 240, 135);
        byte[] imagebytes = tex.EncodeToPNG();
        DataManager.GetInstance().SetTempVar("缩略图", imagebytes);
        Destroy(tex);
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
    public void Close()
    {
        StartCoroutine(FadeOutP(0.3f));
        if (fromAVG) dbum.ShowWindow();
    }

    /// <summary>
    /// 【按钮】打开设置界面
    /// </summary>
    public void OpenSetting()
    {
        butContainer.SetActive(false);
        StartCoroutine(FadeIn(settingContainer, 0.3f));
    }

    /// <summary>
    /// 【按钮】打开文字记录
    /// </summary>
    public void OpenBacklog()
    {
        DataManager.GetInstance().UnblockBacklog();
        butContainer.SetActive(false);
        StartCoroutine(FadeIn(backlogContainer, 0.3f));
    }

    /// <summary>
    /// 【按钮】打开储存进度
    /// </summary>
    public void OpenSave()
    {
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
        if (Input.GetMouseButtonUp(1)) return;
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
        if (Input.GetMouseButtonUp(1)) return;
        switch (currentMode)
        {
            case Constants.WarningMode.Title:
                //StartCoroutine(FadeOut(warningContainer));
                StartCoroutine(FadeOutP());
                ps.SwitchTo_VerifyIterative("Title_Panel");
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
    private IEnumerator FadeOutP(float time = 0.5f)
    {
        UIPanel panel = transform.GetComponent<UIPanel>();
        float x = 1;
        while (x > 0)
        {
            x = Mathf.MoveTowards(x, 0, 1 / time * Time.deltaTime);
            panel.alpha = x;
            yield return null;
        }
        butContainer.SetActive(false);
        saveloadContainer.SetActive(false);
        settingContainer.SetActive(false);
        backlogContainer.SetActive(false);
        warningContainer.SetActive(false);
        transform.gameObject.SetActive(false);
    }

    private IEnumerator FadeIn(GameObject target, float time = 0.5f)
    {
        target.SetActive(true);
        UIWidget widget = target.GetComponent<UIWidget>();
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / time * Time.deltaTime);
            widget.alpha = x;
            yield return null;
        }
    }
    private IEnumerator FadeOut(GameObject target, float time = 0.5f)
    {
        UIWidget widget = target.GetComponent<UIWidget>();
        float x = 1;
        while (x > 0)
        {
            x = Mathf.MoveTowards(x, 0, 1 / time * Time.deltaTime);
            widget.alpha = x;
            yield return null;
        }
        target.SetActive(false);
        //if (final != null) final.SetActive(true);
    }

}
