using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;
using System.Collections;

public class SelfUIManager : MonoBehaviour
{
    private UILabel wenlb, lilb, tilb, yilb, zhailb, energylb;
    private UIProgressBar wenb, lib, tib, yib, zhaib, energyb;
    private UILabel namelb, ranklb, moneylb, statuslb;
    private UIProgressBar lengb, koub, sib, guanb;

    private Player player;

    private void Awake()
    {
        wenlb = transform.Find("Num_Grid/Wen_Label").gameObject.GetComponent<UILabel>();
        lilb = transform.Find("Num_Grid/Li_Label").gameObject.GetComponent<UILabel>();
        tilb = transform.Find("Num_Grid/Ti_Label").gameObject.GetComponent<UILabel>();
        yilb = transform.Find("Num_Grid/Yi_Label").gameObject.GetComponent<UILabel>();
        zhailb = transform.Find("Num_Grid/Zhai_Label").gameObject.GetComponent<UILabel>();
        energylb = transform.Find("Num_Grid/Energy_Label").gameObject.GetComponent<UILabel>();

        wenb = transform.Find("Bar_Grid/Wen_Bar").gameObject.GetComponent<UIProgressBar>();
        lib = transform.Find("Bar_Grid/Li_Bar").gameObject.GetComponent<UIProgressBar>();
        tib = transform.Find("Bar_Grid/Ti_Bar").gameObject.GetComponent<UIProgressBar>();
        yib = transform.Find("Bar_Grid/Yi_Bar").gameObject.GetComponent<UIProgressBar>();
        zhaib = transform.Find("Bar_Grid/Zhai_Bar").gameObject.GetComponent<UIProgressBar>();
        energyb = transform.Find("Bar_Grid/Energy_Bar").gameObject.GetComponent<UIProgressBar>();

        namelb = transform.Find("BasicInfo_Grid/Name_Label").gameObject.GetComponent<UILabel>();
        ranklb = transform.Find("Other_Grid/Rank_Label").gameObject.GetComponent<UILabel>();
        moneylb = transform.Find("Other_Grid/Money_Label").gameObject.GetComponent<UILabel>();
        statuslb = transform.Find("Other_Grid/Status_Label").gameObject.GetComponent<UILabel>();
        lengb = transform.Find("Bar_Grid_H/Leng_Bar").gameObject.GetComponent<UIProgressBar>();
        koub = transform.Find("Bar_Grid_H/Kou_Bar").gameObject.GetComponent<UIProgressBar>();
        sib = transform.Find("Bar_Grid_H/Si_Bar").gameObject.GetComponent<UIProgressBar>();
        guanb = transform.Find("Bar_Grid_H/Guan_Bar").gameObject.GetComponent<UIProgressBar>();

    }

    private void OnEnable()
    {
        player = DataManager.GetInstance().GetGameVar<Player>("玩家");
        SetCardInfo();
        //数值条动画
        StartCoroutine(ShowBar(wenb, player.GetBasicStatus("文科"), 200));
        StartCoroutine(ShowBar(lib, player.GetBasicStatus("理科"), 200));
        StartCoroutine(ShowBar(tib, player.GetBasicStatus("体育"), 200));
        StartCoroutine(ShowBar(yib, player.GetBasicStatus("艺术"), 200));
        StartCoroutine(ShowBar(zhaib, player.GetBasicStatus("宅力"), 200));
        StartCoroutine(ShowBar(energyb, player.energyPoint, 100));
    }

    private void SetCardInfo()
    {
        //[基本信息]设置学生证
        namelb.text = DataManager.GetInstance().GetGameVar<string>("姓") + DataManager.GetInstance().GetGameVar<string>("名");
        wenlb.text = player.GetBasicStatus("文科").ToString();
        lilb.text = player.GetBasicStatus("理科").ToString();
        tilb.text = player.GetBasicStatus("体育").ToString();
        yilb.text = player.GetBasicStatus("艺术").ToString();
        zhailb.text = player.GetBasicStatus("宅力").ToString();
        energylb.text = player.energyPoint.ToString();
        ranklb.text = ChineseRank(player.GetBasicStatus("排名"));
        moneylb.text = "存款: " + player.GetBasicStatus("金钱") + " 元";
        //statuslb.text = ChineseStatus(player.GetBasicStatus("体力"));
        lengb.value = player.GetLogicStatus("冷静") / 10f;
        koub.value = player.GetLogicStatus("口才") / 10f;
        sib.value = player.GetLogicStatus("思维") / 10f;
        guanb.value = player.GetLogicStatus("观察") / 10f;
    }

    private string ChineseRank(int x)
    {
        if (x == 0)
            return "暂无考试排名，请参加全省统一测试";
        else
            return "当前排名是\n全省 " + x.ToString() + " 名";
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

    private IEnumerator ShowBar(UIProgressBar target, int x, int max)
    {
        float value = 0;
        float t = (x + 1) / (float)max;
        while (value < t)
        {
            value = Mathf.MoveTowards(value, t, t / 0.3f * Time.deltaTime);
            target.value = value;
            yield return null;
        }
    }

}
