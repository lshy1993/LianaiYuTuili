using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;

public class LoveUIManager : MonoBehaviour
{
    private UILabel namelb, classlb, clublb, hlb, wlb, birthlb, starlb, rlb, likelb, dislb, infolb;
    private GameObject introCon, qtabGrid, infoNumGrid;
    private Player player { get { return DataManager.GetInstance().gameData.player; } }
    private Dictionary<string, Girl> dic;
    private List<string> girlInfo;

    private void Awake()
    {
        introCon = transform.Find("Intro_Container").gameObject;
        qtabGrid = transform.Find("QTab_Grid").gameObject;

        namelb = introCon.transform.Find("Name_Label").gameObject.GetComponent<UILabel>();

        classlb = introCon.transform.Find("Table/Class_Label").gameObject.GetComponent<UILabel>();
        clublb = introCon.transform.Find("Table/Club_Label").gameObject.GetComponent<UILabel>();
        hlb = introCon.transform.Find("Table/Height_Label").gameObject.GetComponent<UILabel>();
        wlb = introCon.transform.Find("Table/Weight_Label").gameObject.GetComponent<UILabel>();
        birthlb = introCon.transform.Find("Table/Birth_Label").gameObject.GetComponent<UILabel>();
        starlb = introCon.transform.Find("Table/Star_Label").gameObject.GetComponent<UILabel>();

        rlb = introCon.transform.Find("Rank_Label").gameObject.GetComponent<UILabel>();
        likelb = introCon.transform.Find("Like_Label").gameObject.GetComponent<UILabel>();
        dislb = introCon.transform.Find("Dislike_Label").gameObject.GetComponent<UILabel>();
        infolb = introCon.transform.Find("Info_Label").gameObject.GetComponent<UILabel>();

        infoNumGrid = introCon.transform.Find("NumTab_Grid").gameObject;

        dic = DataManager.GetInstance().staticData.girls;
    }

    private void OnEnable()
    {
        //每次开启时调用
        SetQButton();
        //隐藏主界面
        introCon.SetActive(false);
    }

    private void SetQButton()
    {
        //根据是否遇到了女生 调整头像开关player.GetGirlPoint("苏梦忆") >= 0player.GetGirlPoint("西门吹") >= 0
        qtabGrid.transform.Find("QButton1").gameObject.SetActive(true);
        qtabGrid.transform.Find("QButton2").gameObject.SetActive(true);
        qtabGrid.transform.Find("QButton3").gameObject.SetActive(player.GetGirlPoint("欧阳晓芸") > 0);
        qtabGrid.transform.Find("QButton4").gameObject.SetActive(player.GetGirlPoint("车小曼") > 0);
        qtabGrid.transform.Find("QButton5").gameObject.SetActive(player.GetGirlPoint("陈瑜") > 0);
    }

    public void ShowGirl(string str)
    {
        //供QBtn按钮调用
        SetGirlInfo(str);
    }

    private void SetGirlInfo(string str)
    {
        introCon.SetActive(true);
        //设置联系人信息 根据好感度变化
        namelb.text = dic[str].name;
        classlb.text = dic[str].cla;
        clublb.text = dic[str].club;
        string heightStr = player.GetGirlPoint(str) >= 1 ? dic[str].height.ToString() : "???";
        hlb.text = "身高：" + heightStr + "cm";
        string weightStr = player.GetGirlPoint(str) >= 1 ? dic[str].weight.ToString() : "???";
        wlb.text = "体重：" + weightStr + "kg";
        string birthM = player.GetGirlPoint(str) >= 2 ? dic[str].monthOfBirth.ToString() : "?";
        string birthD = player.GetGirlPoint(str) >= 2 ? dic[str].dayOfBirth.ToString() : "?";
        string horoStr = player.GetGirlPoint(str) >= 2 ? dic[str].horoscope : "???";
        birthlb.text = "生日：" +birthM  + "月" + birthD + "日";
        starlb.text = "星座：" + horoStr;
        //rlb.text = "排名：年级" + gm.girl[x].graderank + "名 全省" + gm.girl[x].provencerank + "名";
        string likeStr = "??????";
        if (player.GetGirlPoint(str) >= 3)
        {
            likeStr = "";
            foreach (string st in dic[str].like)
            {
                likeStr += " " + st;
            }
        }
        likelb.text = "喜欢：" + likeStr;
        string dislikeStr = "??????";
        if(player.GetGirlPoint(str) >= 3)
        {
            dislikeStr = "";
            foreach (string st in dic[str].dislike)
            {
                dislikeStr += " " + st;
            }
        }
        dislb.text = "讨厌：" + dislikeStr;
        //多信息介绍界面
        for(int i = 0; i < player.GetGirlPoint(str); i++)
        {
            infoNumGrid.transform.GetChild(i).gameObject.SetActive(true);
        }
        girlInfo = dic[str].intro;
        ShowGirlIntro("");
    }

    public void ShowGirlIntro(string btnname)
    {
        switch (btnname)
        {
            case "One_Toggle":
                infolb.text = girlInfo[0];
                break;
            case "Two_Toggle":
                infolb.text = girlInfo[1];
                break;
            case "Three_Toggle":
                infolb.text = girlInfo[2];
                break;
            case "Four_Toggle":
                infolb.text = girlInfo[3];
                break;
            case "Five_Toggle":
                infolb.text = girlInfo[4];
                break;
            default:
                infolb.text = girlInfo[0];
                break;
        }
    }

}
