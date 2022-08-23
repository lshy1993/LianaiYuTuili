using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;
using System.Collections;

/// <summary>
/// 信息UI管理
/// </summary>
public class SelfUIManager : MonoBehaviour
{
    private UILabel wenlb, lilb, tilb, yilb, zhailb, energylb;
    private UIProgressBar wenb, lib, tib, yib, zhaib, energyb;
    private UILabel namelb, seedlb, ranklb1, ranklb2, moneylb, statuslb;
    private UIProgressBar lengb, koub, sib, guanb;
    private UILabel ranklb_wen, ranklb_li, ranklb_yi, ranklb_ti, ranklb_zhai;
    private UISlider ranksd_wen, ranksd_li, ranksd_yi, ranksd_ti, ranksd_zhai;

    private Player player;

    private void Awake()
    {
        //各科属性数值
        wenlb = transform.Find("StudyData_Container/Num_Grid/Wen_Label").gameObject.GetComponent<UILabel>();
        lilb = transform.Find("StudyData_Container/Num_Grid/Li_Label").gameObject.GetComponent<UILabel>();
        tilb = transform.Find("StudyData_Container/Num_Grid/Ti_Label").gameObject.GetComponent<UILabel>();
        yilb = transform.Find("StudyData_Container/Num_Grid/Yi_Label").gameObject.GetComponent<UILabel>();
        zhailb = transform.Find("StudyData_Container/Num_Grid/Zhai_Label").gameObject.GetComponent<UILabel>();
        
        //各科进度条
        wenb = transform.Find("StudyData_Container/Bar_Grid/Wen_Bar").gameObject.GetComponent<UIProgressBar>();
        lib = transform.Find("StudyData_Container/Bar_Grid/Li_Bar").gameObject.GetComponent<UIProgressBar>();
        tib = transform.Find("StudyData_Container/Bar_Grid/Ti_Bar").gameObject.GetComponent<UIProgressBar>();
        yib = transform.Find("StudyData_Container/Bar_Grid/Yi_Bar").gameObject.GetComponent<UIProgressBar>();
        zhaib = transform.Find("StudyData_Container/Bar_Grid/Zhai_Bar").gameObject.GetComponent<UIProgressBar>();
        
        //基本框
        namelb = transform.Find("Card_Container/Name_Label").gameObject.GetComponent<UILabel>();
        seedlb = transform.Find("Card_Container/UID_Container/StudentNumber_Label").gameObject.GetComponent<UILabel>();
        energylb = transform.Find("Card_Container/Energy_Container/Energy_Label").gameObject.GetComponent<UILabel>();
        energyb = transform.Find("Card_Container/Energy_Container/Energy_Bar").gameObject.GetComponent<UIProgressBar>();

        //排名
        ranklb1 = transform.Find("Card_Container/TestRank_Container/LoaclRank_Label").gameObject.GetComponent<UILabel>();
        ranklb2 = transform.Find("Card_Container/TestRank_Container/NationalRank_Label").gameObject.GetComponent<UILabel>();
        moneylb = transform.Find("Card_Container/Money_Container/Money_Label").gameObject.GetComponent<UILabel>();
        statuslb = transform.Find("Card_Container/Status_Container/Status_Label").gameObject.GetComponent<UILabel>();

        //侦探数据
        lengb = transform.Find("DetectData_Container/Bar_Grid_H/Leng_Bar").gameObject.GetComponent<UIProgressBar>();
        koub = transform.Find("DetectData_Container/Bar_Grid_H/Kou_Bar").gameObject.GetComponent<UIProgressBar>();
        sib = transform.Find("DetectData_Container/Bar_Grid_H/Si_Bar").gameObject.GetComponent<UIProgressBar>();
        guanb = transform.Find("DetectData_Container/Bar_Grid_H/Guan_Bar").gameObject.GetComponent<UIProgressBar>();

        //网络统计类
        ranklb_wen = transform.Find("WebRank_Container/WebRank_Grid/WenRank_Label").gameObject.GetComponent<UILabel>();
        ranklb_li =  transform.Find("WebRank_Container/WebRank_Grid/LiRank_Label").gameObject.GetComponent<UILabel>();
        ranklb_yi = transform.Find("WebRank_Container/WebRank_Grid/YiRank_Label").gameObject.GetComponent<UILabel>();
        ranklb_ti = transform.Find("WebRank_Container/WebRank_Grid/TiRank_Label").gameObject.GetComponent<UILabel>();
        ranklb_zhai = transform.Find("WebRank_Container/WebRank_Grid/ZhaiRank_Label").gameObject.GetComponent<UILabel>();

        ranksd_wen = transform.Find("WebRank_Container/WebRankPos_Grid/WenPos_Bar").gameObject.GetComponent<UISlider>();
        ranksd_li = transform.Find("WebRank_Container/WebRankPos_Grid/LiPos_Bar").gameObject.GetComponent<UISlider>();
        ranksd_yi = transform.Find("WebRank_Container/WebRankPos_Grid/YiPos_Bar").gameObject.GetComponent<UISlider>();
        ranksd_ti = transform.Find("WebRank_Container/WebRankPos_Grid/TiPos_Bar").gameObject.GetComponent<UISlider>();
        ranksd_zhai = transform.Find("WebRank_Container/WebRankPos_Grid/ZhaiPos_Bar").gameObject.GetComponent<UISlider>();


    }

    private void OnEnable()
    {
        player = DataManager.GetInstance().gameData.player;
        SetCardInfo();
        //数值条动画
        StartCoroutine(ShowBar(wenb, player.GetBasicStatus("文科"), 200));
        StartCoroutine(ShowBar(lib, player.GetBasicStatus("理科"), 200));
        StartCoroutine(ShowBar(tib, player.GetBasicStatus("体育"), 200));
        StartCoroutine(ShowBar(yib, player.GetBasicStatus("艺术"), 200));
        StartCoroutine(ShowBar(zhaib, player.GetBasicStatus("宅力"), 200));
        StartCoroutine(ShowBar(energyb, player.energyPoint, 150));
    }

    /// <summary>
    /// 基本信息UI显示
    /// </summary>
    private void SetCardInfo()
    {
        //[基本信息]设置学生证
        string xing = DataManager.GetInstance().gameData.heroXing;
        string ming = DataManager.GetInstance().gameData.heroMing;
        namelb.text = xing + ming;
        wenlb.text = player.GetBasicStatus("文科").ToString();
        lilb.text = player.GetBasicStatus("理科").ToString();
        tilb.text = player.GetBasicStatus("体育").ToString();
        yilb.text = player.GetBasicStatus("艺术").ToString();
        zhailb.text = player.GetBasicStatus("宅力").ToString();
        energylb.text = player.energyPoint.ToString();
        ranklb1.text = ChineseRank(player.GetBasicStatus("排名"));
        moneylb.text = player.GetBasicStatus("金钱").ToString();
        //statuslb.text = ChineseStatus(player.GetBasicStatus("体力"));
        lengb.value = player.GetLogicStatus("冷静") / 10f;
        koub.value = player.GetLogicStatus("口才") / 10f;
        sib.value = player.GetLogicStatus("思维") / 10f;
        guanb.value = player.GetLogicStatus("观察") / 10f;
        seedlb.text = SaveLoadTool.GetKey();

        //TODO 网络统计部分
    }

    private string ChineseRank(int x)
    {
        if (x == 0)
            return "暂无考试排名，请参加全省统一测试";
        else
            return x.ToString();
    }

    private string ChineseStatus(int x)
    {
        string result;
        switch (x)
        {
            case 1:
                result = "非常好";
                break;
            case 2:
                result = "良好";
                break;
            case 3:
                result = "较好";
                break;
            case 4:
                result = "一般";
                break;
            case 5:
                result = "很差";
                break;
            default:
                return "";
        }
        return "当前状态：" + result;
    }

    /// <summary>
    /// 显示养成数值条
    /// </summary>
    /// <param name="target">对象</param>
    /// <param name="x">数值</param>
    /// <param name="max">最大值</param>
    private IEnumerator ShowBar(UIProgressBar target, int x, int max)
    {
        float value = 0;
        float final = (x + 1) / (float)max;
        //默认0-0.2颜色条
        //Sprite sp = Resources.Load<Sprite>("UI/hp");
        //if (final >= 0.5) sp = Resources.Load<Sprite>("UI/mp");
        //target.foregroundWidget.GetComponent<UI2DSprite>().sprite2D = sp;

        while (value < final)
        {
            value = Mathf.MoveTowards(value, final, final / 0.1f * Time.deltaTime);
            target.value = value;
            yield return null;
        }
    }

}
