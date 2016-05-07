using UnityEngine;
using System.Collections;
using System;
using Assets.Script.GameStruct.Model;
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
    private GameManager gm;
    private GameObject eduObject;
    private UIPanel eduPanel;
    private UILabel daylabel, datelabel, moneylabel;
    private UILabel wenlabel, lilabel, tilabel, yilabel, zhailabel, showlabel;

    private GameObject qgo, sgo, acgo;

    void Awake()
    {
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
            x = Mathf.MoveTowards(x, 1, 1 / 0.3f * Time.deltaTime);
            eduPanel.alpha = x;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public void UIFresh()
    {
        Player player = (Player)GameManager.GetGlobalVars()["玩家数据"];
        daylabel.text = player.GetTime("月") + "月" + player.GetTime("日") + "日";
        datelabel.text = Player.WEEKDAYS[player.GetTime("星期")];
        moneylabel.text = "金钱: " + player.GetBasicStatus("金钱");
        wenlabel.text = player.GetBasicStatus("文科").ToString();
        lilabel.text = player.GetBasicStatus("理科").ToString();
        yilabel.text = player.GetBasicStatus("艺术").ToString();
        tilabel.text = player.GetBasicStatus("体育").ToString();
        zhailabel.text = player.GetBasicStatus("宅力").ToString();
    }

    string GetWeek(int x)
    {
        return Player.WEEKDAYS[x];
    }

    

    /// <summary>
    /// 显示Q版界面与文字信息
    /// </summary>
    /// <param name="eduItem">edu的项目</param>
    /// <param name="result">执行结果</param>
    public void ShowAnime(string eduItem, int result)
    {
        switch (result)
        {

        }
        //NumGenerate(x);//数值增减
        //StartCoroutine(Animate());//显示动画(临时用文字代替)
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
        //        gm.NextDay();
        sgo.SetActive(true);
        UIFresh();
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
