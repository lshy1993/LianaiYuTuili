using UnityEngine;
using System.Collections;
using System;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;
using System.Collections.Generic;

/// <summary>
/// EduManager: 
/// 整个游戏只允许一个，作为EduPanel的组件，不能被删除
/// 控制EduPanel下面的各部分与之交互
/// 提供方法供旗下按钮调用，并修改游戏数据
/// 实现与AVG模块的互动，推动游戏进程
/// </summary>
public class EduUIManager : MonoBehaviour
{
    private UILabel dateLabel, weekLabel, moneyLabel;
    private UILabel statusLabel, energyLabel;

    /// <summary>
    /// 养成数值标签 文理艺体宅
    /// </summary>
    private UILabel[] numLabels = new UILabel[5];

    /// <summary>
    /// 养成数值增减标签 文理艺体宅
    /// </summary>
    private UILabel[] changeLabels = new UILabel[5];
    
    /// <summary>
    /// 结算标签
    /// </summary>
    private UILabel showLabel;

    /// <summary>
    /// 提示标签 选择/属性
    /// </summary>
    private UILabel selectHintLabel, infoHintLabel;

    /// <summary>
    /// 加成数值标签
    /// </summary>
    private UILabel foreindexLabel, afterindexLabel;

    /// <summary>
    /// 养成数值条 文理艺体宅
    /// </summary>
    private UIProgressBar[] numBars = new UIProgressBar[5];

    private UIProgressBar energyBar;

    private UI2DSprite foreicon, aftericon, animate;

    private GameObject changeTable, btnTable, functionTable, scheduleContainer, acgo;
    private GameObject qspriteContainer, buttonContainer, bottomContainer;

    public SoundManager sm;
    private DataManager dm;

    private Player player;

    private DateTime date
    {
        get { return dm.GetToday(); }
    }

    private List<EduEvent> allEvents;
    //private string foreclass, afterclass, forename, aftername;
    private int forenoon, afternoon, foreindex, afterindex;

    private EduNode currentNode;

    private string[] defaultEnum = { "文科", "理科", "艺术", "体育", "宅力" };
    private string[] defaultFileName = { "wen", "li", "yi", "ti", "zhai" };

    void Awake()
    {
        dm = DataManager.GetInstance();

        dateLabel = transform.Find("Time_Container/Date_Label").GetComponent<UILabel>();
        weekLabel = transform.Find("Time_Container/Week_Label").GetComponent<UILabel>();
        moneyLabel = transform.Find("Time_Container/Money_Label").GetComponent<UILabel>();

        Transform ntr = transform.Find("NewCharaInfo_Container/Num_Grid");
        for(int i = 0; i < 5; i++)
        {
            numLabels[i] = ntr.GetChild(i).GetComponent<UILabel>();
        }

        changeTable = transform.Find("NewCharaInfo_Container/ChangeNum_Grid").gameObject;
        for (int i = 0; i < 5; i++)
        {
            changeLabels[i] = changeTable.transform.GetChild(i).GetComponent<UILabel>();
        }

        energyBar = transform.Find("NewCharaInfo_Container/EnergyStatus_Container/Energy_Bar").GetComponent<UIProgressBar>();
        statusLabel = transform.Find("NewCharaInfo_Container/EnergyStatus_Container/Status_Label").GetComponent<UILabel>();
        energyLabel = energyBar.transform.Find("Energy_Label").GetComponent<UILabel>();

        Transform tr = transform.Find("NewCharaInfo_Container/Bar_Grid");
        for(int i = 0; i < 5; i++)
        {
            numBars[i] = tr.GetChild(i).GetComponent<UIProgressBar>();
        }

        showLabel = transform.Find("BottomHint_Container/Label").gameObject.GetComponent<UILabel>();

        selectHintLabel = transform.Find("NewSelection_Container/Hint_Label").GetComponent<UILabel>();
        infoHintLabel = transform.Find("NewCharaInfo_Container/Hint_Label").GetComponent<UILabel>();

        scheduleContainer = transform.Find("Schedule_Container").gameObject;
        foreindexLabel = scheduleContainer.transform.Find("Fore_Container/ForeIndex_Label").GetComponent<UILabel>();
        afterindexLabel = scheduleContainer.transform.Find("After_Container/AfterIndex_Label").GetComponent<UILabel>();
        foreicon = scheduleContainer.transform.Find("Fore_Container/ForeIcon_Sprite").GetComponent<UI2DSprite>();
        aftericon = scheduleContainer.transform.Find("After_Container/AfterIcon_Sprite").GetComponent<UI2DSprite>();

        qspriteContainer = transform.Find("NewSelection_Container/QSprite_Container").gameObject;
        buttonContainer = transform.Find("NewSelection_Container/Button_Container").gameObject;
        bottomContainer = transform.Find("BottomHint_Container").gameObject;

        functionTable = buttonContainer.transform.Find("Function_Container").gameObject;
        btnTable = buttonContainer.transform.Find("Grid").gameObject;

        acgo = qspriteContainer.transform.Find("Container").gameObject;
        animate = qspriteContainer.transform.Find("Animate_Sprite").GetComponent<UI2DSprite>();

        SetEduButton();
    }

    void OnEnable()
    {
        player = dm.gameData.player;
        forenoon = dm.gameData.morningSchedule;
        afternoon = dm.gameData.afternoonSchedule;
        foreindex = (int)dm.gameData.morningRate;
        afterindex = (int)dm.gameData.afternoonRate;
        //TODO:加上对节日的判断
        if (date.Month == 8 && date.Day == 31)
        {
            //8.31日教学关关闭function
            functionTable.SetActive(false);
        }
        else
        {
            functionTable.SetActive(true);
        }
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

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    private void SetBGM()
    {
        //TODO:根据游戏时间播放不同的音乐
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
    /// 养成按钮悬停提示
    /// </summary>
    /// <param name="x">参数，-3闲逛，-2休息，-1空白</param>
    public void SetHelp(int x)
    {
        string result = string.Empty;
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
        selectHintLabel.text = result;
    }

    /// <summary>
    /// 养成信息悬停提示
    /// </summary>
    /// <param name="x">参数-1空白-2体力-3状态</param>
    public void SetInfoHelp(int x)
    {
        string result = string.Empty;
        if(x == -2)
        {
            result += "体力：" + JudgeEnergy();
        }
        else if (x == -3)
        {
            result += "学习状态：" + JudgeStatus();
        }
        else if (x != -1)
        {
            result += defaultEnum[x] + "：" + JudgeScore(player.GetBasicStatus(defaultEnum[x]));
        }
        infoHintLabel.text = result;
    }
    //评价成绩
    private string JudgeScore(int x)
    {
        string str = x.ToString();
        if (x > 430) return str + "  顶尖的水准！";//S
        if (x >= 360) return str + "  非常优异的成绩！";//A
        if (x >= 290) return str + "  优秀的成绩！";//B
        if (x >= 220) return str + "  良好的成绩！";//C
        if (x >= 150) return str + "  合格了！！";//D
        if (x >= 90) return str + "  勉勉强强的成绩！";//E
        return str + "  还需要加把劲！";//F
    }
    private string JudgeEnergy()
    {
        string str = player.energyPoint.ToString();
        if (player.energyPoint >= 100) return str += "体力充沛！尽情享受吧！";
        if(player.energyPoint >= 50) return str += "注意：学习的效率会随着体力而变化！";
        return str += "体力不支，请及时休息！";
    }
    private string JudgeStatus()
    {
        if(player.studyStatus == -1)
        {
            return "消极  节省体力，但是效果偏差！";
        }
        if (player.studyStatus == 0)
        {
            return "普通  体力和效果没有什么变化！";
        }
        if (player.studyStatus == 1)
        {
            return "积极  效果较好，但耗费更多的体力！";
        }
        return string.Empty;
    }

    /// <summary>
    /// 刷新界面
    /// </summary>
    private void UIFresh()
    {
        //属性值显示
        dateLabel.text = date.Month + "月" + date.Day + "日";
        weekLabel.text = Constants.WEEK_DAYS[Convert.ToInt16(date.DayOfWeek)];
        moneyLabel.text = "金钱: " + player.GetBasicStatus("金钱");

        for(int i = 0; i < 5; i++)
        {
            numLabels[i].text = player.GetBasicStatus(defaultEnum[i]).ToString();
            numBars[i].value = player.GetBasicStatus(defaultEnum[i]) / 500f;
        }
        energyLabel.text = player.energyPoint.ToString();
        if (player.studyStatus == 1)
        {
            statusLabel.text = "积极";
        }
        else if (player.studyStatus == -1)
        {
            statusLabel.text = "消极";
        }
        else
        {
            statusLabel.text = "普通";
        }
        energyBar.value = player.energyPoint / 150f;

        //课表加成的显示 假日的判定
        if (!DataManager.GetInstance().IsHoliday())
        {
            foreindexLabel.text = "加成" + foreindex.ToString("0.0");
            afterindexLabel.text = "加成" + afterindex.ToString("0.0");
            foreicon.sprite2D = Resources.Load<Sprite>("UI/icon_" + defaultFileName[forenoon]);
            aftericon.sprite2D = Resources.Load<Sprite>("UI/icon_" + defaultFileName[afternoon]);
        }
        else
        {
            scheduleContainer.SetActive(false);
        }

    }

    /// <summary>
    /// 休息按钮的触发操作
    /// </summary>
    public void RelaxExecute()
    {
        //由体力决定成功率
        float successrate = player.energyPoint * 0.9f;
        //是否成功计算
        float seed = UnityEngine.Random.Range(0, 1);
        bool success = seed < successrate;
        //系数1 失败则减半
        float index1 = success ? 1f : 0.5f;
        //体力恢复初始范围
        float delta = UnityEngine.Random.Range(5, 10);
        float final = delta * index1;
        int energyGet = (int)Math.Round(final);
        //动画显示（养成Q版与数值跳跃）
        StartCoroutine(ShowResult(energyGet, success));
        player.AddEnergy(energyGet);
    }

    /// <summary>
    /// 养成按钮触发
    /// </summary>
    /// <param name="x">养成按钮编号</param>
    public void Execute(int x)
    {
        Debug.Log("你选择了" + allEvents[x].name);
        EduEvent selectEvent = allEvents[x];
        string foreclass = defaultEnum[forenoon];
        string afterclass = defaultEnum[afternoon];
        //由体力决定成功率
        float successrate = player.energyPoint * 0.9f;
        //符合课表 增加成功率
        if (selectEvent.name.Contains(foreclass) 
            || selectEvent.name.Contains(afterclass))
        {
            successrate += 0.1f;
        }
        float seed = UnityEngine.Random.Range(0f, 1f);
        bool success = seed < successrate;
        //系数1 成功则1.5倍 失败减半
        float index1 = success ? 1.51f : 0.5f;
        //系数2 课表加成
        float index2 = 1f;
        //TODO:假日的判定有课才加倍？
        if (!DataManager.GetInstance().IsHoliday())
        {
            if (selectEvent.name.Contains(foreclass)) index2 = foreindex;
            if (selectEvent.name.Contains(afterclass)) index2 = afterindex;
        }
        //执行各项属性值 加倍
        int[] change = new int[5] { 0, 0, 0, 0, 0 };
        foreach (KeyValuePair<string, EduStatistic> kv in selectEvent.statistic)
        {
            EduStatistic es = kv.Value;
            if (es == null) continue;
            //养成初始区间内随机值
            float final = UnityEngine.Random.Range(es.min, es.max);
            //正数 才乘以系数
            if (final > 0)
            {
                final *= index1;
                if (kv.Key == foreclass || kv.Key == afterclass) final *= index2;
            }
            int i = Array.IndexOf(defaultEnum, kv.Key);
            change[i] = (int)Math.Round(final);
        }
        int energyCost = selectEvent.ap;
        //动画显示（养成Q版与数值跳跃）
        StartCoroutine(ShowResult(selectEvent.name, change, energyCost, success));
        //数值更新
        for(int i = 0; i < 5; i++)
        {
            player.AddBasicStatus(defaultEnum[i], change[i]);
        }
        player.AddEnergy(energyCost);
    }

    private IEnumerator ShowResult(string name, int[] change, int cost, bool success = false)
    {
        //关闭选项框
        buttonContainer.SetActive(false);
        //打开动画窗
        qspriteContainer.SetActive(true);
        animate.gameObject.SetActive(true);
        selectHintLabel.text = string.Empty;
        //帧动画文件名
        if (name.Contains("文科")) name = "wen";
        if (name.Contains("理科")) name = "li";
        if (name.Contains("艺术")) name = "yi";
        if (name.Contains("体育")) name = "ti";
        if (name.Contains("动漫")) name = "zhai";
        //等待3s动画
        int i = 0;
        while (i < 9)
        {
            animate.sprite2D = Resources.Load<Sprite>("UI/Q_" + name + "_" + i % 3);
            i++;
            yield return new WaitForSeconds(0.25f);
        }
        StartCoroutine(ShowChangeNum(change));
        string fileName = "UI/Q_" + name + (success ? "_o" : "_x");
        animate.sprite2D = Resources.Load<Sprite>(fileName);
        //结算的文字
        selectHintLabel.text = "消耗体力" + (-cost).ToString() + "\n请点击任意地方进入下一天";
        showLabel.text = "结算显示：成功" + (success ? "1.5倍！" : "0.5倍");
        for(int j = 0; j < 5; j++)
        {
            if (change[j] != 0)
            {
                showLabel.text += "  " + defaultEnum[j] + "：" + change[j];
            }
        }
        StartCoroutine(ShowHintLabel());
        //等待点击
        acgo.SetActive(true);
        UIFresh();
    }

    //休息时的Q版动作
    private IEnumerator ShowResult(int cost, bool success = false)
    {
        buttonContainer.SetActive(false);
        qspriteContainer.SetActive(true);
        animate.gameObject.SetActive(true);
        selectHintLabel.text = string.Empty;
        int i = 0;
        while (i < 9)
        {
            animate.sprite2D = (Sprite)Resources.Load<Sprite>("UI/Q_re_" + i % 3);
            i++;
            yield return new WaitForSeconds(0.25f);
        }
        string fileName = "UI/Q_re" + (success ? "_o" : "_x");
        animate.sprite2D = Resources.Load<Sprite>(fileName);
        //结算文字
        selectHintLabel.text = "恢复活力" + cost.ToString() + "\n请点击任意地方进入下一天";
        showLabel.text = "结算显示：成功" + (success ? "1.5倍！" : "0.5倍！");
        StartCoroutine(ShowHintLabel());
        //等待点击
        acgo.SetActive(true);
        UIFresh();
    }

    private IEnumerator ShowHintLabel()
    {
        float t = 0, y;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.2f * Time.deltaTime);
            y = -580 + 80 * t;
            bottomContainer.transform.localPosition = new Vector3(0,y);
            yield return null;
        }
    }

    private IEnumerator ShowChangeNum(int[] change, int cost = 0)
    {
        for(int i = 0; i < 5; i++)
        {
            changeLabels[i].color = change[i] > 0 ? Color.green : Color.red;
            changeLabels[i].text = ChangeToString(change[i]);
        }
        changeTable.SetActive(true);
        float t = 0, y;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.2f * Time.deltaTime);
            y = -65 + 10 * t;
            changeTable.transform.localPosition = new Vector3(370, y);
            for(int i = 0; i < 5; i++)
            {
                changeLabels[i].alpha = t;
            }
            yield return null;
        }
    }
    private string ChangeToString(int x)
    {
        if (x > 0) return "+" + x.ToString();
        if (x < 0) return x.ToString();
        return "";
    }

    /// <summary>
    /// 结束养成 进入下一天
    /// </summary>
    public void NextDay()
    {
        //供调试调用
        acgo.SetActive(false);
        currentNode.EduExit();
        changeTable.SetActive(false);
        //spriteContainer.SetActive(false);
        //animate.sprite2D = null;
        //showlabel.text = "";
        //animate.gameObject.SetActive(false);
    }

    /// <summary>
    /// 进入地图选择模式
    /// </summary>
    public void ReturnMap()
    {
        currentNode.ReturnMap();
    }
}
