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
    private UI2DSprite foreicon, aftericon;

    private GameObject spriteContainer, selectionContainer, functionContainer, acgo;
    private GameObject btnTable;

    private Player player;
    //private Hashtable gVars;
    private DateTime date;

    private List<EduEvent> allEvents;
    private string foreclass, afterclass;
    private int foreindex, afterindex;

    private EduNode currentNode;


    private string[] defaultSchedule = { "文科", "理科", "艺术", "体育" };

    void Awake()
    {
        daylabel = transform.Find("Time_Container/Day_Label").gameObject.GetComponent<UILabel>();
        datelabel = transform.Find("Time_Container/Date_Label").gameObject.GetComponent<UILabel>();
        moneylabel = transform.Find("Time_Container/Money_Label").gameObject.GetComponent<UILabel>();
        wenlabel = transform.Find("CharaInfo_Container/Num_Grid/Wen_Label").gameObject.GetComponent<UILabel>();
        lilabel = transform.Find("CharaInfo_Container/Num_Grid/Li_Label").gameObject.GetComponent<UILabel>();
        tilabel = transform.Find("CharaInfo_Container/Num_Grid/Ti_Label").gameObject.GetComponent<UILabel>();
        yilabel = transform.Find("CharaInfo_Container/Num_Grid/Yi_Label").gameObject.GetComponent<UILabel>();
        zhailabel = transform.Find("CharaInfo_Container/Num_Grid/Zhai_Label").gameObject.GetComponent<UILabel>();
        energylabel = transform.Find("CharaInfo_Container/Num_Grid/Energy_Label").gameObject.GetComponent<UILabel>();

        helplabel = transform.Find("Selection_Container/Left_Container/Help_Label").GetComponent<UILabel>();
        foreindexlabel = transform.Find("Selection_Container/Right_Container/Fore_Container/ForeIndex_Label").GetComponent<UILabel>();
        afterindexlabel = transform.Find("Selection_Container/Right_Container/After_Container/AfterIndex_Label").GetComponent<UILabel>();
        foreicon = transform.Find("Selection_Container/Right_Container/Fore_Container/ForeIcon_Sprite").GetComponent<UI2DSprite>();
        aftericon = transform.Find("Selection_Container/Right_Container/After_Container/AfterIcon_Sprite").GetComponent<UI2DSprite>();

        spriteContainer = transform.Find("QSprite_Container").gameObject;
        selectionContainer = transform.Find("Selection_Container").gameObject;
        functionContainer = transform.Find("Function_Container").gameObject;
        acgo = transform.Find("QSprite_Container/Container").gameObject;

        showlabel = transform.Find("QSprite_Container/Show_Label").gameObject.GetComponent<UILabel>();

        btnTable = transform.Find("Selection_Container/Left_Container/Grid").gameObject;
        SetEduButton();
        //UIFresh();
    }

    void OnEnable()
    {
        //player = Player.GetInstance();
        //gVars = DataManager.GetInstance().GetGameVars();
        //date = (DateTime)gVars["日期"];
        player = DataManager.GetInstance().GetGameVar<Player>("玩家");
        int turn = DataManager.GetInstance().GetGameVar<int>("回合");
        date = DataManager.START_DAY.AddDays(turn);
        //TODO:加上对节日的判断
        if (date.Month == 9 && date.Day == 1)
        {
            //9.1日教学关关闭function
            functionContainer.SetActive(false);
        }
        functionContainer.SetActive(true);
        UIFresh();
    }

    public void SetEduNode(EduNode node)
    {
        this.currentNode = node;
    }

    public void SetEduEvent(List<EduEvent> es)
    {
        this.allEvents = es;
        player = DataManager.GetInstance().GetGameVar<Player>("玩家");
        int turn = DataManager.GetInstance().GetGameVar<int>("回合");
        date = DataManager.START_DAY.AddDays(turn);
        UIFresh();
    }

    public void SetRandomSchedule()
    {
        //随机生成2个课表 显示在UI上 并且随机生成加成值
        int forenoon = (int)DataPool.GetInstance().GetGameVar("上午课程");
        int afternoon = (int)DataPool.GetInstance().GetGameVar("下午课程");
        foreclass = defaultSchedule[forenoon];
        afterclass = defaultSchedule[afternoon];
        foreindex = (int)DataPool.GetInstance().GetGameVar("上午指数");
        afterindex = (int)DataPool.GetInstance().GetGameVar("下午指数");
    }

    public void SetEduButton()
    {
        btnTable.transform.DestroyChildren();
        for (int i = 0; i < 5; i++)
        {
            GameObject eduBtn = (GameObject)Resources.Load("Prefab/Edu_Choice");
            //NGUITools.AddChild(btnTable, eduBtn);
            eduBtn = Instantiate(eduBtn) as GameObject;
            eduBtn.transform.parent = btnTable.transform;

            UIButton btn = eduBtn.GetComponent<UIButton>();
            btn.normalSprite2D = (Sprite)Resources.Load("but_place_n");
            btn.hoverSprite2D = (Sprite)Resources.Load("but_place_o");
            btn.pressedSprite2D = (Sprite)Resources.Load("but_place_d");

            EduButton script = eduBtn.GetComponent<EduButton>();
            script.eduID = i;
            script.SetUIManager(this);

            eduBtn.GetComponent<UI2DSprite>().MakePixelPerfect();
        }
        //Debug.Log("Reposition");
        btnTable.GetComponent<UIGrid>().Reposition();
    }

    public void SetHelp(int x)
    {
        string result = "";
        if (x == -1)
        {
            result = "请选择想要执行的任务";
        }
        else
        {
            result += allEvents[x].name + " 熟练度等级：" + new string('★', allEvents[x].level);
            result += " 消耗活力：" + -allEvents[x].ap;
        }
        helplabel.text = result;
        //UIFresh();
    }

    private void UIFresh()
    {
        daylabel.text = date.Month + "月" + date.Day + "日";
        datelabel.text = Constants.WEEK_DAYS[Convert.ToInt16(date.DayOfWeek)];
        moneylabel.text = "金钱: " + player.GetBasicStatus("金钱");
        wenlabel.text = player.GetBasicStatus("文科").ToString();
        lilabel.text = player.GetBasicStatus("理科").ToString();
        yilabel.text = player.GetBasicStatus("艺术").ToString();
        tilabel.text = player.GetBasicStatus("体育").ToString();
        zhailabel.text = player.GetBasicStatus("宅力").ToString();
        energylabel.text = player.energyPoint.ToString();

        foreindexlabel.text = foreclass + foreindex.ToString();
        afterindexlabel.text = afterclass + afterindex.ToString();
        //foreicon.sprite2D = (Sprite)Resources.Load("");
        //aftericon.sprite2D = (Sprite)Resources.Load("");
    }

    public void RelaxExecute()
    {
        float successrate = player.energyPoint * 0.9f;
        float index1 = UnityEngine.Random.Range(0, 1) < successrate ? 1f : 0.5f;
        float delta = UnityEngine.Random.Range(5, 10);
        float final = delta * index1;
        int energyGet = (int)final;
        StartCoroutine(ShowResult(energyGet));
        //Player.GetInstance().AddEnergy(energyGet);
        player.AddEnergy(energyGet);
    }

    public void Execute(int x)
    {
        Player player = DataManager.GetInstance().GetGameVar<Player>("玩家");
        //执行计算并更改
        Debug.Log("你选择了" + allEvents[x].name);
        float successrate = player.energyPoint * 0.9f;
        if (allEvents[x].name.Contains(foreclass) || allEvents[x].name.Contains(afterclass))
        {
            successrate += 0.1f;
        }
        //成功则加倍
        float index1 = UnityEngine.Random.Range(0f, 1f) < successrate ? 2f : 1f;
        //加成系数
        float index2 = 1f;
        if (allEvents[x].name.Contains(foreclass)) index2 = foreindex;
        if (allEvents[x].name.Contains(afterclass)) index2 = afterindex;
        Dictionary<string, int> change = new Dictionary<string, int>();
        foreach (KeyValuePair<string, EduStatistic> kv in allEvents[x].statistic)
        {
            if (kv.Value != null)
            {
                //区间内随机值
                float delta = UnityEngine.Random.Range(kv.Value.min, kv.Value.min);
                float final = delta;
                //正数值加倍
                if (delta > 0) final = delta * index1;
                //课表加成
                if (kv.Key == foreclass || kv.Key == afterclass) final *= index2;
                change.Add(kv.Key, (int)final);
            }
        }
        int energyCost = allEvents[x].ap;
        StartCoroutine(ShowResult(change, energyCost, index1));
        foreach(KeyValuePair<string,int> kv in change)
        {
            player.AddBasicStatus(kv.Key, kv.Value);
        }
        player.AddEnergy(energyCost);
        //currentNode.EduExit();
    }

    private IEnumerator ShowResult(Dictionary<string, int> change, int cost, float xxx)
    {
        functionContainer.SetActive(false);
        selectionContainer.SetActive(false);
        spriteContainer.SetActive(true);
        float i = 0;
        while (i < 2)
        {
            if (i < 1) showlabel.text = "动画还有 2 秒";
            else if (i < 2) showlabel.text = "动画还有 1 秒";
            else showlabel.text = "";
            i = Mathf.MoveTowards(i, 2, Time.deltaTime);
            yield return null;
        }
        //spriteContainer.SetActive(false);
        acgo.SetActive(true);
        UIFresh();
        showlabel.text = "结算显示：";
        if (xxx == 2f) showlabel.text += "大成功！\r\n";
        foreach(KeyValuePair<string,int> kv in change)
        {
            showlabel.text += kv.Key + "：" + kv.Value + "  ";
        }
        showlabel.text += "消耗活力" + (-cost).ToString() + "点";
        showlabel.text += "\r\n请点击任意地方进入下一天";
    }

    private IEnumerator ShowResult(int cost)
    {
        functionContainer.SetActive(false);
        selectionContainer.SetActive(false);
        spriteContainer.SetActive(true);
        float i = 0;
        while (i < 2)
        {
            if (i < 1) showlabel.text = "动画还有 2 秒";
            else if (i < 2) showlabel.text = "动画还有 1 秒";
            else showlabel.text = "";
            i = Mathf.MoveTowards(i, 2, Time.deltaTime);
            yield return null;
        }
        //spriteContainer.SetActive(false);
        acgo.SetActive(true);
        UIFresh();
        showlabel.text = "恢复活力" + cost;
        showlabel.text += "\r\n请点击任意地方进入下一天";
    }

    public void NextDay()
    {
        //供调试调用
        currentNode.EduExit();
        spriteContainer.SetActive(false);
        acgo.SetActive(false);
    }

    public void ReturnMap()
    {
        currentNode.ReturnMap();
    }
}
