using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.EventSystem;
using Assets.Script.GameStruct.Model;

/// <summary>
/// 编辑器插件 查看读入游戏的静态数据
/// </summary>
public class VisualStaticData : EditorWindow
{
    VisualStaticData()
    {
        this.titleContent = new GUIContent("StaticData");
    }

    private Dictionary<string, Evidence> evidences;
    private Dictionary<string, MapEvent> events;
    private Dictionary<string, DetectEvent> detects;
    private Dictionary<string, EnquireEvent> enquires;
    private Dictionary<string, ReasoningEvent> reasons;
    private Dictionary<string, Girl> girls;
    private Dictionary<string, Tour> tours;
    private Dictionary<string, Keyword> keywords;
    private Dictionary<int, Routine> routines;

    private Vector2 scrollPosition;
    private string content;
    private bool isEng;
    private int toggleNum;

    [MenuItem("GalTool/StaticData")]
    public static void showWindow()
    {
        CreateInstance<VisualStaticData>().Show();
    }

    private void OnEnable()
    {
        evidences = DataManager.GetInstance().staticData.evidenceDic;
        events = DataManager.GetInstance().staticData.eventTable;
        detects = DataManager.GetInstance().staticData.detectEvents;
        enquires = DataManager.GetInstance().staticData.enquireEvents;
        reasons = DataManager.GetInstance().staticData.reasonEvents;
        girls = DataManager.GetInstance().staticData.girls;
        tours = DataManager.GetInstance().staticData.tours;
        keywords = DataManager.GetInstance().staticData.keywords;
        routines = DataManager.GetInstance().staticData.routines;
    }

    public void OnGUI()
    {
        //按钮选项
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("地图事件"))
        {
            Reset(toggleNum = 1);
        }
        if (GUILayout.Button("侦探"))
        {
            Reset(toggleNum = 2);
        }
        if(GUILayout.Button("询问"))
        {
            Reset(toggleNum = 3);
        }
        if (GUILayout.Button("推理"))
        {
            Reset(toggleNum = 4);
        }
        if (GUILayout.Button("证据表"))
        {
            Reset(toggleNum = 5);
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("APP-女主角"))
        {
            Reset(toggleNum = 6);
        }
        if (GUILayout.Button("APP-旅游资讯"))
        {
            Reset(toggleNum = 7);
        }
        if (GUILayout.Button("APP-帮助词条"))
        {
            Reset(toggleNum = 8);
        }
        if (GUILayout.Button("APP-日历"))
        {
            Reset(toggleNum = 9);
        }
        if (GUILayout.Button("养成"))
        {
            Reset(toggleNum = 10);
        }
        GUILayout.EndHorizontal();
        isEng = GUILayout.Toggle(isEng, "显示变量名");
        if (isEng)
        {
            Reset(toggleNum);
        }else
        {
            Reset(toggleNum);
        }
        //isEng = GUILayout.Toggle(isEng, "显示变量名");
        GUILayout.Space(5);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        GUILayout.Label(content);
        GUILayout.EndScrollView();
    }

    void Reset(int num)
    {
        switch (num)
        {
            case 1:
                SetEvents();
                break;
            case 2:
                SetDetect();
                break;
            case 3:
                SetEnquire();
                break;
            case 4:
                SetReason();
                break;
            case 5:
                SetEvidence();
                break;
            case 6:
                break;
            case 7:
                break;
        }
        Repaint();
    }

    void SetEvents()
    {
        content = string.Empty;
        foreach (KeyValuePair<string, MapEvent> kv in events)
        {
            content += kv.Value.ToString(isEng);
            content += "\n\n";
        }
    }

    void SetDetect()
    {
        content = string.Empty;
        foreach (KeyValuePair<string, DetectEvent> kv in detects)
        {
            content += kv.Value.ToString(isEng);
            content += "\n\n";
        }
    }

    void SetEnquire()
    {
        content = string.Empty;
        foreach (KeyValuePair<string, EnquireEvent> kv in enquires)
        {
            content += kv.Value.ToString(isEng);
            content += "\n\n";
        }
    }

    void SetReason()
    {
        content = string.Empty;
        foreach (KeyValuePair<string, ReasoningEvent> kv in reasons)
        {
            content += kv.Value.ToString(isEng);
            content += "\n\n";
        }
    }

    void SetEvidence()
    {
        content = string.Empty;
        foreach (KeyValuePair<string, Evidence> kv in evidences)
        {
            Evidence ev = kv.Value;
            content += ev.ToString(isEng);
            content += "\n\n";
        }
    }

}
