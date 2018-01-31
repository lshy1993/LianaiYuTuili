using UnityEngine;
using System.Collections;
using System;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;
using System.Collections.Generic;
//using Assets.Script.UIScript;

/**
 * EduManager: 
 * 整个游戏只允许一个，作为EduPanel的组件，不能被删除
 * 控制EduPanel下面的各部分与之交互
 * 提供方法供旗下按钮调用，并修改游戏数据
 * 实现与AVG模块的互动，推动游戏进程
 */
public class EduUIManager : MonoBehaviour
{
    private UILabel daylabel, datelabel, moneylabel;
    private UILabel wenlabel, lilabel, tilabel, yilabel, zhailabel, energylabel;
    private UILabel showlabel, helplabel, foreindexlabel, afterindexlabel;
    private UIProgressBar wenbar, libar, tibar, yibar, zhaibar, energybar;
    private UI2DSprite foreicon, aftericon, animate;

    private GameObject functionContainer, scheduleContainer, acgo;
    private GameObject btnTable, spriteContainer, buttonContainer;

    public SoundManager sm;
    private DataManager dm;

    private Player player;

    private DateTime date
    {
        get
        {
            return dm.GetToday();
        }
    }

    private List<EduEvent> allEvents;
    private string foreclass, afterclass, forename, aftername;
    private int foreindex, afterindex;

    private EduNode currentNode;


    private string[] defaultSchedule = { "文科", "理科", "艺术", "体育" };
    private string[] defaultFileName = { "wen", "li", "yi", "ti" };

    void Awake()
    {
        dm = DataManager.GetInstance();

        daylabel = transform.Find("Time_Container/Day_Label").gameObject.GetComponent<UILabel>();
        datelabel = transform.Find("Time_Container/Date_Label").gameObject.GetComponent<UILabel>();
        moneylabel = transform.Find("Time_Container/Money_Label").gameObject.GetComponent<UILabel>();

        wenlabel = transform.Find("NewCharaInfo_Container/Num_Grid/Wen_Label").gameObject.GetComponent<UILabel>();
        lilabel = transform.Find("NewCharaInfo_Container/Num_Grid/Li_Label").gameObject.GetComponent<UILabel>();
        tilabel = transform.Find("NewCharaInfo_Container/Num_Grid/Ti_Label").gameObject.GetComponent<UILabel>();
        yilabel = transform.Find("NewCharaInfo_Container/Num_Grid/Yi_Label").gameObject.GetComponent<UILabel>();
        zhailabel = transform.Find("NewCharaInfo_Container/Num_Grid/Zhai_Label").gameObject.GetComponent<UILabel>();
        energylabel = transform.Find("NewCharaInfo_Container/Num_Grid/Energy_Label").gameObject.GetComponent<UILabel>();

        wenbar = transform.Find("NewCharaInfo_Container/Bar_Grid/Wen_Bar").gameObject.GetComponent<UIProgressBar>();
        libar = transform.Find("NewCharaInfo_Container/Bar_Grid/Li_Bar").gameObject.GetComponent<UIProgressBar>();
        tibar = transform.Find("NewCharaInfo_Container/Bar_Grid/Ti_Bar").gameObject.GetComponent<UIProgressBar>();
        yibar = transform.Find("NewCharaInfo_Container/Bar_Grid/Yi_Bar").gameObject.GetComponent<UIProgressBar>();
        zhaibar = transform.Find("NewCharaInfo_Container/Bar_Grid/Zhai_Bar").gameObject.GetComponent<UIProgressBar>();
        energybar = transform.Find("NewCharaInfo_Container/Bar_Grid/Energy_Bar").gameObject.GetComponent<UIProgressBar>();

        helplabel = transform.Find("NewSelection_Container/Help_Label").GetComponent<UILabel>();

        scheduleContainer = transform.Find("Schedule_Container").gameObject;
        foreindexlabel = scheduleContainer.transform.Find("Fore_Container/ForeIndex_Label").GetComponent<UILabel>();
        afterindexlabel = scheduleContainer.transform.Find("After_Container/AfterIndex_Label").GetComponent<UILabel>();
        foreicon = scheduleContainer.transform.Find("Fore_Container/ForeIcon_Sprite").GetComponent<UI2DSprite>();
        aftericon = scheduleContainer.transform.Find("After_Container/AfterIcon_Sprite").GetComponent<UI2DSprite>();

        spriteContainer = transform.Find("NewSelection_Container/QSprite_Container").gameObject;
        buttonContainer = transform.Find("NewSelection_Container/Button_Container").gameObject;

        functionContainer = buttonContainer.transform.Find("Function_Container").gameObject;
        btnTable = buttonContainer.transform.Find("Grid").gameObject;

        acgo = spriteContainer.transform.Find("Container").gameObject;
        showlabel = spriteContainer.transform.Find("Show_Label").gameObject.GetComponent<UILabel>();
        animate = spriteContainer.transform.Find("Animate_Sprite").gameObject.GetComponent<UI2DSprite>();

        SetEduButton();
    }

    void OnEnable()
    {
        player = dm.gameData.player;
        int forenoon = dm.gameData.morningSchedule;
        int afternoon = dm.gameData.afternoonSchedule;
        foreclass = defaultSchedule[forenoon];
        afterclass = defaultSchedule[afternoon];
        forename = defaultFileName[forenoon];
        aftername = defaultFileName[afternoon];
        foreindex = (int)dm.gameData.morningRate;
        afterindex = (int)dm.gameData.afternoonRate;
        //TODO:加上对节日的判断
        if (date.Month == 8 && date.Day == 31)
        {
            //8.31日教学关关闭function
            functionContainer.SetActive(false);
        }
        else
        {
            functionContainer.SetActive(true);
        }
        //根据时间播放音乐
        SetBGM();
        UIFresh();
    }

    public void SetEduNode(EduNode node)
    {
        this.currentNode = node;
    }

    public void SetEduEvent(List<EduEvent> es)
    {
        this.allEvents = es;
    }

    private void SetBGM()
    {
        string ptr = "watashi-time";
        sm.SetBGM(ptr);
    }

    private void SetEduButton()
    {
        //btnTable.transform.DestroyChildren();
        for (int i = 0; i < 5; i++)
        {
            //GameObject eduBtn = Resources.Load("Prefab/Edu_Choice") as GameObject;
            //eduBtn = NGUITools.AddChild(btnTable, eduBtn);

            //UIButton btn = eduBtn.GetComponent<UIButton>();
            //btn.normalSprite2D = Resources.Load<Sprite>("but_place_n");
            //btn.hoverSprite2D = Resources.Load<Sprite>("but_place_o");
            //btn.pressedSprite2D = Resources.Load<Sprite>("but_place_d");
            //eduBtn 

            EduButton script = btnTable.transform.GetChild(i).GetComponent<EduButton>();
            script.eduID = i;
            script.SetUIManager(this);
        }

        btnTable.GetComponent<UIGrid>().Reposition();
    }

    /// <summary>
    /// 提示鼠标悬停
    /// </summary>
    /// <param name="x">参数，-3闲逛，-2休息，-1空白</param>
    public void SetHelp(int x)
    {
        string result = "";
        if (x == -3)
        {
            result = "在校园内闲逛";
        }
        else if (x == -2)
        {
            result = "休息，回复一定的体力";
        }
        else if (x == -1)
        {
            result = "请选择想要执行的任务";
        }
        else
        {
            result += allEvents[x].name + " 熟练度等级：" + new string('★', allEvents[x].level);
            result += "\n消耗活力：" + -allEvents[x].ap;
        }
        helplabel.text = result;
    }

    /// <summary>
    /// 刷新界面
    /// </summary>
    private void UIFresh()
    {
        //属性值显示
        daylabel.text = date.Month + "月" + date.Day + "日";
        datelabel.text = Constants.WEEK_DAYS[Convert.ToInt16(date.DayOfWeek)];
        moneylabel.text = "金钱: " + player.GetBasicStatus("金钱");

        wenlabel.text = player.GetBasicStatus("文科").ToString();
        lilabel.text = player.GetBasicStatus("理科").ToString();
        yilabel.text = player.GetBasicStatus("艺术").ToString();
        tilabel.text = player.GetBasicStatus("体育").ToString();
        zhailabel.text = player.GetBasicStatus("宅力").ToString();
        energylabel.text = player.energyPoint.ToString();

        SetBarValue(wenbar, player.GetBasicStatus("文科") / 200f);
        SetBarValue(libar, player.GetBasicStatus("理科") / 200f);
        SetBarValue(yibar, player.GetBasicStatus("艺术") / 200f);
        SetBarValue(tibar, player.GetBasicStatus("体育") / 200f);
        SetBarValue(zhaibar, player.GetBasicStatus("宅力") / 200f);
        SetBarValue(energybar, player.energyPoint / 150f);

        //课表加成的显示 假日的判定
        if (!DataManager.GetInstance().IsHoliday())
        {
            foreindexlabel.text = "加成" + foreindex.ToString("0.0");
            afterindexlabel.text = "加成" + afterindex.ToString("0.0");
            foreicon.sprite2D = Resources.Load<Sprite>("UI/icon_" + forename);
            aftericon.sprite2D = Resources.Load<Sprite>("UI/icon_" + aftername);
        }
        else
        {
            scheduleContainer.SetActive(false);
        }

    }

    /// <summary>
    /// 设置数值条
    /// </summary>
    /// <param name="target">对象</param>
    /// <param name="value">数值</param>
    private void SetBarValue(UIProgressBar target, float value)
    {
        //默认0-0.2颜色条
        Sprite sp = Resources.Load<Sprite>("UI/edu_bar_1");
        if (value >= 0.8)
            sp = Resources.Load<Sprite>("UI/edu_bar_5");
        else if (value >= 0.6)
            sp = Resources.Load<Sprite>("UI/edu_bar_4");
        else if (value >= 0.4)
            sp = Resources.Load<Sprite>("UI/edu_bar_3");
        else if (value >= 0.2)
            sp = Resources.Load<Sprite>("UI/edu_bar_2");

        target.foregroundWidget.GetComponent<UI2DSprite>().sprite2D = sp;
        target.value = value;
    }

    /// <summary>
    /// 休息按钮的触发操作
    /// </summary>
    public void RelaxExecute()
    {
        //由体力决定成功率（体力即成功率）
        float successrate = player.energyPoint * 0.9f;
        //是否成功计算
        float seed = UnityEngine.Random.Range(0, 1);
        bool success = seed < successrate;
        //系数1
        float index1 = success ? 1f : 0.5f;
        float delta = UnityEngine.Random.Range(5, 10);
        float final = delta * index1;
        int energyGet = (int)final;
        StartCoroutine(ShowResult(energyGet, success));
        player.AddEnergy(energyGet);
    }

    /// <summary>
    /// 养成按钮得触发
    /// </summary>
    /// <param name="x">养成按钮编号</param>
    public void Execute(int x)
    {
        Debug.Log("你选择了" + allEvents[x].name);
        //执行计算并更改
        EduEvent selectEvent = allEvents[x];
        //由体力决定成功率（体力即成功率）
        float successrate = player.energyPoint * 0.9f;
        if (selectEvent.name.Contains(foreclass) || selectEvent.name.Contains(afterclass))
        {
            successrate += 0.1f;
        }
        //成功则加倍 失败减半
        float seed = UnityEngine.Random.Range(0f, 1f);
        bool success = seed < successrate;
        //系数1 成功
        float index1 = success ? 1.5f : 0.5f;
        //系数2 课表加成
        float index2 = 1f;
        //TODO:假日的判定有课才加倍
        if (!DataManager.GetInstance().IsHoliday())
        {
            if (selectEvent.name.Contains(foreclass)) index2 = foreindex;
            if (selectEvent.name.Contains(afterclass)) index2 = afterindex;
        }
        //执行各项属性值 加倍
        Dictionary<string, int> change = new Dictionary<string, int>();
        foreach (KeyValuePair<string, EduStatistic> kv in selectEvent.statistic)
        {
            if (kv.Value != null)
            {
                //养成区间内随机值
                float final = UnityEngine.Random.Range(kv.Value.min, kv.Value.max);
                //正数 才乘以系数
                if (final > 0)
                {
                    final *= index1;
                    if (kv.Key == foreclass || kv.Key == afterclass) final *= index2;
                }
                change.Add(kv.Key, (int)final);
            }
        }
        int energyCost = selectEvent.ap;
        //养成Q版动画显示
        StartCoroutine(ShowResult(selectEvent.name, change, energyCost, success));
        //数值更新
        foreach (KeyValuePair<string, int> kv in change)
        {
            player.AddBasicStatus(kv.Key, kv.Value);
        }
        player.AddEnergy(energyCost);
    }


    private IEnumerator ShowResult(string name, Dictionary<string, int> change, int cost, bool success = false)
    {
        buttonContainer.SetActive(false);
        spriteContainer.SetActive(true);
        animate.gameObject.SetActive(true);

        if (name.Contains("文科")) name = "wen";
        if (name.Contains("理科")) name = "li";
        if (name.Contains("艺术")) name = "yi";
        if (name.Contains("体育")) name = "ti";
        if (name.Contains("动漫")) name = "zhai";

        int i = 0;
        while (i < 9)
        {
            animate.sprite2D = (Sprite)Resources.Load<Sprite>("UI/Q_" + name + "_" + i % 3);
            i++;
            yield return new WaitForSeconds(0.25f);
        }
        if (success)
        {
            animate.sprite2D = (Sprite)Resources.Load<Sprite>("UI/Q_" + name + "_o");
            showlabel.text = "结算显示：成功1.5倍！\r\n";
        }
        else
        {
            animate.sprite2D = (Sprite)Resources.Load<Sprite>("UI/Q_" + name + "_x");
            showlabel.text = "结算显示：失败0.5倍！\r\n";
        }
        foreach (KeyValuePair<string, int> kv in change)
        {
            showlabel.text += kv.Key + "：" + kv.Value + "  ";
        }
        showlabel.text += "消耗活力" + (-cost).ToString() + "点";
        showlabel.text += "\r\n请点击任意地方进入下一天";
        acgo.SetActive(true);
        UIFresh();
    }

    //休息时的Q版动作
    private IEnumerator ShowResult(int cost, bool success = false)
    {
        buttonContainer.SetActive(false);
        spriteContainer.SetActive(true);
        animate.gameObject.SetActive(true);
        int i = 0;
        while (i < 9)
        {
            animate.sprite2D = (Sprite)Resources.Load<Sprite>("UI/Q_re_" + i % 3);
            i++;
            yield return new WaitForSeconds(0.25f);
        }
        if (success)
        {
            animate.sprite2D = (Sprite)Resources.Load<Sprite>("UI/Q_re_o");
            showlabel.text = "结算显示：成功1.5倍！\r\n";
        }
        else
        {
            animate.sprite2D = (Sprite)Resources.Load<Sprite>("UI/Q_re_x");
            showlabel.text = "结算显示：失败0.5倍！\r\n";
        }
        acgo.SetActive(true);
        UIFresh();
        showlabel.text = "恢复活力" + cost;
        showlabel.text += "\r\n请点击任意地方进入下一天";
    }

    public void NextDay()
    {
        //供调试调用
        acgo.SetActive(false);
        currentNode.EduExit();
        //spriteContainer.SetActive(false);
        //animate.sprite2D = null;
        //showlabel.text = "";
        //animate.gameObject.SetActive(false);
    }

    public void ReturnMap()
    {
        currentNode.ReturnMap();
    }
}
