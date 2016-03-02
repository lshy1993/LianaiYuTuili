using Assets.Script.GameStruct.Model;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;
using System;

/// <summary>
/// EventManager
/// 用于管理事件树
/// TODO
/// </summary>

namespace Assets.Script.GameStruct
{
    public class EventManager
    {
        private System.Random random;
        private static EventManager instance;
        private readonly string PATH = "MapEvents/";
        public static EventManager GetInstance()
        {
            if (instance == null) instance = new EventManager();
            return instance;
        }

        private EventManager()
        {
            init();

        }


        /// <summary>
        /// 以事件链为key的所有事件表
        /// </summary>
        private Dictionary<string, List<MapEvent>> eventTable;

        /// <summary>
        /// 以地点为key的当前事件表
        /// </summary>
        private Dictionary<string, List<MapEvent>> currentEvents;


        /// <summary>
        /// 事件指针，存储目前事件的位置
        /// </summary>
        private Dictionary<string, int> eventPointers;


        
        private void init()
        {
            eventTable = new Dictionary<string, List<MapEvent>>();
            currentEvents = new Dictionary<string, List<MapEvent>>();
            eventPointers = new Dictionary<string, int>();
            random = new System.Random();
            LoadEvents();
            initPointer();
            updateEvents();
        }



        /// <summary>
        /// 获取当前地点的事件，没有返回null
        /// </summary>
        /// <param name="pos">地点</param>
        /// <returns>当前地点的事件，没有事件则为null</returns>
        public MapEvent GetCurrentEvent(string pos)
        {
            updateEvents();

            return currentEvents.ContainsKey(pos) ? currentEvents[pos][random.Next(0, currentEvents[pos].Count - 1)] : null;
        }

        /// <summary>
        /// 更新当前事件列表
        /// </summary>
        private void updateEvents()
        {

            foreach(KeyValuePair<string, List<MapEvent>> kv in currentEvents)
            {
                kv.Value.Clear();
            }


            foreach(KeyValuePair<string, int> kv in eventPointers)
            {
                MapEvent me = eventTable[kv.Key][kv.Value];

                if (!currentEvents.ContainsKey(me.position))
                {
                    currentEvents.Add(me.position, new List<MapEvent>());
                }

                currentEvents[kv.Key].Add(me);
            }

        }


        /// <summary>
        /// 初始化事件指针
        /// </summary>
        private void initPointer()
        {
            foreach(string eventLink in eventTable.Keys)
            {
                Debug.Log("Reset event: " + eventLink);
                eventPointers.Add(eventLink, 0); 
            }
        }

        private bool isAvailableEvent(MapEvent value)
        {
            User user = (User)GameManager.GetGlobalVars()["用户数据"];
            if (user == null) return false;

            // 不满足前置日期
            if (value.conditionTurn.GetMax() < user.GetTime("回合") &&
                value.conditionTurn.GetMin() > user.GetTime("回合"))
                return false;

            // 不满足前置属性
            if (value.conditionStatus != null &&
                value.conditionStatus.Count > 0)
            {
                foreach (KeyValuePair<string, Range> kv in value.conditionStatus)
                {
                    if (user.ContainsClass(kv.Key) &&
                        (kv.Value.GetMin() > user.GetClass(kv.Key) || user.GetClass(kv.Key) > kv.Value.GetMax()))
                        return false;

                    if (user.ContainsStatus(kv.Key) &&
                        (kv.Value.GetMin() > user.GetStatus(kv.Key) || user.GetStatus(kv.Key) > kv.Value.GetMax()))
                        return false;
                }
            }

            // 不满足前置事件
            if (value.conditionEvents != null && value.conditionEvents.Count > 0)
            {
                // TODO

            }

            // 不是正确的妹子
            if (value.girls != null && value.girls.Count > 0)
            {
                // 噫
            }


            return true;
        }

        private void LoadEvents()
        {
            if (Directory.GetFiles(PATH).Length < 1)
            {
                Debug.Log("事件路径无效");
                return;
            }



            foreach (string file in Directory.GetFiles(PATH))
            {
                if (Path.GetExtension(file) == ".json")
                {
                    string jsonContent = File.ReadAllText(file);

                    Debug.Log("读取：" + Path.GetFileName(file));

                    eventTable.Add(Path.GetFileName(file), ParseJsonToEventList(jsonContent));
                }

            }
        }

        private List<MapEvent> ParseJsonToEventList(string jsonContent)
        {

            //Dictionary<string, MapEvent> dict = new Dictionary<string, MapEvent>();
            List<MapEvent> list = new List<MapEvent>();

            JsonData alldata = JsonMapper.ToObject(jsonContent)["data"];

            foreach (JsonData data in alldata)
            {
                string name = (string)data["name"];
                string position = (string)data["positon"];
                string entryNode = (string)data["entryNode"];
                MapEvent me = new MapEvent(name, position, entryNode);



                /// 属性
                if (data.Contains("conditionStatus")) 
                {
                    foreach (KeyValuePair<string, JsonData> kv in data["conditionStatus"])
                    {
                        Range range = new Range((int)kv.Value["min"], (int)kv.Value["max"]);
                        me.conditionStatus.Add(kv.Key, range);
                    }

                }

                // 回合
                if (data.Contains("conditionTurn"))
                {
                    JsonData conditionTurn = data["conditionTurn"];

                    if (conditionTurn.Contains("min"))
                        me.conditionTurn.SetMin((int)conditionTurn["min"]);

                    if (conditionTurn.Contains("max"))
                        me.conditionTurn.SetMax((int)conditionTurn["max"]);
                }

                list.Add(me);

            }

            return list;
        }
    }
}