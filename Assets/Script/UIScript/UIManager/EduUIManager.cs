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
    private GameObject eduObject;
    private UIPanel eduPanel;
    private UILabel daylabel, datelabel, moneylabel;
    private UILabel wenlabel, lilabel, tilabel, yilabel, zhailabel, showlabel;
    private UILabel helplabel, infolabel;

    private GameObject spriteContainer, selectionContainer, acgo;
    private GameObject btnTable;

    private Player player;
    //private Hashtable gVars;
    private DateTime date;

    private List<EduEvent> allEvents;
    private List<GameObject> eduButtons;

    private EduNode currentNode;

    void Start()
    {
        daylabel = transform.Find("Time_Container/Day_Label").gameObject.GetComponent<UILabel>();
        datelabel = transform.Find("Time_Container/Date_Label").gameObject.GetComponent<UILabel>();
        moneylabel = transform.Find("Time_Container/Money_Label").gameObject.GetComponent<UILabel>();
        wenlabel = transform.Find("CharaInfo_Container/Num_Grid/Wen_Label").gameObject.GetComponent<UILabel>();
        lilabel = transform.Find("CharaInfo_Container/Num_Grid/Li_Label").gameObject.GetComponent<UILabel>();
        tilabel = transform.Find("CharaInfo_Container/Num_Grid/Ti_Label").gameObject.GetComponent<UILabel>();
        yilabel = transform.Find("CharaInfo_Container/Num_Grid/Yi_Label").gameObject.GetComponent<UILabel>();
        zhailabel = transform.Find("CharaInfo_Container/Num_Grid/Zhai_Label").gameObject.GetComponent<UILabel>();

        helplabel = transform.Find("Selection_Container/Left_Container/Help_Label").GetComponent<UILabel>();
        infolabel = transform.Find("Selection_Container/Right_Container/Hint_Label").GetComponent<UILabel>();

        spriteContainer = transform.Find("QSprite_Container").gameObject;
        selectionContainer = transform.Find("Selection_Container").gameObject;
        acgo = transform.Find("QSprite_Container/Container").gameObject;

        showlabel = transform.Find("QSprite_Container/Show_Label").gameObject.GetComponent<UILabel>();

        btnTable = transform.Find("Selection_Container/Left_Container/Table").gameObject;

        eduButtons = new List<GameObject>();
        //UIFresh();
    }

    public void SetEduNode(EduNode node)
    {
        this.currentNode = node;
    }

    public void SetEduEvent(List<EduEvent> es)
    {
        this.allEvents = es;
        //player = Player.GetInstance();
        player = DataManager.GetInstance().GetGameVar<Player>("玩家");
        //gVars = DataManager.GetInstance().GetGameVars();
        //date = (DateTime)gVars["日期"];
        int turn = DataManager.GetInstance().GetGameVar<int>("回合");
        date = DataManager.START_DAY.AddDays(turn);
        //date = DataManager.GetInstance().;
        UIFresh();
    }

    public void SetEduButton(string type)
    {
        eduButtons.Clear();
        btnTable.transform.DestroyChildren();
        for (int i = 0; i < 8; i++)
        {
            GameObject eduBtn = (GameObject)Resources.Load("Prefab/Edu_Choice");
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

            eduButtons.Add(eduBtn);
        }
        btnTable.GetComponent<UITable>().Reposition();
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
    }

    public void Execute(int x)
    {
        Player player = DataManager.GetInstance().GetGameVar<Player>("玩家");
        //执行计算并更改
        Debug.Log("x=" + x);
        float successrate = player.GetBasicStatus("体力") * 0.9f;
        if (x == 1)
        {
            successrate += 0.1f;
        }
        float index1 = UnityEngine.Random.Range(0, 1) < successrate ? 1f : 0.5f;
        float index2 = x == 1 ? 1.25f : 1f;
        Dictionary<string, int> change = new Dictionary<string, int>();
        foreach (KeyValuePair<string, EduStatistic> kv in allEvents[x].statistic)
        {
            if (kv.Value != null)
            {
                float delta = UnityEngine.Random.Range(kv.Value.min, kv.Value.min);
                float final = delta * index1 * index2;
                change.Add(kv.Key, (int)final);
            }
        }
        int energyCost = allEvents[x].ap;
        StartCoroutine(ShowResult(change, energyCost));
        foreach(KeyValuePair<string,int> kv in change)
        {
            player.AddBasicStatus(kv.Key, kv.Value);
        }
        player.AddEnergy(energyCost);
        //currentNode.EduExit();
    }

    private IEnumerator ShowResult(Dictionary<string, int> change, int cost)
    {
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
        foreach(KeyValuePair<string,int> kv in change)
        {
            showlabel.text += kv.Key + "：" + kv.Value + "  ";
        }
        showlabel.text += "\r\n请点击任意地方进入下一天";
    }

    public void NextDay()
    {
        //供调试调用
        currentNode.EduExit();
        spriteContainer.SetActive(false);
        acgo.SetActive(false);
    }
}
