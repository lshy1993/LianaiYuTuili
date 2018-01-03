using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;

public class SystemUIManager : MonoBehaviour
{
    private GameManager gm;
    public PanelSwitch ps;
    public GameObject butContainer, saveloadContainer, settingContainer, backlogContainer, warningContainer;
    public GameObject saveTable;
    public UILabel sysIndexInfo;

    private int saveID, groupnum = 1;
    private Dictionary<int, SavingInfo> savedic;
    private Dictionary<string, byte[]> savepic;

    public enum WarningMode { Title, Quit, Save, Load, Delete};
    private WarningMode currentMode;
    private bool SaveMode, fromTitle;

    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnEnable()
    {
        DataManager.GetInstance().blockClick = true;
    }

    private void OnDisable()
    {
        DataManager.GetInstance().blockClick = false;
        DataManager.GetInstance().blockBacklog = false;
    }

    public void Open()
    {
        //1.预截图
        StartCoroutine(ScreenShot());
        //2.打开面板
        DataManager.GetInstance().blockBacklog = true;
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
        DataPool.GetInstance().WriteTempVar("缩略图", imagebytes);
        Destroy(tex);
    }

    public void Close()
    {
        StartCoroutine(FadeOutP(0.3f));
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
        DataManager.GetInstance().blockBacklog = false;
        butContainer.SetActive(false);
        StartCoroutine(FadeIn(backlogContainer, 0.3f));
    }

    /// <summary>
    /// 【按钮】打开储存进度
    /// </summary>
    public void OpenSave()
    {
        butContainer.SetActive(false);
        SaveMode = true;
        FreshSaveTable();
        StartCoroutine(FadeIn(saveloadContainer, 0.3f));
    }

    /// <summary>
    /// 【按钮】打开读取进度
    /// </summary>
    public void OpenLoad(bool fromTitle = false)
    { 
        butContainer.SetActive(false);
        SaveMode = false;
        this.fromTitle = fromTitle;
        FreshSaveTable();
        StartCoroutine(FadeIn(saveloadContainer, 0.3f));
    }

    /// <summary>
    /// 【按钮】返回标题画面
    /// </summary>
    public void OpenTitle()
    {
        if (Input.GetMouseButtonUp(1)) return;
        OpenWarning(WarningMode.Title);
    }

    /// <summary>
    /// 【按钮】退出游戏
    /// </summary>
    public void OpenExit()
    {
        if (Input.GetMouseButtonUp(1)) return;
        OpenWarning(WarningMode.Quit);
    }

    /// <summary>
    /// 【档位按钮】按下存档/读档
    /// </summary>
    /// <param name="x">存档栏位置1-6</param>
    public void SelectSave(int x)
    {
        saveID = groupnum * 6 + x;
        if (SaveMode)
        {
            //存档模式
            if (savedic.ContainsKey(saveID))
            {
                OpenWarning(WarningMode.Save);
            }
            else
            {
                SaveData();
            }
        }
        else
        {
            //读取模式
            if (savedic.ContainsKey(saveID))
            {
                OpenWarning(WarningMode.Load);
            }
        }

    }

    /// <summary>
    /// 【档位按钮】按下删除
    /// </summary>
    /// <param name="x">存档栏位置1-6</param>
    public void SelectDelete(int x)
    {
        saveID = groupnum * 6 + x;
        if (savedic.ContainsKey(saveID))
        {
            OpenWarning(WarningMode.Delete);
        }
    }

    /// <summary>
    /// 【界面】打开警告框
    /// </summary>
    /// <param name="arg">警告框类型</param>
    private void OpenWarning(WarningMode arg)
    {
        if (Input.GetMouseButtonUp(1)) return;
        warningContainer.GetComponent<UIWidget>().alpha = 1;

        string showMessage = "";
        currentMode = arg;

        switch (arg)
        {
            case WarningMode.Title:
                showMessage = "返回标题画面吗";
                break;
            case WarningMode.Quit:
                showMessage = "退出游戏吗";
                break;
            case WarningMode.Save:
                showMessage = "覆盖这个存档吗";
                break;
            case WarningMode.Load:
                showMessage = "读取这个存档吗";
                break;
            case WarningMode.Delete:
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
            case WarningMode.Title:
                //StartCoroutine(FadeOut(warningContainer));
                StartCoroutine(FadeOutP());
                ps.SwitchTo_VerifyIterative("Title_Panel");
                break;
            case WarningMode.Quit:
                Application.Quit();
                break;
            case WarningMode.Save:
                SaveData();
                warningContainer.SetActive(false);
                break;
            case WarningMode.Load:
                DataManager.GetInstance().Load(saveID);
                //TODO:执行切换界面
                LoadSwtich();
                break;
            case WarningMode.Delete:
                DataManager.GetInstance().Delete(saveID);
                break;
            default:
                break;
        }
    }

    private void SaveData()
    {
        string mode = DataManager.GetInstance().gameData.MODE;
        if (mode == "Avg模式" || mode == "侦探模式")
        {
            gm.sm.SaveSoundInfo();
            gm.im.SaveImageInfo();
        }
        DataManager.GetInstance().Save(saveID);
        FreshSaveTable();
    }

    /// <summary>
    /// 【按钮】点击警告框的取消
    /// </summary>
    public void WarningCancel()
    {
        if (Input.GetMouseButtonUp(1)) return;
        warningContainer.SetActive(false);
    }

    /// <summary>
    /// 读档时的界面复原
    /// </summary>
    private void LoadSwtich()
    {
        gm.sm.StopBGM();
        StartCoroutine(FadeOutAndLoad());
    }

    /// <summary>
    /// 【按钮】变更存档栏组号 1-9
    /// </summary>
    public void SetFileNum()
    {
        if (!UIToggle.current.value) return;
        int x = UIToggle.current.GetComponent<ToggleNum>().id;
        groupnum = x - 1;
        FreshSaveTable();
    }

    /// <summary>
    /// 刷新存档表格
    /// </summary>
    private void FreshSaveTable()
    {
        saveloadContainer.transform.Find("SL_Label").GetComponent<UILabel>().text = SaveMode ? "存档" : "读档";
        savedic = DataManager.GetInstance().systemData.saveInfo;
        savepic = DataManager.GetInstance().GetTempVar<Dictionary<string, byte[]>>("存档缩略图");
        for (int i = 1; i <= 6; i++)
        {
            int saveid = groupnum * 6 + i;
            GameObject go = saveTable.transform.Find("Saving_Box" + i).gameObject;
            //检查 存档位 是否为空
            if (savedic.ContainsKey(saveid) && savedic[saveid] != null)
            {
                //若为非空 则开启按钮
                go.GetComponent<UIButton>().enabled = true;
                go.transform.Find("Info_Label").GetComponent<UILabel>().text = savedic[saveid].saveText;
                go.transform.Find("Mode_Label").GetComponent<UILabel>().text = savedic[saveid].gameMode;
                go.transform.Find("Time_Label").GetComponent<UILabel>().text = savedic[saveid].saveTime;
                Texture2D texture = new Texture2D(240, 135);
                if (savepic.ContainsKey(savedic[saveid].picPath))
                {
                    texture.LoadImage(savepic[savedic[saveid].picPath]);
                    Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                    go.GetComponent<UIButton>().normalSprite2D = sp;
                }
            }
            else
            {
                //若为空 仅存档可以开按钮
                go.GetComponent<UIButton>().enabled = SaveMode;
                go.transform.Find("Info_Label").GetComponent<UILabel>().text = "";
                go.transform.Find("Mode_Label").GetComponent<UILabel>().text = "";
                go.transform.Find("Time_Label").GetComponent<UILabel>().text = "";
                if (SaveMode)
                {
                    go.GetComponent<UIButton>().normalSprite2D = Resources.Load<Sprite>("Background/Title");
                }
                else
                {
                    go.transform.Find("Save_Sprite").GetComponent<UI2DSprite>().sprite2D = Resources.LoadAll<Sprite>("Background/Title")[0];
                }
                go.transform.Find("Saving_Button").gameObject.SetActive(false);
                go.transform.Find("Delete_Button").gameObject.SetActive(false);
            }
        }
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

    private IEnumerator FadeOutAndLoad(float time = 0.5f)
    {
        UIPanel panel = transform.GetComponent<UIPanel>();
        float x = 1;
        while (x > 0)
        {
            x = Mathf.MoveTowards(x, 0, 1 / time * Time.deltaTime);
            panel.alpha = x;
            yield return null;
        }
        saveloadContainer.SetActive(false);
        warningContainer.SetActive(false);
        //Node复原
        string modeName = DataManager.GetInstance().gameData.MODE;
        Debug.Log(modeName);
        switch (modeName)
        {
            case "大地图模式":
                gm.node = NodeFactory.GetInstance().GetMapNode();
                break;
            case "养成模式":
                gm.node = NodeFactory.GetInstance().GetEduNode();
                break;
            case "侦探模式":
                string currentDetect = DataManager.GetInstance().inturnData.currentDetectEvent;
                //界面复原
                gm.im.LoadImageInfo();
                gm.sm.LoadSoundInfo();
                gm.node = NodeFactory.GetInstance().GetDetectJudgeNode(currentDetect);
                break;
            case "Avg模式":
                string textName = DataManager.GetInstance().gameData.currentScript;
                //界面复原
                gm.im.LoadImageInfo();
                gm.sm.LoadSoundInfo();
                gm.node = NodeFactory.GetInstance().FindTextScriptNoneInit(textName);
                break;
        }
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
