using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct;
using System.Collections.Generic;
using Assets.Script.GameStruct.Model;

public class SystemUIManager : MonoBehaviour
{

    public PanelSwitch ps;
    public GameObject butContainer, saveloadContainer, settingContainer, backlogContainer, warningContainer;
    public GameObject saveTable;
    private int savenum, groupnum = 1;
    private Dictionary<int, SavingInfo> savedic;
    private Dictionary<string, byte[]> savepic;

    public void Open()
    {
        StartCoroutine(ScreenShot());
        StartCoroutine(FadeInP(0.3f));
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
        DataPool.GetInstance().WriteSystemVar("缩略图", imagebytes);
        Destroy(tex);
    }

    public void Close()
    {
        StartCoroutine(FadeOutP(0.3f));
    }

    public void OpenSetting()
    {
        if (Input.GetMouseButtonUp(1)) return;
        butContainer.SetActive(false);
        StartCoroutine(FadeIn(settingContainer, 0.3f));
    }

    public void OpenBacklog()
    {
        if (Input.GetMouseButtonUp(1)) return;
        butContainer.SetActive(false);
        StartCoroutine(FadeIn(backlogContainer, 0.3f));
    }

    public void OpenSave()
    {
        if (Input.GetMouseButtonUp(1)) return;
        saveloadContainer.transform.Find("SL_Label").GetComponent<UILabel>().text = "存档";
        butContainer.SetActive(false);
        StartCoroutine(FadeIn(saveloadContainer, 0.3f));
    }

    public void OpenLoad()
    {
        if (Input.GetMouseButtonUp(1)) return;
        saveloadContainer.transform.Find("SL_Label").GetComponent<UILabel>().text = "读档";
        butContainer.SetActive(false);
        StartCoroutine(FadeIn(saveloadContainer));
    }

    //按下存档按钮
    public void SelectSave(int x)
    {
        savenum = x;
        if(savedic.ContainsKey(groupnum * 6 + savenum))
        {
            OpenWarning("存档");
        }
        else
        {
            DataManager.GetInstance().Save(savenum);
            FreshSaveTable();
        }
    }

    //警告按钮
    public void OpenWarning(string str)
    {
        if (Input.GetMouseButtonUp(1)) return;
        warningContainer.GetComponent<UIWidget>().alpha = 1;
        if (str.Contains("Title"))
        {
            warningContainer.transform.Find("Warning_Label").GetComponent<UILabel>().text = "返回标题画面吗";
            warningContainer.SetActive(true);
        }

        if (str.Contains("Exit"))
        {
            warningContainer.transform.Find("Warning_Label").GetComponent<UILabel>().text = "退出游戏吗";
            warningContainer.SetActive(true);
        }

        if (str.Contains("存档"))
        {
            warningContainer.transform.Find("Warning_Label").GetComponent<UILabel>().text = "覆盖这个存档吗";
            warningContainer.SetActive(true);
        }
        if (str.Contains("读档"))
        {
            warningContainer.transform.Find("Warning_Label").GetComponent<UILabel>().text = "读取这个存档吗";
            warningContainer.SetActive(true);
        }

        if (str.Contains("Delete"))
        {
            warningContainer.transform.Find("Warning_Label").GetComponent<UILabel>().text = "删除这个存档吗";
            warningContainer.SetActive(true);
        }
    }

    //警告确认
    public void WarningComfirm(string str)
    {
        if (Input.GetMouseButtonUp(1)) return;
        if (str == "返回标题画面吗")
        {
            StartCoroutine(FadeOut(warningContainer));
            StartCoroutine(FadeOutP());
            ps.SwitchTo_VerifyIterative("Title_Panel");
        }
        if(str == "读取这个存档吗")
        {
            DataManager.GetInstance().Load(groupnum * 6 + savenum);
            //切换界面
        }
        if(str == "覆盖这个存档吗")
        {
            DataManager.GetInstance().Save(groupnum * 6 + savenum);
            warningContainer.SetActive(false);
            FreshSaveTable();
        }
        if(str == "退出游戏吗")
        {
            Application.Quit();
        }
    }

    //警告取消
    public void WarningCancel()
    {
        if (Input.GetMouseButtonUp(1)) return;
        savenum = 0;
        warningContainer.SetActive(false);
    }

    public void SetFileNum()
    {
        if (!UIToggle.current.value) return;
        int x = UIToggle.current.GetComponent<ToggleNum>().id;
        groupnum = x - 1;
        FreshSaveTable();
    }

    //刷新存档表格
    private void FreshSaveTable()
    {
        savedic = (Dictionary<int, SavingInfo>)DataPool.GetInstance().GetSystemVar("存档信息");
        savepic = (Dictionary<string, byte[]>)DataPool.GetInstance().GetSystemVar("存档缩略图");
        for (int i = 1; i <= 6; i++)
        {
            int saveid = groupnum * 6 + i;
            GameObject go = saveTable.transform.Find("Saving_Box" + i).gameObject;
            if (savedic.ContainsKey(saveid) && savedic[saveid] != null)
            {
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
                go.transform.Find("Info_Label").GetComponent<UILabel>().text = "";
                go.transform.Find("Mode_Label").GetComponent<UILabel>().text = "";
                go.transform.Find("Time_Label").GetComponent<UILabel>().text = "";
                go.transform.Find("Save_Sprite").GetComponent<UI2DSprite>().sprite2D = Resources.LoadAll<Sprite>("Background/Title")[0];
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
        transform.gameObject.SetActive(false);
        butContainer.SetActive(false);
        saveloadContainer.SetActive(false);
        settingContainer.SetActive(false);
        backlogContainer.SetActive(false);
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
