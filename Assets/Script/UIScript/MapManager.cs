using UnityEngine;
using System.Collections;

/**
 * MapManager: 
 * 整个游戏只允许一个，作为MapPanel的组件，不能被删除
 * 控制MapPanel下面的各部分与之交互
 * 提供方法供旗下按钮调用，并修改游戏数据
 * 实现与AVG模块的互动，推动游戏进程
 */
public class MapManager : MonoBehaviour, IPanelManager
{

    private GameManager gm;
    private GameObject mapObject;
    private UIPanel mapPanel;
    private UILabel daylabel, datelabel;
    private UILabel wenlabel, lilabel, tilabel, yilabel, zhailabel, moneylabel;

    void Start () {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        mapObject = transform.parent.gameObject;
        daylabel = transform.Find("Time_Container/Day_Label").gameObject.GetComponent<UILabel>();
        datelabel = transform.Find("Time_Container/Date_Label").gameObject.GetComponent<UILabel>();
        moneylabel = transform.Find("CharaInfo_Container/Number_Container/Money_Label").gameObject.GetComponent<UILabel>();
        wenlabel = transform.Find("CharaInfo_Container/Number_Container/Wen_Label").gameObject.GetComponent<UILabel>();
        lilabel = transform.Find("CharaInfo_Container/Number_Container/Li_Label").gameObject.GetComponent<UILabel>();
        tilabel = transform.Find("CharaInfo_Container/Number_Container/Ti_Label").gameObject.GetComponent<UILabel>();
        yilabel = transform.Find("CharaInfo_Container/Number_Container/Yi_Label").gameObject.GetComponent<UILabel>();
        zhailabel = transform.Find("CharaInfo_Container/Number_Container/Zhai_Label").gameObject.GetComponent<UILabel>();
        UIFresh();
    }

	void Update () {
	
	}

    public IEnumerator Open()
    {
        mapPanel.alpha = 0;
        yield return StartCoroutine(FadeIn());
    }
    public IEnumerator Close()
    {
        yield return StartCoroutine(FadeOut());
    }
    IEnumerator FadeIn()
    {
        mapObject.SetActive(true);
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, Time.deltaTime);
            mapPanel.alpha = x;
            yield return null;
        }
    }
    IEnumerator FadeOut()
    {
        float x = 1;
        while (x > 0)
        {
            x = Mathf.MoveTowards(x, 0, Time.deltaTime);
            mapPanel.alpha = x;
            yield return null;
        }
        mapObject.SetActive(false);
    }
    public void UIFresh()
    {
        daylabel.text = gm.playerdata.month.ToString() + "月" + gm.playerdata.day.ToString() + "日";
        datelabel.text = GetWeek(gm.playerdata.week);
        moneylabel.text = gm.playerdata.money.ToString();
        wenlabel.text = gm.playerdata.wen.ToString();
        lilabel.text = gm.playerdata.li.ToString();
        yilabel.text = gm.playerdata.yi.ToString();
        tilabel.text = gm.playerdata.ti.ToString();
        zhailabel.text = gm.playerdata.zhai.ToString();
    }
    string GetWeek(int x)
    {
        switch (x)
        {
            case 1:
                return "星期一";
            case 2:
                return "星期二";
            case 3:
                return "星期三";
            case 4:
                return "星期四";
            case 5:
                return "星期五";
            case 6:
                return "星期六";
            case 7:
                return "星期日";
            default:
                return "";
        }
    }
    public void GoPlace(int placeid)
    {
        gm.placeid = placeid.ToString();
        gm.MapEvent();
        UIFresh();
    }
}
