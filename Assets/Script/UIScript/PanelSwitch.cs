using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Assets.Script.UIScript;
//using Assets.Script.UIScript;

/**
 * PanelSwitch: 
 * 整个游戏只允许一个，作为GameManager的组件，不能删除
 * 保存其他模块间切换函数，供其他Manager调用
 * 修改相应参数即修改显示效果
 */
public class PanelSwitch : MonoBehaviour
{

    private GameObject root;

    /// <summary>
    /// 存储面板名称
    /// </summary>
    public readonly List<String> PANEL_NAMES = new List<String>()
    {
        "Avg",
        "Title",
        //"Invest",
        //"Detect",
        "System",
        //"Enquire",
        //"Negotiate",
        "Map",
        "Edu",
        "Phone"
    };

    Dictionary<string, GameObject> panels;
    private string current;

    // Use this for initialization
    public void Init()
    {
        root = GameObject.Find("UI Root");
        panels = new Dictionary<string, GameObject>();
        for (int i = 0; i < PANEL_NAMES.Count; i++)
        {
            GameObject panelObj = root.transform.Find(PANEL_NAMES[i] + "_Panel").gameObject;
            panels.Add(PANEL_NAMES[i], panelObj);
        }
        current = "Title";
    }
    //开启关闭系统菜单
    public void OpenMenu()
    {
        if (!panels["Title"].activeSelf)//标题除外
        {
            //if (panels["SysMenu"].activeSelf)//已经开启的情况
            if (panels["System"].activeSelf)
            {
                StartCoroutine(Fadeout(0.5f, panels["System"]));
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
    /// 切换面板,TODO:可能需要对每个Panel做一个切换特效？
    /// </summary>
    /// <param name="panel">切换目标面板</param>
    /// <param name="fadein">淡入时间，默认0.3s</param>
    /// <param name="fadeout">淡出时间，默认0.3s</param>
    public void SwitchTo(string panel, float fadein = 0.3f, float fadeout = 0.3f)
    {
        
        if (panels.ContainsKey(panel))
        {
            GameObject currentPanel = panels[current];
            GameObject nextPanel = panels[panel];
            Debug.Log(panel);
            StartCoroutine(Switch(currentPanel, nextPanel, fadein, fadeout));
            current = panel;
        }
        else
        {
            Debug.Log("Can't find panel: " + panel);
        }
    }

    private IEnumerator Switch(GameObject currentPanel, GameObject nextPanel, float fadein, float fadeout)
    {
        PanelFade2 current = currentPanel.GetComponent<PanelFade2>(),
                   next = nextPanel.GetComponent<PanelFade2>();
        current.Close(fadeout);
        yield return new WaitForSeconds(fadeout);
        nextPanel.SetActive(true);
        next.Open(fadein);

    }

    //打开手机
    public void OpenPhone()
    {
        StartCoroutine(Fadein(0.2f, panels["Phone"]));
    }

    public void ClosePhone()
    {
        StartCoroutine(Fadeout(0.2f, panels["Phone"]));
    }

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

    IEnumerator DelaySec(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action();
    }
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
