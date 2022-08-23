using Assets.Script.GameStruct.Model;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// EventManager
/// 事件管理器：事件树 逻辑的处理方法
/// </summary>
namespace Assets.Script.GameStruct.EventSystem
{
    public class EventManager
    {
        private static EventManager instance = new EventManager();

        private static readonly int STATE_NOT_RUNNED = 0;
        private static readonly int STATE_RUNNED = 1;

        /// <summary>
        ///  事件总表《事件名，事件》
        /// </summary>
        private Dictionary<string, MapEvent> eventTable
        {
            get { return dataManager.staticData.eventTable; }
        }

        /// <summary>
        /// 事件状态《事件名，状态编号》
        /// </summary>
        private Dictionary<string, int> eventState
        {
            get { return dataManager.gameData.eventStatus; }
        }

        /// <summary>
        /// 选项开关
        /// </summary>
        private List<string> selectionSwitch
        {
            get { return dataManager.gameData.selectionSwitch; }
        }

        /// <summary>
        /// 触发式事件表《事件名，事件》
        /// </summary>
        private Dictionary<string, MapEvent> nonDefaultTable;

        /// <summary>
        /// 强制事件表，是事件表的子集《事件名，事件》
        /// </summary>
        private Dictionary<string, MapEvent> forceEventTable;

        /// <summary>
        /// 当前地点的可用事件表《地点名，事件列表》
        /// </summary>
        private Dictionary<string, List<MapEvent>> availableEvents;

        /// <summary>
        /// 默认事件表《地点名，事件列表》
        /// </summary>
        private Dictionary<string, List<MapEvent>> defaultEvents;

        private DataManager dataManager;

        public static EventManager GetInstance()
        {
            //if (instance == null) instance = new EventManager();
            return instance;
        }

        private EventManager() { }

        public MapEvent currentEvent;

        /// <summary>
        /// 事件管理器初始化
        /// </summary>
        public void Init(DataManager manager)
        {
            this.dataManager = manager;
            //初始化地点索引的空表
            availableEvents = new Dictionary<string, List<MapEvent>>();
            defaultEvents = new Dictionary<string, List<MapEvent>>();
            //遍历所有的地点
            foreach (TextAsset text in Resources.LoadAll<TextAsset>(Constants.LOCATION_PATH))
            {
                JsonData data = JsonMapper.ToObject(text.text);
                if (data != null && data.Contains("地点"))
                {
                    string location = (string)data["地点"];
                    availableEvents.Add(location, new List<MapEvent>());
                    defaultEvents.Add(location, new List<MapEvent>());
                }
            }
            //遍历所有事件
            forceEventTable = new Dictionary<string, MapEvent>();
            nonDefaultTable = new Dictionary<string, MapEvent>();
            foreach (KeyValuePair<string, MapEvent> kv in eventTable)
            {
                //强制事件的提取
                if (kv.Value.position == null) forceEventTable.Add(kv.Key, kv.Value);
                //默认事件的提取
                else if (kv.Value.isdefault) defaultEvents[kv.Value.position].Add(kv.Value);
                //触发式事件
                else nonDefaultTable.Add(kv.Key, kv.Value);

            }
        }

        #region Get方法
        /// <summary>
        /// 获取触发式事件表
        /// </summary>
        public Dictionary<string, MapEvent> GetNonDefaultEvents()
        {
            return nonDefaultTable;
        }
        /// <summary>
        /// 获取强制事件表
        /// </summary>
        public Dictionary<string, MapEvent> GetForceEvents()
        {
            return forceEventTable;
        }
        /// <summary>
        /// 获取所有事件
        /// </summary>
        public Dictionary<string, MapEvent> GetEvents()
        {
            return eventTable;
        }
        /// <summary>
        /// 获取所有事件当前状态
        /// </summary>
        public Dictionary<string, int> GetEventState()
        {
            return eventState;
        }
        /// <summary>
        /// 获取某地点所有的可能事件
        /// </summary>
        public Dictionary<string, List<MapEvent>> GetAvailableEvents()
        {
            return availableEvents;
        }
        /// <summary>
        /// 获取某地点所有的默认事件
        /// </summary>
        public Dictionary<string, List<MapEvent>> GetDefaultEvents()
        {
            return defaultEvents;
        }
        #endregion


        /// <summary>
        /// 初始化事件状态表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, int> LoadEventState(Dictionary<string,MapEvent> eventTable)
        {
            Dictionary<string, int> eventState = new Dictionary<string, int>();
            // 初始化事件状态
            foreach (KeyValuePair<string, MapEvent> kv in eventTable)
            {
                if (!kv.Value.isdefault) eventState.Add(kv.Key, STATE_NOT_RUNNED);
            }
            return eventState;
        }



        /// <summary>
        /// 获取当前条件下可以触发的强制事件
        /// </summary>
        public MapEvent GetCurrentForceEvent()
        {
            foreach (KeyValuePair<string, MapEvent> kv in forceEventTable)
            {
                //Debug.Log("强制事件遍历"+kv.Key + " 状态" + IsAvailableEvent(kv.Value));
                if (IsAvailableEvent(kv.Value))
                {
                    //Debug.Log("选取强制事件:" + kv.Value.name);
                    return kv.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// 判断是否有新的事件
        /// </summary>
        /// <param name="location">地点名</param>
        public bool IsNewEventAt(string location)
        {
            return availableEvents[location].Count != 0;
        }

        /// <summary>
        /// 获取当前地点的事件
        /// </summary>
        /// <param name="location">地点名</param>
        public MapEvent GetCurrentEventAt(string location)
        {
            Debug.Log("检测的地点名：" + location);
            if (availableEvents[location].Count == 0)
            {
                if (dataManager.gameData.gameTurn == 0) return null;
                //当没有其他事件时，才触发默认事件
                int ranNum = UnityEngine.Random.Range(0, defaultEvents[location].Count);
                return defaultEvents[location][ranNum];
            }
            else
            {
                //否侧抽取事件池
                int ranNum = UnityEngine.Random.Range(0, availableEvents[location].Count);
                return availableEvents[location][ranNum];
            }
        }

        /// <summary>
        /// 刷新每个地点可以触发的事件
        /// </summary>
        public void UpdateEvent()
        {
            //清空每个地点的可触发事件表
            foreach (List<MapEvent> list in availableEvents.Values)
            {
                list.Clear();
            }
            //从非默认事件表中将可以发生的事件加入
            foreach (KeyValuePair<string, MapEvent> kv in nonDefaultTable)
            {
                if (IsAvailableEvent(kv.Value)
                    && kv.Value.position != null
                    && availableEvents.ContainsKey(kv.Value.position))
                {
                    //Debug.Log("now available map event" + kv.Key);
                    availableEvents[kv.Value.position].Add(kv.Value);
                }
            }
        }

        /// <summary>
        /// 当前事件的完成
        /// </summary>
        public void FinishCurrentEvent()
        {
            string currentName = dataManager.gameData.currentEvent;
            eventState[currentName] = STATE_RUNNED;
            //刷新
            UpdateEvent();
        }

        /// <summary>
        /// 判断在当前条件下，此事件是否能发生
        /// </summary>
        private bool IsAvailableEvent(MapEvent e)
        {
            Player player = dataManager.gameData.player;
            int turn = dataManager.gameData.gameTurn;

            //如果该事件未在eventState内
            if (!eventState.ContainsKey(e.name))
            {
                DebugLog.LogError("该事件未在状态表中，key=" + e.name);
            }

            //如果该事件已经执行过
            if (!e.isdefault && eventState[e.name] != STATE_NOT_RUNNED)
            {
                return false;
            }

            // 不满足回合数
            if (turn > e.conditionTurn.GetMax() || turn < e.conditionTurn.GetMin())
            {
                return false;
            }

            if (!Constants.DEBUG)
            {
                // 不满足前置属性
                if (e.conditionStatus != null && e.conditionStatus.Count > 0)
                {
                    foreach (KeyValuePair<string, Range> kv in e.conditionStatus)
                    {
                        int bstatus = player.GetBasicStatus(kv.Key);
                        if ((kv.Value.GetMin() > bstatus || bstatus > kv.Value.GetMax()))
                            return false;
                        /* 暂时去掉了侦探属性限制
                        int lstatus = player.GetLogicStatus(kv.Key);
                        if ((kv.Value.GetMin() > lstatus  || lstatus > kv.Value.GetMax()))
                            return false;
                        */
                    }
                }

                // 好感度
                if (e.conditionGirls != null && e.conditionGirls.Count > 0)
                {
                    foreach (KeyValuePair<string, Range> kv in e.conditionGirls)
                    {
                        int gstatus = player.GetGirlPoint(kv.Key);
                        if ((kv.Value.GetMin() > gstatus || gstatus > kv.Value.GetMax()))
                        {
                            return false;
                        }
                            
                    }
                }
            }

            //前置【非】事件锁
            if (e.conditionNotEvents != null && e.conditionNotEvents.Count > 0)
            {
                foreach(string eventName in e.conditionNotEvents)
                {
                    if (!eventState.ContainsKey(eventName))
                    {
                        DebugLog.LogError("前置非事件未在事件表中！key=" + eventName);
                        continue;
                    }
                    if (eventState[eventName] == STATE_RUNNED)
                    {
                        return false;
                    }
                }
            }

            //前置【与】事件判断
            if (e.conditionAndEvents != null && e.conditionAndEvents.Count > 0)
            {
                foreach (string eventName in e.conditionAndEvents)
                {
                    if (!eventState.ContainsKey(eventName))
                    {
                        DebugLog.LogError("前置与事件未在事件表中！key=" + eventName);
                        continue;
                    }
                    if (eventState[eventName] == STATE_NOT_RUNNED)
                    {
                        return false;
                    }
                }
            }

            //前置【或】事件判断
            if (e.conditionOrEvents != null && e.conditionOrEvents.Count > 0)
            {
                foreach (string eventName in e.conditionOrEvents)
                {
                    if (!eventState.ContainsKey(eventName))
                    {
                        DebugLog.LogError("前置或事件未在事件表中！key=" + eventName);
                        continue;
                    }
                    if (eventState[eventName] == STATE_RUNNED)
                    {
                        return true;
                    }
                }
                return false;
            }

            //选项判断
            if (e.conditionSelection != null && e.conditionSelection.Count > 0)
            {
                foreach (string eventName in e.conditionSelection)
                {
                    if (selectionSwitch.Contains(eventName))
                    {
                        return true;
                    }
                }
                return false;
            }


            return true;
        }

        /// <summary>
        /// 运行在某个地点的事件
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        public GameNode RunEvent(string place)
        {
            MapEvent e = GetCurrentEventAt(place);

            currentEvent = e;
            dataManager.gameData.currentEvent = e.name;

            GameNode node = NodeFactory.GetInstance().FindTextScript(e.entryNode);
            return node;
        }

        /// <summary>
        /// 运行强制事件
        /// </summary>
        public GameNode RunForceEvent()
        {
            MapEvent e = GetCurrentForceEvent();

            currentEvent = e;
            dataManager.gameData.currentEvent = e.name;

            GameNode node = NodeFactory.GetInstance().FindTextScript(e.entryNode);
            return node;
        }

        /// <summary>
        /// 运行结局事件
        /// </summary>
        public GameNode RunFinEvent()
        {
            //TODO结局判断
            GameNode node = NodeFactory.GetInstance().FindTextScript("demo_fin");
            return node;
        }

        /*
        /// <summary>
        /// 读取事件表 json文件
        /// </summary>
        public static Dictionary<string, MapEvent> GetStaticEvent()
        {
            string path = (Constants.DEBUG ? Constants.DEBUG_PATH : Constants.DEFAULT_PATH) + "Events/";
            Dictionary<string, MapEvent> eventTable = new Dictionary<string, MapEvent>();

            Debug.Log("读取事件表");
            foreach (TextAsset text in Resources.LoadAll<TextAsset>(path))
            {
                Debug.Log("读取：" + text.name);
                JsonData alldata = JsonMapper.ToObject(text.text);
                if (alldata.IsArray)
                {
                    foreach (JsonData data in alldata)
                    {
                        if (data.Contains("事件"))
                        {
                            MapEvent e = new MapEvent(data);
                            eventTable.Add(e.name, e);
                        }
                    }
                }
                else
                {
                    if (alldata.Contains("事件"))
                    {
                        MapEvent e = new MapEvent(alldata);
                        eventTable.Add(e.name, e);
                    }
                }
            }
            //【注意】具体事件逻辑添加
            LoadStaticEventLogic(eventTable);
            return eventTable;
        }

        /// <summary>
        /// 初始化事件的内在逻辑
        /// </summary>
        private static void LoadStaticEventLogic(Dictionary<string, MapEvent> eventTable)
        {
            // 读入事件逻辑文件
            string path = (Constants.DEBUG ? Constants.DEBUG_PATH : Constants.DEFAULT_PATH) + "EventLogic/";

            foreach (TextAsset text in Resources.LoadAll<TextAsset>(path))
            {
                JsonData alldata = JsonMapper.ToObject(text.text);
                if (alldata.Contains("线性事件"))
                {
                    // 线性事件链的处理，对于i > 0, 将前一事件加入后事件的conditionAndEvent中
                    JsonData eventChain = alldata["线性事件"];
                    for (int i = 1; i < eventChain.Count; i++)
                    {
                        string eName = (string)eventChain[i];
                        string prevName = (string)eventChain[i - 1];
                        eventTable[eName].conditionAndEvents.Add(prevName);
                    }
                }
                else if (alldata.Contains("特殊条件"))
                {
                    JsonData nonChainEvents = alldata["特殊条件"];
                    foreach (JsonData data in nonChainEvents)
                    {
                        addSingleEventLogic(data, eventTable);
                    }
                }

            }
        }

        /// <summary>
        /// 为单个事件添加逻辑条件
        /// </summary>
        /// <param name="data">json文件</param>
        /// <param name="eventTable">静态事件总表</param>
        /// <returns></returns>
        private static MapEvent addSingleEventLogic(JsonData data, Dictionary<string, MapEvent> eventTable)
        {
            MapEvent me = null;
            if (data != null)
            {
                if (data.IsString)
                {
                    me = eventTable[(string)data];
                }
                else if (data.IsObject && data.Contains("事件"))
                {
                    me = eventTable[(string)data["事件"]];

                    if (data.Contains("前置与"))
                    {
                        foreach (JsonData eventName in data["前置与"])
                        {
                            me.conditionAndEvents.Add((string)eventName);
                        }
                    }

                    if (data.Contains("前置或"))
                    {
                        foreach (JsonData eventName in data["前置或"])
                        {
                            me.conditionOrEvents.Add((string)eventName);
                        }
                    }
                }
            }
            return me;
        }

        /// <summary>
        /// 读入强制事件表
        /// 强制事件表被用于EndTurnNode的判定。
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, MapEvent> LoadForceEvents()
        {
            // 读入强制事件
            string path = (Constants.DEBUG ? DEBUG_PATH : DEFAULT_PATH) + "ForceEvents/";
            Dictionary<string, MapEvent> forceEventTable = new Dictionary<string, MapEvent>();

            foreach (TextAsset text in Resources.LoadAll<TextAsset>(path))
            {
                JsonData alldata = JsonMapper.ToObject(text.text);
                if (alldata.IsArray)
                {
                    foreach (JsonData data in alldata)
                    {
                        if (data.Contains("事件"))
                        {
                            MapEvent e = new MapEvent(data);
                            forceEventTable.Add((string)data["事件"], e);
                        }
                    }
                }
                else
                {
                    if (alldata.Contains("事件"))
                    {
                        MapEvent e = new MapEvent(alldata);
                        forceEventTable.Add((string)alldata["事件"], e);
                    }
                }
            }
            return forceEventTable;
        }
        */

    }
}