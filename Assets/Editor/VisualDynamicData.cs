using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct.EventSystem;

/// <summary>
/// 编辑器插件 查看动态数据
/// </summary>
public class VisualDynamicData : EditorWindow
{
    VisualDynamicData()
    {
        this.titleContent = new GUIContent("DynamicData");
    }

    private string detitle, content;
    private Vector2 scrollPosition1, scrollPosition2, scrollPosition3;
    private bool toggle1, toggle2, toggle3, toggle4, toggle5, toggle6;
    private int index;
    private DataManager dm;
    private Player p { get { return dm.gameData.player; } }

    [MenuItem("GalTool/DynamicData")]
    public static void showWindow()
    {
        CreateInstance<VisualDynamicData>().Show();
    }

    private void OnEnable()
    {
        //开启窗口时 加载数据
        dm = DataManager.GetInstance();
        autoRepaintOnSceneChange = true;
        toggle1 = false;
    }

    public void OnGUI()
    {
        //GUI显示
        if (GUILayout.Button("REFRESH"))
        {
            index = -1;
        }
        GUILayout.Space(5);
        EditorGUILayout.LabelField("面板链", ShowChain());
        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        //game var
        GUILayout.BeginVertical(GUILayout.MaxWidth(300));
        EditorGUILayout.LabelField("GameVar", "Value");
        scrollPosition1 = GUILayout.BeginScrollView(scrollPosition1);
        EditorGUILayout.LabelField("回合数", dm.gameData.gameTurn.ToString());
        EditorGUILayout.LabelField("MODE", dm.gameData.MODE);
        EditorGUILayout.LabelField("当前事件名", dm.gameData.currentEvent);
        EditorGUILayout.LabelField("当前脚本名", dm.gameData.currentScript);
        EditorGUILayout.LabelField("文字位置", dm.gameData.currentTextPos.ToString());
        if(GUILayout.Button("全事件状态表", GUILayout.MaxWidth(100)))
        {
            index = 0;
        }
        if (GUILayout.Button("地点可触发事件", GUILayout.MaxWidth(100)))
        {
            index = 1;
        }
        if (GUILayout.Button("重复默认事件", GUILayout.MaxWidth(100)))
        {
            index = 6;
        }
        if (GUILayout.Button("强制事件表", GUILayout.MaxWidth(100)))
        {
            index = 7;
        }
        EditorGUILayout.LabelField("背景图片", dm.gameData.bgSprite);
        if(GUILayout.Button("立绘信息", GUILayout.MaxWidth(100)))
        {
            index = 2;
        }
        EditorGUILayout.LabelField("BGM", dm.gameData.BGM);
        EditorGUILayout.LabelField("SE", dm.gameData.SE);
        EditorGUILayout.LabelField("Voice", dm.gameData.Voice);
        toggle1 = GUILayout.Toggle(toggle1, "Player数据");
        if (toggle1)
        {
            GUILayout.Label("=========养成相关=========");
            EditorGUILayout.LabelField("姓", dm.gameData.heroXing);
            EditorGUILayout.LabelField("名", dm.gameData.heroMing);
            EditorGUILayout.LabelField("上午课程", dm.gameData.morningSchedule.ToString());
            EditorGUILayout.LabelField("上午指数", dm.gameData.morningRate.ToString());
            EditorGUILayout.LabelField("下午课程", dm.gameData.afternoonSchedule.ToString());
            EditorGUILayout.LabelField("下午指数", dm.gameData.afternoonRate.ToString());
            GUILayout.Label("=======角色养成属性=======");
            EditorGUILayout.LabelField("文科", p.GetBasicStatus("文科").ToString());
            EditorGUILayout.LabelField("理科", p.GetBasicStatus("理科").ToString());
            EditorGUILayout.LabelField("艺术", p.GetBasicStatus("艺术").ToString());
            EditorGUILayout.LabelField("体育", p.GetBasicStatus("体育").ToString());
            EditorGUILayout.LabelField("宅力", p.GetBasicStatus("宅力").ToString());
            EditorGUILayout.LabelField("体力", p.energyPoint.ToString());
            EditorGUILayout.LabelField("排名", p.GetBasicStatus("排名").ToString());
            EditorGUILayout.LabelField("金钱", p.GetBasicStatus("金钱").ToString());
            GUILayout.Label("=========逻辑属性=========");
            EditorGUILayout.LabelField("冷静", p.GetLogicStatus("冷静").ToString());
            EditorGUILayout.LabelField("口才", p.GetLogicStatus("口才").ToString());
            EditorGUILayout.LabelField("思维", p.GetLogicStatus("思维").ToString());
            EditorGUILayout.LabelField("观察", p.GetLogicStatus("观察").ToString());
            EditorGUILayout.LabelField("生命上限", p.LimitHP.ToString());
            EditorGUILayout.LabelField("精力总量", dm.gameData.All_MP.ToString());
            GUILayout.Label("=======攻略对象好感度=======");
            EditorGUILayout.LabelField("苏梦忆", p.GetGirlPoint("苏梦忆").ToString());
            EditorGUILayout.LabelField("西门吹", p.GetGirlPoint("西门吹").ToString());
            EditorGUILayout.LabelField("欧阳晓芸", p.GetGirlPoint("欧阳晓芸").ToString());
            EditorGUILayout.LabelField("车小曼", p.GetGirlPoint("车小曼").ToString());
            EditorGUILayout.LabelField("陈瑜", p.GetGirlPoint("陈瑜").ToString());
        }
        GUILayout.EndScrollView();
        GUILayout.EndVertical();

        //In turn var
        GUILayout.BeginVertical(GUILayout.MaxWidth(300));
        EditorGUILayout.LabelField("InTurnVar", "Value");
        scrollPosition2 = GUILayout.BeginScrollView(scrollPosition2);
        if(GUILayout.Button("持有证据", GUILayout.MaxWidth(100)))
        {
            index = 3;
        }
        if(GUILayout.Button("已知信息", GUILayout.MaxWidth(100)))
        {
            index = 4;
        }
        EditorGUILayout.LabelField("侦探模式", dm.inturnData.detectMode.ToString());
        EditorGUILayout.LabelField("当前侦探事件", dm.inturnData.currentDetectEvent);
        EditorGUILayout.LabelField("当前侦探位置", dm.inturnData.currentDetectPos);
        if(GUILayout.Button("位置状态表", GUILayout.MaxWidth(100)))
        {
            index = 5;
        }
        List<int> ls = dm.inturnData.pressedTestimony;
        string str = "已威慑证词序号 : \n";
        foreach(int item in ls)
        {
            str += item + "  ";
        }
        GUILayout.Label(str);
        EditorGUILayout.LabelField("当前询问编号",dm.inturnData.currentEnquire);
        EditorGUILayout.LabelField("当前证词序号", dm.inturnData.currentTestimonyNum.ToString());
        EditorGUILayout.LabelField("当前血量", dm.inturnData.currentHP.ToString());
        GUILayout.EndScrollView();
        GUILayout.EndVertical();

        //temp var
        GUILayout.BeginVertical();
        GUILayout.Label(detitle);
        scrollPosition3 = GUILayout.BeginScrollView(scrollPosition3);
        ReShowContent();
        GUILayout.Label(content);
        GUILayout.EndScrollView();
        GUILayout.EndVertical();
        //end
        GUILayout.EndHorizontal();
    }

    string ShowChain()
    {
        string result = "";
        foreach(string ss in dm.tempData.panelChain)
        {
            result += ss + ">";
        }
        return result;
    }

    void ReShowContent()
    {
        switch (index)
        {
            case -1:
                Repaint();
                break;
            case 0:
                ShowEventTabel();
                break;
            case 1:
                ShowAvalible();
                break;
            case 2:
                ShowCharaTabel();
                break;
            case 3:
                ShowEvidence();
                break;
            case 4:
                ShowKnown();
                break;
            case 5:
                ShowDetect();
                break;
            case 6:
                ShowDefault();
                break;
            case 7:
                ShowForce();
                break;
        }
    }

    void ShowAvalible()
    {
        detitle = "可触发事件";
        content = string.Empty;
        Dictionary<string, List<MapEvent>> locationEvents = EventManager.GetInstance().GetAvailableEvents();
        foreach(string item in locationEvents.Keys)
        {
            content += item + " : ";
            foreach(MapEvent e in locationEvents[item])
            {
                content += e.name + "  ";
            }
            content += "\n";
        }
    }

    void ShowDefault()
    {
        detitle = "默认事件";
        content = string.Empty;
        Dictionary<string, List<MapEvent>> defaultEvents = EventManager.GetInstance().GetDefaultEvents();
        foreach (string item in defaultEvents.Keys)
        {
            content += item + " : ";
            foreach (MapEvent e in defaultEvents[item])
            {
                content += e.name + "  ";
            }
            content += "\n";
        }
    }

    void ShowForce()
    {
        detitle = "强制事件表";
        content = string.Empty;
        Dictionary<string, MapEvent> fe = EventManager.GetInstance().GetForceEvents();
        foreach (string item in fe.Keys)
        {
            content += item + "\n";
        }
    }

    void ShowEvidence()
    {
        detitle = "持有证据";
        content = string.Empty;
        Dictionary<string, Evidence> dic = dm.staticData.evidenceDic;
        List<string> ls = dm.inturnData.holdEvidences;
        foreach (string item in ls)
        {
            content += item + " : ";
            content += dic[item].name + "\n";
        }
    }

    void ShowKnown()
    {
        detitle = "已知信息";
        content = string.Empty;
        List<string> lss = dm.inturnData.detectKnown;
        foreach (string item in lss)
        {
            content += item + "\n";
        }
    }

    void ShowDetect()
    {
        detitle = "侦探位置状态表";
        content = string.Empty;
        Dictionary<string, bool> lss = dm.inturnData.detectEventTable;
        foreach (string item in lss.Keys)
        {
            content += item + " : " + lss[item] + "\n";
        }
    }

    void ShowEventTabel()
    {
        detitle = "全地图事件状态表";
        content = string.Empty;
        Dictionary<string, int> lss = dm.gameData.eventStatus;
        foreach (string item in lss.Keys)
        {
            content += item + " : " + lss[item] + "\n";
        }
    }

    void ShowCharaTabel()
    {
        detitle = "立绘信息";
        content = string.Empty;
        Dictionary<int, SpriteState> lss = dm.gameData.fgSprites;
        foreach (int item in lss.Keys)
        {
            content += "sprite" + item + " : \n";
            content += "    name : " + lss[item].spriteName + "\n";
            content += "    alpha : " + lss[item].spriteAlpha + "\n";
            content += "    pos : " + lss[item].GetPosition() + "\n";
            content += "\n";
        }
    }

}
