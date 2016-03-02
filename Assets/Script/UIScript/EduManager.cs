using UnityEngine;
using System.Collections;
using System;
//using Assets.Script.UIScript;

/**
 * EduManager: 
 * 整个游戏只允许一个，作为EduPanel的组件，不能被删除
 * 控制EduPanel下面的各部分与之交互
 * 提供方法供旗下按钮调用，并修改游戏数据
 * 实现与AVG模块的互动，推动游戏进程
 */
public class EduManager : MonoBehaviour, IPanelManager
{

    public static readonly string[] WEEKDAYS = {
            "星期一",
            "星期二",
            "星期三",
            "星期四",
            "星期五",
            "星期六",
            "星期天"
        };

    private GameManager gm;
    private GameObject eduObject;
    private UIPanel eduPanel;
    private UILabel daylabel, datelabel, moneylabel;
    private UILabel wenlabel, lilabel, tilabel, yilabel, zhailabel, showlabel;

    private GameObject qgo, sgo, acgo;

    void Awake () {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        daylabel = transform.Find("Time_Container/Day_Label").gameObject.GetComponent<UILabel>();
        datelabel = transform.Find("Time_Container/Date_Label").gameObject.GetComponent<UILabel>();
        moneylabel = transform.Find("Time_Container/Money_Label").gameObject.GetComponent<UILabel>();
        wenlabel = transform.Find("CharaInfo_Container/Num_Grid/Wen_Label").gameObject.GetComponent<UILabel>();
        lilabel = transform.Find("CharaInfo_Container/Num_Grid/Li_Label").gameObject.GetComponent<UILabel>();
        tilabel = transform.Find("CharaInfo_Container/Num_Grid/Ti_Label").gameObject.GetComponent<UILabel>();
        yilabel = transform.Find("CharaInfo_Container/Num_Grid/Yi_Label").gameObject.GetComponent<UILabel>();
        zhailabel = transform.Find("CharaInfo_Container/Num_Grid/Zhai_Label").gameObject.GetComponent<UILabel>();
        qgo = transform.Find("QSprite_Container").gameObject;
        sgo = transform.Find("Selection_Container").gameObject;
        acgo = transform.Find("QSprite_Container/Container").gameObject;
        showlabel = transform.Find("QSprite_Container/Show_Label").gameObject.GetComponent<UILabel>();
        UIFresh();
    }



    public IEnumerator Open()
    {

        this.GetComponent<PanelFade>().FadeIn(0, 0);

        return null;
    }

    IEnumerator FadeIn()
    {
        eduObject.SetActive(true);
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1/ 0.3f * Time.deltaTime);
            eduPanel.alpha = x;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public void UIFresh()
    {
        daylabel.text = gm.playerdata.month.ToString() + "月"+ gm.playerdata.day.ToString() + "日";
        datelabel.text = GetWeek(gm.playerdata.week);
        moneylabel.text = "金钱：" + gm.playerdata.money.ToString();
        wenlabel.text = gm.playerdata.wen.ToString();
        lilabel.text = gm.playerdata.li.ToString();
        yilabel.text = gm.playerdata.yi.ToString();
        tilabel.text = gm.playerdata.ti.ToString();
        zhailabel.text = gm.playerdata.zhai.ToString();
    }
    string GetWeek(int x)
    {
        return WEEKDAYS[x];
    }

    //显示Q版界面与文字信息
    public void ShowAnime(int x)
    {
        //NumGenerate(x);//数值增减
        StartCoroutine(Animate());//显示动画(临时用文字代替)
    }
    IEnumerator Animate()
    {
        sgo.SetActive(false);
        qgo.SetActive(true);
        float i = 0;
        while (i < 2)
        {
            if (i < 1) showlabel.text = "动画还有 2 秒";
            else if (i < 2) showlabel.text = "动画还有 1 秒";
            else showlabel.text = "";
            i = Mathf.MoveTowards(i, 2, Time.deltaTime);
            yield return null;
        }
        showlabel.text = "结算显示：请点击任意地方进入下一天";
        acgo.SetActive(true);
    }
    public void NextDay()
    {
        acgo.SetActive(false);
        qgo.SetActive(false);
        gm.NextDay();
        sgo.SetActive(true);
        UIFresh();
    }
    // duplicated
    void NumGenerate(int choice)
    {
        switch (choice)
        {
            case 1:
                gm.playerdata.wen += UnityEngine.Random.Range(1,6);
                break;
            case 2:
                gm.playerdata.li += UnityEngine.Random.Range(1, 6);
                break;
            case 3:
                gm.playerdata.ti += UnityEngine.Random.Range(1, 6);
                break;
            case 4:
                gm.playerdata.yi += UnityEngine.Random.Range(1, 6);
                break;
            case 5:
                gm.playerdata.wen += 8;
                gm.playerdata.li -= 3;
                break;
            case 6:
                gm.playerdata.li += 8;
                gm.playerdata.wen -= 3;
                break;
            case 7:
                gm.playerdata.ti += 8;
                gm.playerdata.yi -= 3;
                break;
            case 8:
                gm.playerdata.yi += 8;
                gm.playerdata.ti -= 3;
                break;
            default:
                break;
        }
    }
    public string GetHelp(int num)
    {
        switch (num)
        {
            case 1:
                return "这是第1个选项：暂定是增加【文科】属性，成功概率：高。";
            case 2:
                return "这是第2个选项：暂定是增加【理科】属性，成功概率：高。";
            case 3:
                return "这是第3个选项：暂定是增加【体育】属性，成功概率：高。";
            case 4:
                return "这是第4个选项：暂定是增加【艺术】属性，成功概率：高。";
            case 5:
                return "这是第5个选项：暂定是大幅增加【文科】属性，但降低【理科】属性，成功概率：中。";
            case 6:
                return "这是第6个选项：暂定是大幅增加【理科】属性，但降低【文科】属性，成功概率：中。";
            case 7:
                return "这是第7个选项：暂定是大幅增加【体育】属性，但降低【艺术】属性，成功概率：中。";
            case 8:
                return "这是第8个选项：暂定是大幅增加【艺术】属性，但降低【体育】属性，成功概率：中。";
        }
        return "";
    }

    public IEnumerator Close()
    {
        this.GetComponent<PanelFade>().FadeOut(0, 0);
        return null;
    }

    IEnumerator FadeOut()
    {
        float x = 1;
        while (x > 0)
        {
            x = Mathf.MoveTowards(x, 0, 1 / 0.3f * Time.deltaTime);
            eduPanel.alpha = x;
            yield return null;
        }
    }
}
