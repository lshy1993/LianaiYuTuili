using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 
/// PanelSwitch: 
/// 用于切换UIpanel,主要的方法有SwitchTo,SwitchPhone等
/// 
/// </summary>
public class PanelSwitch : MonoBehaviour{


    // Sources:
    private readonly string[] PANEL_NAMES = {
        "Avg",
        "Title",
        "Invest",
        "Detect",
        "SysMenu",
        "Enquire",
        "Negotiate",
        "Map",
        "Edu",
        "Phone"
    };


    private GameObject root;
    private Dictionary<string, GameObject> panels;
    private bool isPhoneOpen;
    //public GameObject currentPanel;
    public string currentPanel;
    public void Init()
    {
        root = GameObject.Find("UI Root");
        panels = new Dictionary<string, GameObject>();
        for(int i = 0; i < PANEL_NAMES.Length; i++)
        {
            panels.Add(PANEL_NAMES[i], root.transform.Find(PANEL_NAMES[i] + 
                "_Panel").gameObject);
        }
        currentPanel = "Title"; // 默认载入标题界面
        isPhoneOpen = false;
        StartCoroutine(Fadein(0.5f, panels[currentPanel]));
        panels[currentPanel].SetActive(true);
   }

    /// <summary>
    /// MenuSwitch 
    /// 用于开启关闭系统菜单
    /// </summary>
    public void MenuSwitch()
    {
        if (!panels["Title"].activeSelf)//标题除外
        {
            if (panels["SysMenu"].activeSelf)//已经开启的情况
            {
                StartCoroutine(Fadeout(0.5f, panels["SysMenu"]));
                Debug.Log("Close Menu!");
            }
            else//关闭的情况
            {
                StartCoroutine(Fadein(0.5f, panels["SysMenu"]));
                Debug.Log("Open Menu!");
            }
        }
    }


    /// <summary>
    /// 用于切换场景UI
    /// </summary>
    /// <param name="nextPanel">下一个需要切换的UI Panel名</param>
    /// <param name="fadein">淡入时间，默认为0.5秒</param>
    /// <param name="fadeout">淡出时间，默认为0.5秒</param>
    public void SwitchTo(string nextPanel, double fadein = 0.5, double fadeout = 0.5)
    {
        if (nextPanel == currentPanel || !panels.ContainsKey(nextPanel)) return;

        StartCoroutine(Fadeout((float)fadeout, panels[currentPanel]));
        Debug.Log("nextPanel: " + nextPanel);
        currentPanel = nextPanel;
        StartCoroutine(Fadein((float)fadein, panels[currentPanel]));
    }
    /// <summary>
    /// 打开手机界面
    /// </summary>
    public void OpenPhone()
    {
        StartCoroutine(Fadein(0.2f, panels["Phone"]));
    }
    /// <summary>
    /// 关闭手机界面
    /// </summary>
    public void ClosePhone()
    {
        StartCoroutine(Fadeout(0.2f, panels["Phone"]));
    }

    public void SwitchPhone()
    {
        if (isPhoneOpen) // close
        {
            StartCoroutine(Fadeout(0.2f, panels["Phone"]));
            isPhoneOpen = false;
        }
        else // open
        {
            StartCoroutine(Fadein(0.2f, panels["Phone"]));
            isPhoneOpen = true;
        }
    }

    /// <summary>
    /// 淡入的实现，用于StartCoroutine
    /// </summary>
    /// <param name="time">时间</param>
    /// <param name="target">目标UI Panel</param>
    /// <returns>返回迭代器</returns>
    IEnumerator Fadein(float time, GameObject target)
    {
        UIPanel panel = target.GetComponent<UIPanel>();
        float f = time == 0 ? 1 : 0;
        panel.alpha = f;
        target.SetActive(true);
        while (f < 1f)
        {
            f = Mathf.MoveTowards(f, 1f, Time.deltaTime / time);
            panel.alpha = f;
            yield return null;
        }
    }

    /// <summary>
    /// 淡出的实现，用于StartCoroutine
    /// </summary>
    /// <param name="time">时间</param>
    /// <param name="target">目标UI Panel</param>
    /// <returns>返回迭代器</returns>
    IEnumerator Fadeout(float time, GameObject target)
    {
        UIPanel panel = target.GetComponent<UIPanel>();
        float f = time == 0 ? 0 : 1;
        panel.alpha = f;
        while (f > 0)
        {
            f = Mathf.MoveTowards(f, 0, Time.deltaTime / time);
            panel.alpha = f;
            yield return null;
        }
        target.SetActive(false);
    }
}
