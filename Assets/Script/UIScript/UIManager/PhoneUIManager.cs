using UnityEngine;
using System.Collections;
using System;
using Assets.Script.GameStruct.Model;
using System.Collections.Generic;
using Assets.Script.GameStruct;

/**
 * PhoneManager: 
 * 整个游戏只允许一个，作为PhonePanel的组件，不能被删除
 * 控制PhonePanel下的各物件并与之交互
 * 提供方法供旗下按钮调用，并修改游戏数据
 * 实现与任何其他模块的互动，推动游戏进程
 */
public class PhoneUIManager : MonoBehaviour, IPanelManager
{
    //private GameManager gm;

    private UILabel wenlb, lilb, tilb, yilb, zhailb, energylb;
    private UIProgressBar wenb, lib, tib, yib, zhaib, energyb;
    private UILabel ranklb, moneylb, statuslb;
    private UIProgressBar lengb, koub, sib, guanb;
    private UILabel namelb, classlb, clublb, hlb, wlb, birthlb, starlb, rlb, likelb, dislb, infolb;
    private GameObject grid, qtabGrid;

    private Dictionary<string, Girls> girlInfo;

    void Awake()
    {
        wenlb = transform.Find("Scroll View/Grid/Info_Container/Num_Grid/Wen_Label").gameObject.GetComponent<UILabel>();
        lilb = transform.Find("Scroll View/Grid/Info_Container/Num_Grid/Li_Label").gameObject.GetComponent<UILabel>();
        tilb = transform.Find("Scroll View/Grid/Info_Container/Num_Grid/Ti_Label").gameObject.GetComponent<UILabel>();
        yilb = transform.Find("Scroll View/Grid/Info_Container/Num_Grid/Yi_Label").gameObject.GetComponent<UILabel>();
        zhailb = transform.Find("Scroll View/Grid/Info_Container/Num_Grid/Zhai_Label").gameObject.GetComponent<UILabel>();
        energylb = transform.Find("Scroll View/Grid/Info_Container/Num_Grid/Energy_Label").gameObject.GetComponent<UILabel>();

        wenb = transform.Find("Scroll View/Grid/Info_Container/Bar_Grid/Wen_Bar").gameObject.GetComponent<UIProgressBar>();
        lib = transform.Find("Scroll View/Grid/Info_Container/Bar_Grid/Li_Bar").gameObject.GetComponent<UIProgressBar>();
        tib = transform.Find("Scroll View/Grid/Info_Container/Bar_Grid/Ti_Bar").gameObject.GetComponent<UIProgressBar>();
        yib = transform.Find("Scroll View/Grid/Info_Container/Bar_Grid/Yi_Bar").gameObject.GetComponent<UIProgressBar>();
        zhaib = transform.Find("Scroll View/Grid/Info_Container/Bar_Grid/Zhai_Bar").gameObject.GetComponent<UIProgressBar>();
        energyb = transform.Find("Scroll View/Grid/Info_Container/Bar_Grid/Energy_Bar").gameObject.GetComponent<UIProgressBar>();

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
        qtabGrid = transform.Find("Scroll View/Grid/Love_Container/QTab_Grid").gameObject;

        //SetCardInfo();
        //SetGirlInfo("");
        SetEvidence();
        SetQButton();
    }

    public void SetCardInfo()
    {
        //[基本信息]设置学生证
        Player player = Player.GetInstance();
        wenlb.text = player.GetBasicStatus("文科").ToString();
        lilb.text = player.GetBasicStatus("理科").ToString();
        tilb.text = player.GetBasicStatus("体育").ToString();
        yilb.text = player.GetBasicStatus("艺术").ToString();
        zhailb.text = player.GetBasicStatus("宅力").ToString();
        energylb.text = player.EnergyPoint.ToString();

        //ranklb.text = ChineseRank(gm.playerdata.rank);
        ranklb.text = "全省排名: " + player.GetBasicStatus("排名");
        moneylb.text = "存款: " + player.GetBasicStatus("金钱") + " 元";
        //statuslb.text = ChineseStatus(gm.playerdata.status);
        lengb.value = player.GetLogicStatus("冷静") / 10f;
        koub.value = player.GetLogicStatus("口才") / 10f;
        sib.value = player.GetLogicStatus("思维") / 10f;
        guanb.value = player.GetLogicStatus("观察") / 10f;
        //数值条动画
        StartCoroutine(ShowBar(wenb, player.GetBasicStatus("文科"), 200));
        StartCoroutine(ShowBar(lib, player.GetBasicStatus("理科"), 200));
        StartCoroutine(ShowBar(tib, player.GetBasicStatus("体育"), 200));
        StartCoroutine(ShowBar(yib, player.GetBasicStatus("艺术"), 200));
        StartCoroutine(ShowBar(zhaib, player.GetBasicStatus("宅力"), 200));
        StartCoroutine(ShowBar(energyb, player.EnergyPoint, 100));
    }

    public void SetGirlInfo(string str)
    {
        //[联系人]设置联系人信息 供按钮调用
        if (str == "") return;
        Dictionary<string, Girls> dic = DataPool.GetInstance().GetStaticVar("女主角资料表") as Dictionary<string, Girls>;
        namelb.text = dic[str].name;
        classlb.text = dic[str].cla;
        clublb.text = dic[str].club;
        hlb.text = "身高：" + dic[str].height.ToString() + "cm";
        wlb.text = "体重：" + dic[str].weight.ToString() + "kg";
        birthlb.text = dic[str].monthOfBirth.ToString() + "月" + dic[str].dayOfBirth.ToString() + "日";
        starlb.text = dic[str].horoscope;
        //rlb.text = "排名：年级" + gm.girl[x].graderank + "名 全省" + gm.girl[x].provencerank + "名";
        string tempstr = "";
        foreach (string st in dic[str].like)
        {
            tempstr += st;
        }
        likelb.text = "喜欢：" + tempstr;
        tempstr = "";
        foreach (string st in dic[str].dislike)
        {
            tempstr += st;
        }
        dislb.text = "讨厌：" + tempstr;
        infolb.text = dic[str].info;
    }

    private void SetQButton()
    {
        //[联系人]根据是否遇到了女生 调整开关
        qtabGrid.transform.Find("QButton1").gameObject.SetActive(true);
        qtabGrid.transform.Find("QButton2").gameObject.SetActive(true);
        qtabGrid.transform.Find("QButton3").gameObject.SetActive(false);
        qtabGrid.transform.Find("QButton4").gameObject.SetActive(false);
        qtabGrid.transform.Find("QButton5").gameObject.SetActive(false);
    }

    private void SetEvidence()
    {
        //初始化[证据]列表
        GameObject grid = transform.Find("Scroll View/Grid/Evidence_Container/Scroll View/Grid").gameObject;
        grid.transform.DestroyChildren();
        for (int i = 0; i < 10; i++)
        {
            GameObject eviBtn = (GameObject)Resources.Load("Prefab/EvidenceContainer");
            eviBtn = Instantiate(eviBtn) as GameObject;
            eviBtn.transform.parent = grid.transform;

            UIButton btn = eviBtn.GetComponent<UIButton>();

            PhoneEvidenceButton script = eviBtn.GetComponent<PhoneEvidenceButton>();
            script.name = "数码相机" + i;
            script.SetUIManager(this);

            UILabel enl = eviBtn.transform.Find("EvidenceName_Label").GetComponent<UILabel>();
            enl.text = "数码相机" + i;

            UI2DSprite eis = eviBtn.transform.Find("EvidenceIcon_Sprite").GetComponent<UI2DSprite>();
            Sprite sp = new Sprite();
            
            eis.sprite2D = Resources.LoadAll<Sprite>("UI/evidence3")[0];
            eis.MakePixelPerfect();
            //eis.SetRect(-150,0,100,100);

            eviBtn.GetComponent<UI2DSprite>().MakePixelPerfect();
        }
        grid.GetComponent<UIGrid>().Reposition();
    }

    public void EvidenceInfoFresh(string str)
    {
        //提供给按钮调用
        UI2DSprite image = transform.Find("Scroll View/Grid/Evidence_Container/EvidenceImage_Sprite").GetComponent<UI2DSprite>();
        UILabel info = transform.Find("Scroll View/Grid/Evidence_Container/EvidenceInfo_Label").GetComponent<UILabel>();
        image.sprite2D = (Sprite)Resources.Load("evidence3");
        image.MakePixelPerfect();
        info.text = "你点击的是证据名称为" + str;
    }

    public void MoveGrid(string tabname)
    {
        if (tabname == "Card_Button")
        {
            SetCardInfo();
            StartCoroutine(StartMove(0));
        }
        if (tabname == "Friend_Button")
        {
            SetGirlInfo("");
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
    IEnumerator ShowBar(UIProgressBar target, int x, int max)
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
