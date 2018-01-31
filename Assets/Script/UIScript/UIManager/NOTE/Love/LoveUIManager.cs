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
    private GameObject basicCon, middleCon, tabCon, qtabGrid, infoNumGrid;
    private Player player { get { return DataManager.GetInstance().gameData.player; } }
    private Dictionary<string, Girl> dic;
    private List<string> girlInfo;

    private void Awake()
    {
        basicCon = transform.Find("Basic_Container").gameObject;
        middleCon = transform.Find("Middle_Container").gameObject;
        tabCon = transform.Find("InfoTab_Container").gameObject;
        //q版按钮
        qtabGrid = transform.Find("QTab_Grid").gameObject;
        //上方区块
        namelb = basicCon.transform.Find("Name_Label").gameObject.GetComponent<UILabel>();
        classlb = basicCon.transform.Find("Table/Class_Label").gameObject.GetComponent<UILabel>();
        clublb = basicCon.transform.Find("Table/Club_Label").gameObject.GetComponent<UILabel>();
        hlb = basicCon.transform.Find("Table/Height_Label").gameObject.GetComponent<UILabel>();
        wlb = basicCon.transform.Find("Table/Weight_Label").gameObject.GetComponent<UILabel>();
        birthlb = basicCon.transform.Find("Table/Birth_Label").gameObject.GetComponent<UILabel>();
        starlb = basicCon.transform.Find("Table/Star_Label").gameObject.GetComponent<UILabel>();
        //中间区块
        rlb = middleCon.transform.Find("Rank_Label").gameObject.GetComponent<UILabel>();
        likelb = middleCon.transform.Find("Like_Label").gameObject.GetComponent<UILabel>();
        dislb = middleCon.transform.Find("Dislike_Label").gameObject.GetComponent<UILabel>();
        //下方区块
        infoNumGrid = tabCon.transform.Find("NumTab_Grid").gameObject;
        infolb = tabCon.transform.Find("Info_Label").gameObject.GetComponent<UILabel>();
        
        dic = DataManager.GetInstance().staticData.girls;
    }

    private void OnEnable()
    {
        //每次开启时调用
        SetQButton();
        //隐藏主界面
        basicCon.SetActive(false);
        middleCon.SetActive(false);
        tabCon.SetActive(false);
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
        basicCon.SetActive(true);
        middleCon.SetActive(true);
        tabCon.SetActive(true);
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
