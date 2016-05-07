using LitJson;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.GameStruct.EventSystem
{


    /// <summary>
    /// 事件池，用于存贮事件相关的表以及相应的更新操作
    /// </summary>
    public class EventPool
    {
        private static EventPool instance;

        private readonly string DEFAULT_PATH = "Text/MapEvents/";
        public static EventPool GetInstance()
        {
            if (instance == null) instance = new EventPool();
            return instance;
        }


        /// <summary>
        /// 以事件链为key的所有事件表
        /// </summary>
        internal Dictionary<string, List<MapEvent>> eventTable;

        /// <summary>
        /// 以地点为key的当前事件表
        /// </summary>
        internal Dictionary<string, List<MapEvent>> currentEvents;

        /// <summary>
        /// 事件指针，存储目前事件的位置
        /// </summary>
        internal Dictionary<string, int> eventPointers;


        private EventPool()
        {
            eventTable = new Dictionary<string, List<MapEvent>>();
            currentEvents = new Dictionary<string, List<MapEvent>>();
            eventPointers = new Dictionary<string, int>();
            Init();
        }

        public void Init()
        {
            InitPointer();
            UpdateEvents();
        }

        /// <summary>
        /// 根据相应的数据初始化
        /// </summary>
        /// <param name="eventTable"></param>
        /// <param name="currentEvents"></param>
        /// <param name="eventPointers"></param>
        public void Init(Dictionary<string, List<MapEvent>> eventTable,
            Dictionary<string, List<MapEvent>> currentEvents,
            Dictionary<string, int> eventPointers)
        {
            this.eventTable = new Dictionary<string, List<MapEvent>>(eventTable);
            this.currentEvents = new Dictionary<string, List<MapEvent>>(currentEvents);
            this.eventPointers = new Dictionary<string, int>(eventPointers);
            UpdateEvents();
        }

        /// <summary>
        /// 更新当前事件列表
        /// </summary>
        internal void UpdateEvents()
        {
            foreach (KeyValuePair<string, List<MapEvent>> kv in currentEvents)
            {
                kv.Value.Clear();
            }

            foreach (KeyValuePair<string, int> kv in eventPointers)
            {
                MapEvent me = eventTable[kv.Key][kv.Value];

                if (!currentEvents.ContainsKey(me.position))
                {
                    currentEvents.Add(me.position, new List<MapEvent>());
                }

                currentEvents[me.position].Add(me);
            }

        }

        /// <summary>
        /// 初始化事件指针
        /// </summary>
        private void InitPointer()
        {
            InitPointer(null);
        }

        internal void InitPointer(Dictionary<string, int> dict)
        {
            if (dict == null)
            {
                foreach (string eventLink in eventTable.Keys)
                {
                    eventPointers.Add(eventLink, 0);
                }
            }
            else
            {
                eventPointers = new Dictionary<string, int>(dict);
            }

            UpdateEvents();
        }


        internal string GetEventLinkName(MapEvent e)
        {
            foreach (KeyValuePair<string, List<MapEvent>> kv in eventTable)
            {
                if (kv.Value.Contains(e)) return kv.Key;
            }
            return null;
        }


        public string GetEventDebugStr()
        {
            string str = "";
            str += "EventTable:\n";
            foreach (KeyValuePair<string, List<MapEvent>> kv in eventTable)
            {
                str += ("  " + kv.Key + "\n");
                foreach (MapEvent e in kv.Value)
                {
                    str += ("    " + e.ToString() + "\n");
                }
            }
            return str;
        }

        private void LoadEvents()
        {
            LoadEvents(DEFAULT_PATH);
        }

        private void LoadEvents(string path)
        {
            foreach (TextAsset text in Resources.LoadAll<TextAsset>(path))
            {
                //Debug.Log("读取：" + text.name);
                eventTable.Add(text.name, ParseJsonToEventList(text.text));
            }
        }

        private List<MapEvent> ParseJsonToEventList(string jsonContent)
        {
            List<MapEvent> list = new List<MapEvent>();

            JsonData alldata = JsonMapper.ToObject(jsonContent)["data"];

            foreach (JsonData data in alldata)
            {
                string name = (string)data["事件"];
                string position = (string)data["地点"];
                string entryNode = (string)data["入口"];
                MapEvent me = new MapEvent(name, position, entryNode);

                if (data.Contains("前置事件"))
                {
                    foreach (JsonData eventName in data["前置事件"])
                    {
                        me.conditionEvents.Add((string)eventName);
                    }
                }

                // 属性
                if (data.Contains("属性条件"))
                {
                    foreach (KeyValuePair<string, JsonData> kv in data["属性条件"])
                    {
                        int min = kv.Value.Contains("最小") ? (int)kv.Value["最小"] : Constants.BASIC_MIN;
                        int max = kv.Value.Contains("最大") ? (int)kv.Value["最大"] : Constants.BASIC_MAX;
                        Range range = new Range(min, max);
                        me.conditionStatus.Add(kv.Key, range);
                    }

                }

                // 回合
                if (data.Contains("回合条件"))
                {
                    JsonData conditionTurn = data["回合条件"];

                    if (conditionTurn.Contains("最小"))
                        me.conditionTurn.SetMin((int)conditionTurn["最小"]);

                    if (conditionTurn.Contains("最大"))
                        me.conditionTurn.SetMax((int)conditionTurn["最大"]);
                }
                if (data.Contains("好感度条件"))
                {
                    foreach (KeyValuePair<string, JsonData> kv in data["好感度条件"])
                    {
                        int min = kv.Value.Contains("最小") ? (int)kv.Value["最小"] : Constants.GIRLS_MIN;
                        int max = kv.Value.Contains("最大") ? (int)kv.Value["最大"] : Constants.GIRLS_MAX;
                        Range range = new Range(min, max);
                        me.conditionGirls.Add(kv.Key, range);
                    }
                }
                list.Add(me);
            }
            return list;
        }

    }
}
