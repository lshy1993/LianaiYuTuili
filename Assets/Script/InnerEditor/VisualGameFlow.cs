using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.EventSystem;

public class VisualGameFlow : EditorWindow
{
    VisualGameFlow()
    {
        this.titleContent = new GUIContent("FlowChart");
    }

    private Dictionary<string, MapEvent> events;
    private Vector2 scrollPosition = Vector2.zero;

    [MenuItem("GalTool/FlowWindow")]
    public static void showWindow()
    {
        CreateInstance<VisualGameFlow>().Show();
    }

    private void OnEnable()
    {
        events = EventManager.GetStaticEvent();
    }

    public class DrawCondition
    {
        public int[] maxRow = new int[10];
        public int currentRound, currentColumn;
        public Vector2 currentDrawPoint;

        public DrawCondition()
        {
            currentRound = 1;
            currentColumn = 1;
            currentDrawPoint = Vector2.zero;
            maxRow[0] = 1;
        }
    }

    public void OnGUI()
    {
        //设定绘制起点
        DrawCondition currentDC = new DrawCondition();
        //事件遍历循环
        GUI.BeginScrollView(new Rect(0, 0, 400, 400), scrollPosition, new Rect(0, 0, 420, 420));
        foreach (KeyValuePair<string,MapEvent> kv in events)
        {
            DrawEvent(kv.Value, currentDC);
            DrawLink(kv.Value, currentDC);
        }
        GUI.EndScrollView();
    }

    //绘制读取的事件
    private void DrawEvent(MapEvent me, DrawCondition dc)
    {
        if (me.isdefault) return;
        //开始行
        int a = me.conditionTurn.GetMin();
        if (a == 999) return;
        int b = me.conditionTurn.GetMax();
        //Debug.Log(me.name + a + b);
        //算法1：搜寻可以放的下的位置
        for (int i = 0; i < dc.maxRow.Length; i++)
        {
            if (dc.maxRow[i] < a)
            {
                dc.currentColumn = i;
                break;
            }
        }
        dc.maxRow[dc.currentColumn] = a;
        int x = 5 + dc.currentColumn * 105;
        int y = 5 + dc.maxRow[dc.currentColumn] * 55;
        GUI.Button(new Rect(x, y, 100, 50), me.name);
    }

    private void DrawLink(MapEvent me, DrawCondition dc)
    {

    }
}
