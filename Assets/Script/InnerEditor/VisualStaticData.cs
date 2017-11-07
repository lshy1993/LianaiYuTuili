using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.EventSystem;
using Assets.Script.GameStruct.Model;

public class VisualStaticData : EditorWindow
{
    VisualStaticData()
    {
        this.titleContent = new GUIContent("StaticData");
    }

    private Dictionary<string, Evidence> evidences;
    private Vector2 scrollPosition = Vector2.zero;

    [MenuItem("GalTool/StaticData")]
    public static void showWindow()
    {
        CreateInstance<VisualStaticData>().Show();
    }

    private void OnEnable()
    {
        evidences = EvidenceManager.GetStaticEvidenceDic();
    }

    public void OnGUI()
    {
        GUILayout.BeginScrollView(scrollPosition);
        foreach (KeyValuePair<string, Evidence> kv in evidences)
        {
            Evidence ev = kv.Value;
            GUILayout.Label(ev.name);
            GUILayout.Label(ev.introduction);
            GUILayout.Space(5);
        }
        GUILayout.EndScrollView();
    }
}
