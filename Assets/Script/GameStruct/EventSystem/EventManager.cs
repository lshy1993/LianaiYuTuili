using Assets.Script.GameStruct.Model;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using System;
using System.Collections;
using System.Collections;

/// <summary>
/// EventManager
/// 用于管理事件树
/// TODO
/// </summary>

namespace Assets.Script.GameStruct.EventSystem
{
    public class EventManager
    {
        private UnityEngine.Random random;
        private static EventManager instance;
        private static readonly string DEFAULT_PATH = "Text/MapEvents/";
        private static readonly string DEBUG_PATH = "Text/TestEvents/";
        private static readonly string LOCATION_PATH = "Text/ButtonConfig/";

        private static readonly int STATE_NOT_RUNNED = 0;
        private static readonly int STATE_RUNNED = 1;

        /// <summary>
        /// 事件表
        /// </summary>
        private Dictionary<string, MapEvent> eventTable;


        /// <summary>
        /// 强制事件表，是事件表的子集
        /// </summary>
        private Dictionary<string, MapEvent> forceEventTable;

        /// <summary>
        /// 事件状态
        /// </summary>
        private Dictionary<string, int> eventState;

        /// <summary>
        /// 当前地点的可用事件表
        /// </summary>
        private Dictionary<string, List<MapEvent>> locationEvents;

        /// <summary>
        /// 游戏变量,来自DataManager
        /// </summary>
        private Hashtable gVars;

        public static EventManager GetInstance()
        {
            if (instance == null) instance = new EventManager();
            return instance;
        }

        private EventManager() { }

        public MapEvent currentEvent;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init(Dictionary<string, MapEvent> eventTable, Hashtable gVars)
        {
            random = new UnityEngine.Random();
            this.eventTable = eventTable;
            this.forceEventTable = new Dictionary<string, MapEvent>();
            foreach (KeyValuePair<string, MapEvent> kv in eventTable)
            {
                if (kv.Value.position == null) forceEventTable.Add(kv.Key, kv.Value);
            }
            this.gVars = gVars;
            this.eventState = (Dictionary<string, int>)gVars["事件状态"];
            InitLocation();
        }

        private void InitLocation()
        {
            locationEvents = new Dictionary<string, List<MapEvent>>();
            foreach (TextAsset text in Resources.LoadAll<TextAsset>(LOCATION_PATH))
            {
                JsonData data = JsonMapper.ToObject(text.text);
                if (data != null && data.Contains("地点"))
                {
                    locationEvents.Add((string)data["地点"], new List<MapEvent>());
                }
            }
        }

        public Dictionary<string, MapEvent> getForceEvents()
        {
            return forceEventTable;
        }

        public Dictionary<string, MapEvent> getEvents()
        {
            return eventTable;
        }

        public MapEvent GetCurrentForceEvent()
        {
            foreach(KeyValuePair<string, MapEvent> kv in forceEventTable)
            {
                if (IsAvailableEvent(kv.Value)) return kv.Value;
            }
            return null;

        }


        /// <summary>
        /// 获取当前地点的事件，没有返回null
        /// </summary>
        public MapEvent GetCurrentEventAt(string location)
        {
            if (locationEvents[location].Count == 0)
            {
                return null;
            }
            else
            {
                Debug.Log(UnityEngine.Random.Range(0, 1));
                Debug.Log(locationEvents[location][0].ToString());
                return locationEvents[location]
                    [UnityEngine.Random.Range(0, locationEvents[location].Count)];
            }
        }

        public void UpdateEvent()
        {
            foreach (List<MapEvent> list in locationEvents.Values)
            {
                list.Clear();
            }
            foreach (KeyValuePair<string, MapEvent> kv in eventTable)
            {
                if (IsAvailableEvent(kv.Value)
                    && kv.Value.position != null
                    && locationEvents.ContainsKey(kv.Value.position))
                {
                    locationEvents[kv.Value.position].Add(kv.Value);
                }
            }
        }

        public void FinishCurrentEvent()
        {
            eventState[currentEvent.name] = STATE_RUNNED;
            UpdateEvent();
        }

        private bool IsAvailableEvent(MapEvent e)
        {
            Player player = Player.GetInstance();
            int turn = (int)gVars["回合"];

            //已经执行过
            if (eventState[e.name] != STATE_NOT_RUNNED) return false;

            // 不满足前置日期
            if (e.conditionTurn.GetMax() < turn &&
                e.conditionTurn.GetMin() > turn)
            {
                return false;
            }

            // 不满足前置属性
            if (e.conditionStatus != null &&
                e.conditionStatus.Count > 0)
            {
                foreach (KeyValuePair<string, Range> kv in e.conditionStatus)
                {
                    if (player.ContainsBasicStatus(kv.Key) &&
                        (kv.Value.GetMin() > player.GetBasicStatus(kv.Key) || player.GetBasicStatus(kv.Key) > kv.Value.GetMax()))
                        return false;

                    if (player.ContainsLogicStatus(kv.Key) &&
                        (kv.Value.GetMin() > player.GetLogicStatus(kv.Key) || player.GetLogicStatus(kv.Key) > kv.Value.GetMax()))
                        return false;
                }
            }

            // 不满足前置事件
            if (e.conditionAndEvents != null && e.conditionAndEvents.Count > 0)
            {
                foreach (string eventName in e.conditionAndEvents)
                {
                    if (eventState[eventName] == STATE_NOT_RUNNED) return false;
                }
            }

            // 好感度
            if (e.conditionGirls != null && e.conditionGirls.Count > 0)
            {
                foreach (KeyValuePair<string, Range> kv in e.conditionGirls)
                {
                    if (player.ContainsBasicStatus(kv.Key) &&
                        (kv.Value.GetMin() > player.GetGirlPoint(kv.Key) || player.GetGirlPoint(kv.Key) > kv.Value.GetMax()))
                        return false;
                }
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

            GameNode node = NodeFactory.GetInstance().FindTextScript(e.entryNode);
            return node;
        }


        /// <summary>
        /// 读取事件表, 注意具体事件逻辑在LoadStaticEventLogic中读取完毕
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, MapEvent> GetStaticEvent()
        {
            string path = (Constants.DEBUG ? DEBUG_PATH : DEFAULT_PATH) + "Events/";
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

            LoadStaticEventLogic(eventTable);
            return eventTable;
        }

        /// <summary>
        /// 初始化事件逻辑
        /// 目前的事件逻辑由前置与/或事件组成
        /// 逻辑文件定义：
        /// {
        ///     "事件链":[事件名， {事件名， 前置与， 前置或}]
        ///     "非链事件":[事件名， {事件名， 前置与，前置或}];
        /// }
        /// </summary>
        private static void LoadStaticEventLogic(Dictionary<string, MapEvent> eventTable)
        {
            // 读入事件逻辑文件
            string path = (Constants.DEBUG ? DEBUG_PATH : DEFAULT_PATH) + "EventLogic/";

            foreach (TextAsset text in Resources.LoadAll<TextAsset>(path))
            {
                JsonData alldata = JsonMapper.ToObject(text.text);
                if (alldata.Contains("事件链") && alldata["事件链"].IsArray)
                {
                    // 事件链的处理，对于i > 0, 自动将后一事件加入前面的conditionAndEvent里面
                    MapEvent me = null;
                    string prevName = null;
                    JsonData eventChain = alldata["事件链"];
                    for (int i = 0; i < eventChain.Count; i++)
                    {
                        JsonData data = eventChain[i];

                        me = addSingleEventLogic(data, eventTable);
                        if (me != null)
                        {
                            if (prevName != null && !me.conditionAndEvents.Contains(prevName))
                            {
                                me.conditionAndEvents.Add(prevName);
                                me.conditionAndEvents.Reverse(); // 保证事件链的事件在最前,是否必要？
                            }

                            if (i > 0)
                            {
                                prevName = me.name;
                            }
                        }
                    }
                }
                else if (alldata.Contains("非链事件") && alldata["非链事件"].IsArray)
                {
                    JsonData nonChainEvents = alldata["非链事件"];
                    foreach (JsonData data in nonChainEvents)
                    {
                        addSingleEventLogic(data, eventTable);
                    }
                }
                else if (alldata.IsObject && alldata.Contains("事件"))
                {
                    // 单个事件
                    addSingleEventLogic(alldata, eventTable);
                }
            }
        }

        /// <summary>
        /// 读入强制事件表
        /// 强制事件表被用于EndTurnNode的判定。
        /// </summary>
        /// <returns></returns>

        //public static Dictionary<string, MapEvent> LoadForceEvents()
        //{
        //    // 读入强制事件
        //    string path = (Constants.DEBUG ? DEBUG_PATH : DEFAULT_PATH) + "ForceEvents/";
        //    Dictionary<string, MapEvent> forceEventTable = new Dictionary<string, MapEvent>();

        //    foreach(TextAsset text in Resources.LoadAll<TextAsset>(path))
        //    {
        //        JsonData alldata = JsonMapper.ToObject(text.text);
        //        if (alldata.IsArray)
        //        {
        //            foreach (JsonData data in alldata)
        //            {
        //                if (data.Contains("事件"))
        //                {
        //                    MapEvent e = new MapEvent(data);
        //                    forceEventTable.Add((string)data["事件"], e);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (alldata.Contains("事件"))
        //            {
        //                MapEvent e = new MapEvent(alldata);
        //                forceEventTable.Add((string)alldata["事件"], e);
        //            }
        //        }
        //    }
        //    return forceEventTable;
        //}

        public static Dictionary<string, int> LoadEventState(
            Dictionary<string, MapEvent> eventTable,
            Dictionary<string, int> eventState = null)
        {
            // 初始化事件状态
            if (eventState == null)
            {
                eventState = new Dictionary<string, int>();
                foreach (KeyValuePair<string, MapEvent> kv in eventTable)
                {
                    eventState.Add(kv.Key, STATE_NOT_RUNNED);
                }
            }
            return eventState;
        }


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
    }
}