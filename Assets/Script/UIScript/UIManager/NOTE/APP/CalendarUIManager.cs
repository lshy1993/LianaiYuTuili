using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;

public class CalendarUIManager : MonoBehaviour
{
    public UILabel monthLabel;
    public GameObject dayGrid;

    /// <summary>
    /// 当前显示的月份
    /// </summary>
    private int currentMon;

    /// <summary>
    /// 游戏首日
    /// </summary>
    private DateTime dt0 = new DateTime(2014, 8, 31);

    private Dictionary<int, Routine> routines
    {
        get { return DataManager.GetInstance().staticData.routines; }
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
        currentMon = DataManager.GetInstance().GetToday().Month;
        SetCalendar(currentMon);
    }

    //设置按钮日程
    private void SetCalendar(int month)
    {
        DateTime dt_o = new DateTime(2013, 12,1);
        DateTime current_dt = dt_o.AddMonths(month);
        //月份显示
        monthLabel.text = Convert.ToInt32(current_dt.Month) + "月";
        //星期对齐
        int week = Convert.ToInt32(current_dt.DayOfWeek);
        current_dt = current_dt.AddDays(-week + 1);
        for (int i = 0; i < 35; i++)
        {
            GameObject go = dayGrid.transform.GetChild(i).gameObject;
            GameObject event_grid = go.transform.Find("Event_Grid").gameObject;
            //UI变更
            UILabel golab = go.transform.Find("Day_Label").GetComponent<UILabel>();
            golab.text = current_dt.Day.ToString();
            golab.color = DayColor(current_dt);
            //日程设置
            event_grid.transform.DestroyChildren();
            int round = (current_dt - dt0).Days;
            if (routines.ContainsKey(round))
            {
                //读取当日数据
                Routine currentR = routines[round];
                if (currentR.isHoliday) golab.color = Color.red;
                foreach (string str in currentR.routines)
                {
                    GameObject eventGo = Resources.Load("Prefab/Event_Label") as GameObject;
                    eventGo.GetComponent<UILabel>().text = str;
                    NGUITools.AddChild(event_grid, eventGo);
                }
                event_grid.GetComponent<UITable>().Reposition();
            }
            //天数加1
            current_dt = current_dt.AddDays(1);
        }
    }

    private Color DayColor(DateTime dt)
    {
        if (dt == DataManager.GetInstance().GetToday()) return Color.green;
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
        if (currentMon >= 13) return;
        SetCalendar(++currentMon);
    }

    public void PrevMonth()
    {
        if (currentMon <= 8) return;
        SetCalendar(--currentMon);
    }
}
