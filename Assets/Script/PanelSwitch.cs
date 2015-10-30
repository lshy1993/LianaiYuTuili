using UnityEngine;
using System.Collections;

/**
 * PanelSwitch: 
 * 整个游戏只允许一个，作为GameManager的组件，不能删除
 * 保存其他模块间切换函数，供其他Manager调用
 * 修改相应参数即修改显示效果
 */
public class PanelSwitch : MonoBehaviour {

    public UIRoot uiroot;

    private GameObject avgpanel;
    private GameObject syspanel;
    private GameObject investpanel;
    private GameObject detectpanel;
    private GameObject titlepanel;
    private GameObject enquirepanel;
    private GameObject negotiatepanel;
    private GameObject mappanel;
    private GameObject edupanel;
    //private UIPanel panel;

    // Use this for initialization
    void Start ()
    {
        avgpanel = uiroot.transform.Find("Avg_Panel").gameObject;
        titlepanel = uiroot.transform.Find("Title_Panel").gameObject;
        investpanel = uiroot.transform.Find("Invest_Panel").gameObject;
        detectpanel = uiroot.transform.Find("Detect_Panel").gameObject;
        syspanel = uiroot.transform.Find("SysMenu_Panel").gameObject;
        enquirepanel = uiroot.transform.Find("Enquire_Panel").gameObject;
        negotiatepanel = uiroot.transform.Find("Negotiate_Panel").gameObject;
        mappanel = uiroot.transform.Find("Map_Panel").gameObject;
        edupanel = uiroot.transform.Find("Edu_Panel").gameObject;
    }
    //开启关闭系统菜单
    public void OpenMenu()
    {
        if (!titlepanel.activeSelf)//标题除外
        {
            if (syspanel.activeSelf)//已经开启的情况
            {
                StartCoroutine(Fadeout(1, syspanel));
                Debug.Log("Close Menu!");
            }
            else//关闭的情况
            {
                StartCoroutine(Fadein(1, syspanel));
                Debug.Log("Open Menu!");
            }
        }
    }
    //开启调查模式 同时关闭avg
    public void OpenInvest()
    {
        StartCoroutine(Fadein(0, investpanel));
        StartCoroutine(Fadeout(0, avgpanel));
    }
    public void CloseInvest()
    {
        StartCoroutine(Fadeout(0, investpanel));
        StartCoroutine(Fadein(0, avgpanel));
    }
    //开启推理模式 同时关闭avg
    public void OpenDetect()
    {
        StartCoroutine(Fadein(0, detectpanel));
        StartCoroutine(Fadeout(0, avgpanel));
    }
    public void CloseDetect()
    {
        StartCoroutine(Fadeout(0, detectpanel));
        StartCoroutine(Fadein(0, avgpanel));
    }
    //开启询问 同时关闭avg
    public void OpenEnquire()
    {
        StartCoroutine(Fadein(0, enquirepanel));
        StartCoroutine(Fadeout(0, avgpanel));
    }
    public void CloseEnquire()
    {
        StartCoroutine(Fadeout(0, enquirepanel));
        StartCoroutine(Fadein(0, avgpanel));
    }
    //开启谈判 同时关闭avg
    public void OpenNegotiate()
    {
        StartCoroutine(Fadein(0, negotiatepanel));
        StartCoroutine(Fadeout(0, avgpanel));
    }
    public void CloseNegotate()
    {
        StartCoroutine(Fadeout(0, negotiatepanel));
        StartCoroutine(Fadein(0, avgpanel));
    }
    //打开大地图
    public void OpenMap()
    {
        StartCoroutine(Fadein(0.5f, mappanel));
        StartCoroutine(Fadeout(0, avgpanel));
    }
    //打开养成
    public void OpenEdu()
    {
        StartCoroutine(Fadein(0.5f, edupanel));
        StartCoroutine(Fadeout(0, avgpanel));
    }
    IEnumerator Fadein(float time, GameObject target)
    {
        UIPanel panel = target.GetComponent<UIPanel>();
        float f = time == 0 ? 1 : 0;
        panel.alpha = f;
        target.SetActive(true);
        while (f < 1)
        {
            f = Mathf.MoveTowards(panel.alpha, 1, Time.deltaTime / time);
            panel.alpha = f;
            yield return null;
        }
    }

    IEnumerator Fadeout(float time, GameObject target)
    {
        UIPanel panel = target.GetComponent<UIPanel>();
        float f = time == 0 ? 0 : 1;
        panel.alpha = f;
        while (f > 0)
        {
            f = Mathf.MoveTowards(panel.alpha, 0, Time.deltaTime / time);
            panel.alpha = f;
            yield return null;
        }
        target.SetActive(false);
    }

}
