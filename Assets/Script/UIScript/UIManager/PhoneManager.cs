using UnityEngine;
using System.Collections;
using System;
using Assets.Script.GameStruct.Model;

/**
 * PhoneManager: 
 * 整个游戏只允许一个，作为PhonePanel的组件，不能被删除
 * 控制PhonePanel下的各物件并与之交互
 * 提供方法供旗下按钮调用，并修改游戏数据
 * 实现与任何其他模块的互动，推动游戏进程
 */
public class PhoneManager : MonoBehaviour, IPanelManager
{

    private GameManager gm;

    private UILabel wenlb, lilb, tilb, yilb, zhailb;
    private UIProgressBar wenb, lib, tib, yib, zhaib;
    private UILabel ranklb, moneylb, statuslb;
    private UIProgressBar lengb, koub, sib, guanb;
    private UILabel namelb, classlb, clublb, hlb, wlb, birthlb, starlb, rlb, likelb, dislb, infolb;
    private GameObject grid;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        wenlb = transform.Find("Scroll View/Grid/Info_Container/Num_Grid/Wen_Label").gameObject.GetComponent<UILabel>();
        lilb = transform.Find("Scroll View/Grid/Info_Container/Num_Grid/Li_Label").gameObject.GetComponent<UILabel>();
        tilb = transform.Find("Scroll View/Grid/Info_Container/Num_Grid/Ti_Label").gameObject.GetComponent<UILabel>();
        yilb = transform.Find("Scroll View/Grid/Info_Container/Num_Grid/Yi_Label").gameObject.GetComponent<UILabel>();
        zhailb = transform.Find("Scroll View/Grid/Info_Container/Num_Grid/Zhai_Label").gameObject.GetComponent<UILabel>();
        wenb = transform.Find("Scroll View/Grid/Info_Container/Bar_Grid/Wen_Bar").gameObject.GetComponent<UIProgressBar>();
        lib = transform.Find("Scroll View/Grid/Info_Container/Bar_Grid/Li_Bar").gameObject.GetComponent<UIProgressBar>();
        tib = transform.Find("Scroll View/Grid/Info_Container/Bar_Grid/Ti_Bar").gameObject.GetComponent<UIProgressBar>();
        yib = transform.Find("Scroll View/Grid/Info_Container/Bar_Grid/Yi_Bar").gameObject.GetComponent<UIProgressBar>();
        zhaib = transform.Find("Scroll View/Grid/Info_Container/Bar_Grid/Zhai_Bar").gameObject.GetComponent<UIProgressBar>();
        ranklb = transform.Find("Scroll View/Grid/Info_Container/Other_Grid/Rank_Label").gameObject.GetComponent<UILabel>();
        moneylb = transform.Find("Scroll View/Grid/Info_Container/Other_Grid/Money_Label").gameObject.GetComponent<UILabel>();
        statuslb = transform.Find("Scroll View/Grid/Info_Container/Other_Grid/Status_Label").gameObject.GetComponent<UILabel>();
        lengb = transform.Find("Scroll View/Grid/Info_Container/Bar_Grid_H/Leng_Bar").gameObject.GetComponent<UIProgressBar>();
        koub = transform.Find("Scroll View/Grid/Info_Container/Bar_Grid_H/Kou_Bar").gameObject.GetComponent<UIProgressBar>();
        sib = transform.Find("Scroll View/Grid/Info_Container/Bar_Grid_H/Si_Bar").gameObject.GetComponent<UIProgressBar>();
        guanb = transform.Find("Scroll View/Grid/Info_Container/Bar_Grid_H/Guan_Bar").gameObject.GetComponent<UIProgressBar>();
        namelb = transform.Find("Scroll View/Grid/Love_Container/Name_Label").gameObject.GetComponent<UILabel>();
        classlb = transform.Find("Scroll View/Grid/Love_Container/Table/Class_Label").gameObject.GetComponent<UILabel>();
        clublb = transform.Find("Scroll View/Grid/Love_Container/Table/Club_Label").gameObject.GetComponent<UILabel>();
        hlb = transform.Find("Scroll View/Grid/Love_Container/Table/Height_Label").gameObject.GetComponent<UILabel>();
        wlb = transform.Find("Scroll View/Grid/Love_Container/Table/Weight_Label").gameObject.GetComponent<UILabel>();
        birthlb = transform.Find("Scroll View/Grid/Love_Container/Table/Birth_Label").gameObject.GetComponent<UILabel>();
        starlb = transform.Find("Scroll View/Grid/Love_Container/Table/Star_Label").gameObject.GetComponent<UILabel>();
        rlb = transform.Find("Scroll View/Grid/Love_Container/Rank_Label").gameObject.GetComponent<UILabel>();
        likelb = transform.Find("Scroll View/Grid/Love_Container/Like_Label").gameObject.GetComponent<UILabel>();
        dislb = transform.Find("Scroll View/Grid/Love_Container/Dislike_Label").gameObject.GetComponent<UILabel>();
        infolb = transform.Find("Scroll View/Grid/Love_Container/Info_Label").gameObject.GetComponent<UILabel>();
        grid = transform.Find("Scroll View/Grid").gameObject;
        CardFresh();
        LoveFresh("QButton1");
        EvidenceFresh();
    }
    //[基本信息]刷新
    public void CardFresh()
    {
        Player player = (Player)GameManager.GetGlobalVars()["玩家数据"];
        wenlb.text = player.GetBasicStatus("文科").ToString();
        lilb.text = player.GetBasicStatus("理科").ToString();
        tilb.text = player.GetBasicStatus("体育").ToString();
        yilb.text = player.GetBasicStatus("艺术").ToString();
        zhailb.text = player.GetBasicStatus("宅度").ToString();
        //ranklb.text = ChineseRank(gm.playerdata.rank);
        ranklb.text = "全省排名: " + player.GetBasicStatus("排名");
        moneylb.text = "存款: " + player.GetBasicStatus("金：钱") + " 元";
        //statuslb.text = ChineseStatus(gm.playerdata.status);
        lengb.value = player.GetLogicStatus("冷静") / 10f;
        koub.value = player.GetLogicStatus("口才") / 10f;
        sib.value = player.GetLogicStatus("思维") / 10f;
        guanb.value = player.GetLogicStatus("观察") / 10f;
        //数值条动画
        StartCoroutine(ShowBar(wenb, player.GetBasicStatus("文科")));
        StartCoroutine(ShowBar(lib, player.GetBasicStatus("理科")));
        StartCoroutine(ShowBar(tib, player.GetBasicStatus("体育")));
        StartCoroutine(ShowBar(yib, player.GetBasicStatus("艺术")));
        StartCoroutine(ShowBar(zhaib, player.GetBasicStatus("宅度")));
    }
    //[联系人]刷新
    public void LoveFresh(string str)
    {
        // TODO: 女生的属性，考虑根据json文档处理
        int x = System.Convert.ToInt32(str.Substring(7));
        //namelb.text = gm.girl[x].name;
        //classlb.text = gm.girl[x].cla;
        //clublb.text = gm.girl[x].club;
        //hlb.text = "身高：" + gm.girl[x].height.ToString() + "cm";
        //wlb.text = "体重：" + gm.girl[x].weight.ToString() + "kg";
        //birthlb.text = gm.girl[x].birth;
        //starlb.text = gm.girl[x].star;
        //rlb.text = "排名：年级" + gm.girl[x].graderank + "名 全省" + gm.girl[x].provencerank + "名";
        //likelb.text = "喜欢：" + gm.girl[x].like;
        //dislb.text = "讨厌：" + gm.girl[x].dislike;
        //infolb.text = gm.girl[x].info;
    }
    //[证据]刷新
    public void EvidenceFresh()
    {
        //载入证据
    }
    public void MoveGrid(string tabname)
    {
        if (tabname == "Card_Button")
        {
            StartCoroutine(StartMove(0));
        }
        if (tabname == "Friend_Button")
        {
            StartCoroutine(StartMove(700));
        }
        if (tabname == "Case_Button")
        {
            StartCoroutine(StartMove(1400));
        }
    }
    IEnumerator StartMove(float final)
    {
        float start = grid.transform.localPosition.y;
        float y = start;
        while (y != final)
        {
            y = Mathf.MoveTowards(y, final, Mathf.Abs(final - start) / 0.2f * Time.deltaTime);
            grid.transform.localPosition = new Vector3(0, y, 0);
            yield return null;
        }
    }
    IEnumerator ShowBar(UIProgressBar target, int x)
    {
        float value = 0;
        float t = (x + 1) / 200f;
        while (value < t)
        {
            value = Mathf.MoveTowards(value, t, t / 0.2f * Time.deltaTime);
            target.value = value;
            yield return null;
        }
    }
    string ChineseRank(int x)
    {
        if (x == 0)
            return "暂无考试排名，请参加全省统一测试";
        else
            return "当前排名是\n全省 " + x.ToString() + " 名";
    }
    string ChineseStatus(int x)
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

    public IEnumerator Open()
    {
        throw new NotImplementedException();
    }

    public IEnumerator Close()
    {
        throw new NotImplementedException();
    }
}
