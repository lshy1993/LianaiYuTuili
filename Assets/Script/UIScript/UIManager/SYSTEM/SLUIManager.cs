using UnityEngine;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;
using System.Collections;
using System.Collections.Generic;

public class SLUIManager : MonoBehaviour
{
    private GameManager gm;

    public SystemUIManager suim;
    public GameObject saveTable;

    private int saveID, groupnum = 1;
    private Dictionary<int, SavingInfo> savedic;
    private Dictionary<string, byte[]> savepic;

    private bool saveMode;
    private bool fromTitle;

    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnEnable()
    {
        DataManager.GetInstance().BlockClick();
    }

    private void OnDisable()
    {
        DataManager.GetInstance().UnblockClick();
        //DataManager.GetInstance().UnblockBacklog();
    }

    public void SetSaveMode()
    {
        saveMode = true;
        FreshSaveTable();
    }

    public void SetLoadMode(bool ori)
    {
        fromTitle = ori;
        saveMode = false;
        FreshSaveTable();
    }


    public void SaveData()
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

    public void LoadData()
    {
        DataManager.GetInstance().Load(saveID);
        //TODO:执行切换界面
        gm.sm.StopBGM();
        StartCoroutine(FadeOutAndLoad());
    }

    public void DeleteData()
    {
        DataManager.GetInstance().Delete(saveID);
        FreshSaveTable();
    }

    /// <summary>
    /// 【档位按钮】按下存档/读档
    /// </summary>
    /// <param name="x">存档栏位置1-6</param>
    public void SelectSave(int x)
    {
        saveID = groupnum * 6 + x;
        if (saveMode)
        {
            //存档模式
            if (savedic.ContainsKey(saveID))
            {
                suim.OpenWarning(Constants.WarningMode.Save);
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
                suim.OpenWarning(Constants.WarningMode.Load);
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
            suim.OpenWarning(Constants.WarningMode.Delete);
        }
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
        transform.Find("SL_Label").GetComponent<UILabel>().text = saveMode ? "存档" : "读档";
        savedic = DataManager.GetInstance().tempData.saveInfo;
        savepic = DataManager.GetInstance().GetTempVar<Dictionary<string, byte[]>>("存档缩略图");
        for (int i = 1; i <= 6; i++)
        {
            int saveid = groupnum * 6 + i;
            GameObject go = saveTable.transform.Find("Saving_Box" + i).gameObject;
            //检查 存档位 是否为空
            if (savedic.ContainsKey(saveid) && savedic[saveid] != null)
            {
                //若为非空 则开启按钮
                go.transform.Find("Save_Sprite").GetComponent<UIButton>().enabled = true;
                go.transform.Find("NO_Label").GetComponent<UILabel>().text = "NO." + saveid;

                go.transform.Find("Info_Label").gameObject.SetActive(true);
                go.transform.Find("Info_Label").GetComponent<UILabel>().text = savedic[saveid].saveText;
                go.transform.Find("Info_Label").GetComponent<UIInput>().defaultText = savedic[saveid].saveText;

                go.transform.Find("Text_Label").GetComponent<UILabel>().text = savedic[saveid].currentText;
                go.transform.Find("Mode_Label").GetComponent<UILabel>().text = savedic[saveid].gameMode;
                go.transform.Find("Time_Label").GetComponent<UILabel>().text = savedic[saveid].saveTime;
                Texture2D texture = new Texture2D(240, 135);
                if (savepic.ContainsKey(savedic[saveid].picPath))
                {
                    texture.LoadImage(savepic[savedic[saveid].picPath]);
                    Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                    go.transform.Find("Save_Sprite").GetComponent<UIButton>().normalSprite2D = sp;
                }
                go.transform.Find("Copy_Button").GetComponent<UIButton>().enabled = true;
                go.transform.Find("Delete_Button").GetComponent<UIButton>().enabled = true;
            }
            else
            {
                //若为空 仅存档可以开按钮
                go.transform.Find("Save_Sprite").GetComponent<UIButton>().enabled = saveMode;
                go.transform.Find("NO_Label").GetComponent<UILabel>().text = "NO." + saveid;
                go.transform.Find("Text_Label").GetComponent<UILabel>().text = "";
                go.transform.Find("Info_Label").gameObject.SetActive(false);
                go.transform.Find("Mode_Label").GetComponent<UILabel>().text = "";
                go.transform.Find("Time_Label").GetComponent<UILabel>().text = "";
                if (saveMode)
                {
                    go.transform.Find("Save_Sprite").GetComponent<UIButton>().normalSprite2D = Resources.Load<Sprite>("Background/Title");
                }
                else
                {
                    go.transform.Find("Save_Sprite").GetComponent<UI2DSprite>().sprite2D = Resources.LoadAll<Sprite>("Background/Title")[0];
                }
                go.transform.Find("Copy_Button").GetComponent<UIButton>().enabled = false;
                go.transform.Find("Delete_Button").GetComponent<UIButton>().enabled = false;
            }
        }
    }

    /// <summary>
    /// 修改存档标签
    /// </summary>
    /// <param name="i">存档栏位置1-6</param>
    public void ChangeSaveInfo(int i)
    {
        Debug.Log(i);
        int saveid = groupnum * 6 + i;
        GameObject go = saveTable.transform.Find("Saving_Box" + i).gameObject;
        string str = go.transform.Find("Info_Label").GetComponent<UILabel>().text;
        savedic[saveid].saveText = str;
        DataManager.GetInstance().ChangeSave();
        FreshSaveTable();
    }


    private IEnumerator FadeOutAndLoad(float time = 0.5f)
    {
        UIPanel panel = suim.transform.GetComponent<UIPanel>();
        float x = 1;
        while (x > 0)
        {
            x = Mathf.MoveTowards(x, 0, 1 / time * Time.deltaTime);
            panel.alpha = x;
            yield return null;
        }
        //关闭读档界面
        this.gameObject.SetActive(false);
        //关闭警告框
        suim.warningContainer.SetActive(false);
        //关闭上级的SystemUI
        suim.gameObject.SetActive(false);
        //Node复原
        string modeName = DataManager.GetInstance().gameData.MODE;
        Debug.Log("读档前往：" + modeName);
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
    }



}