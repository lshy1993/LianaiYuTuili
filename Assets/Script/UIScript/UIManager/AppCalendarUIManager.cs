using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;

public class AppCalendarUIManager : MonoBehaviour
{
    public UILabel monthLabel;
    public GameObject dayGrid;
    private int currentMon;

    private Dictionary<int, Routine> routines
    {
        get { return DataPool.GetInstance().GetStaticVar("日程表") as Dictionary<int, Routine>; }
    }

    private void Awake()
    {
        //事先生成所有的块
        dayGrid.transform.DestroyChildren();
        for(int i = 0; i < 35; i++)
        {
            //读取prefab
            GameObject go = Resources.Load("Prefab/DayRoutine_Container") as GameObject;
            go = NGUITools.AddChild(dayGrid, go);
        }
        dayGrid.GetComponent<UIGrid>().Reposition();
    }

    private void OnEnable()
    {
        int month = DataManager.GetInstance().GetToday().Month;
        SetCalendar(month);
    }

    //设置按钮日程
    private void SetCalendar(int month)
    {
        if (month == currentMon) return;
        currentMon = month;
        monthLabel.text = month + "月";
        //获取当月的首项
        DateTime dt0 = new DateTime(2014, 8, 31);
        DateTime dt = new DateTime(2014, month, 1);
        int week = Convert.ToInt32(dt.DayOfWeek);
        dt = dt.AddDays(-week + 1);
        for (int i = 0; i < 35; i++)
        {
            GameObject go = dayGrid.transform.GetChild(i).gameObject;
            GameObject event_grid = go.transform.Find("Event_Grid").gameObject;
            //UI变更
            UILabel golab = go.transform.Find("Day_Label").GetComponent<UILabel>();
            golab.text = dt.Day.ToString();
            golab.color = DayColor(dt);
            //日程设置
            event_grid.transform.DestroyChildren();
            int round = (dt - dt0).Days;
            if(routines.ContainsKey(round))
            {
                //读取当日数据
                Routine currentR = routines[round];
                //Debug.Log(dt.ToShortDateString() + " " + routines[round].ToString());
                foreach (string str in currentR.routines)
                {
                    GameObject eventGo = Resources.Load("Prefab/Event_Label") as GameObject;
                    eventGo.GetComponent<UILabel>().text = str;
                    NGUITools.AddChild(event_grid, eventGo);
                }
                event_grid.GetComponent<UITable>().Reposition();
            }
            //天数加1
            dt = dt.AddDays(1);
        }
        
    }

    private Color DayColor(DateTime dt)
    {
        if (dt.Month != currentMon) return Color.gray;
        int x = Convert.ToInt32(dt.DayOfWeek);
        if (x == 6 || x == 0)
        {
            return Color.red;
        }
        return Color.black;
    }

    //public方法 供按钮调用
    public void NextMonth()
    {
        SetCalendar(currentMon + 1);
    }

    public void PrevMonth()
    {
        SetCalendar(currentMon - 1);
    }
}
