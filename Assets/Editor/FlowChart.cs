using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.EventSystem;

[ExecuteInEditMode]
public class FlowChart : MonoBehaviour
{
    private bool eventSystemPresent = false;

    /// 事件表《事件名，事件》
    public Dictionary<string, MapEvent> eventTable;

    /// 强制事件表《事件名，事件》
    public Dictionary<string, MapEvent> forceEventTable;

    /// 事件状态《事件名，状态编号》
    public Dictionary<string, int> eventState;

    /// 当前地点的可用事件表《地点名，事件列表》
    public Dictionary<string, List<MapEvent>> locationEvents;

    void Start()
    {
        CheckEventSystem();
    }

    //读取游戏的 eventManager 获取事件表
    private void CheckEventSystem()
    {
        if (eventSystemPresent) return;
        EventManager eManager = GameObject.Find("GameManager").GetComponent<EventManager>();
        eventTable = eManager.GetEvents();
        forceEventTable = eManager.GetForceEvents();
        eventState = eManager.GetEventState();
    }
}
