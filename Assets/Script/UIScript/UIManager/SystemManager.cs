using UnityEngine;
using System.Collections;

public class SystemManager : MonoBehaviour
{

    public PanelSwitch ps;
    public GameObject butContainer, saveloadContainer, settingContainer, backlogContainer, warningContainer;

    #region 打开到but 与 从任意状态关闭
    public void Open()
    {
        StartCoroutine(FadeInP(0.3f));
        StartCoroutine(FadeIn(butContainer, 0.3f));
    }
    public void Close()
    {
        StartCoroutine(FadeOutP(0.3f));
    }
    #endregion

    public void OpenSetting()
    {
        if (Input.GetMouseButtonUp(1)) return;
        butContainer.SetActive(false);
        //StartCoroutine(FadeOut(butContainer));
        StartCoroutine(FadeIn(settingContainer, 0.3f));
    }

    public void OpenBacklog()
    {
        if (Input.GetMouseButtonUp(1)) return;
        butContainer.SetActive(false);
        //StartCoroutine(FadeOut(butContainer));
        StartCoroutine(FadeIn(backlogContainer, 0.3f));
    }

    public void OpenSave()
    {
        if (Input.GetMouseButtonUp(1)) return;
        butContainer.SetActive(false);
        //StartCoroutine(FadeOut(butContainer));
        StartCoroutine(FadeIn(saveloadContainer, 0.3f));
    }

    public void OpenLoad()
    {
        if (Input.GetMouseButtonUp(1)) return;
        butContainer.SetActive(false);
        //StartCoroutine(FadeOut(butContainer));
        StartCoroutine(FadeIn(saveloadContainer));
    }

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

        }
        if(str == "覆盖这个存档吗")
        {

        }
        if(str == "退出游戏吗")
        {
            Application.Quit();
        }
    }

    public void WarningCancel()
    {
        if (Input.GetMouseButtonUp(1)) return;
        warningContainer.SetActive(false);
    }

    public void SetFileNum(int x)
    {

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
