﻿using Assets.Script.GameStruct.Model;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

/// <summary>
/// EventManager
/// 用于管理事件树
/// TODO
/// </summary>

namespace Assets.Script.GameStruct
{
    public class EventManager
    {
        private UnityEngine.Random random;
        private static EventManager instance;
        private readonly string PATH = "Text/MapEvents/";
        public static EventManager GetInstance()
        {
            if (instance == null) instance = new EventManager();
            return instance;
        }

        private EventManager()
        {
            Init();
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


        
        private void Init()
        {
            Debug.Log("Init Event Manager");
            eventTable = new Dictionary<string, List<MapEvent>>();
            currentEvents = new Dictionary<string, List<MapEvent>>();
            eventPointers = new Dictionary<string, int>();
            //random = new System.Random();
            random = new Random();
            LoadEvents();
            initPointer();
            updateEvents();
            Debug.Log(GetEventDebugStr());
        }


        /// <summary>
        /// 获取当前地点的事件，没有返回null
        /// </summary>
        /// <param name="pos">地点</param>
        /// <returns>当前地点的事件，没有事件则为null</returns>
        public MapEvent GetCurrentEvent(string pos)
        {
            updateEvents();
            return currentEvents.ContainsKey(pos) ? currentEvents[pos][Random.Range(0, currentEvents[pos].Count)] : null;
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

                currentEvents[me.position].Add(me);
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

        private void MovePointer(MapEvent e)
        {
            string eventLink = GetEventLinkName(e);
            if (eventLink != null)
            {
                //eventPointers[place] = eventPointers[place] + 1;
                eventPointers[eventLink]++;
            }
            else
            {
                Debug.LogError("找不到事件对应的事件链: " + e.name);
            }
        }

        private string GetEventLinkName(MapEvent e)
        {

            foreach(KeyValuePair<string, List<MapEvent>> kv in eventTable)
            {
                if (kv.Value.Contains(e)) return kv.Key;
            }
            return null;
        }


        public MapEvent GetEventByName(string name)
        {
            foreach (List<MapEvent> eventLink in eventTable.Values)
            {
                foreach(MapEvent e in eventLink)
                {
                    if (e.name.Equals(name))
                    {
                        return e;
                    }
                }
            }

            return null;
        }

        private bool isAvailableEvent(MapEvent value)
        {
            User user = (User)GameManager.GetGlobalVars()["用户数据"];
            if (user == null) return false;

            // 不满足前置日期
            if (value.conditionTurn.GetMax() < user.GetTime("回合") &&
                value.conditionTurn.GetMin() > user.GetTime("回合"))
            {
                return false;
            }

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
                foreach(string eventName in value.conditionEvents)
                {
                    if (!GetEventByName(eventName).finished)
                    {
                        return false;
                    }
                }
            }

            // 不是正确的妹子
            if (value.girls != null && value.girls.Count > 0)
            {
                // 噫
            }


            return true;
        }

        public GameNode RunEvent(string place)
        {
            GameNode nextNode = null;
            MapEvent e = GetCurrentEvent(place);

            if (e.entryNode.Equals("WeekNode"))
            {
                // TODO 
            }
            else
            {
                nextNode = NodeFactory.GetInstance().FindTextScript(e.entryNode);
            }

            MovePointer(e);
            e.finished = true;
            return nextNode;
        }

        private void LoadEvents()
        {

            foreach (TextAsset text in Resources.LoadAll<TextAsset>(PATH))
            {
                Debug.Log("读取：" + text.name);

                eventTable.Add(text.name, ParseJsonToEventList(text.text));
            }
        }

        private List<MapEvent> ParseJsonToEventList(string jsonContent)
        {

            List<MapEvent> list = new List<MapEvent>();

            JsonData alldata = JsonMapper.ToObject(jsonContent)["data"];

            foreach (JsonData data in alldata)
            {
                string name = (string)data["name"];
                string position = (string)data["position"];
                string entryNode = (string)data["entryNode"];
                MapEvent me = new MapEvent(name, position, entryNode);

                if (data.Contains("conditionEvents"))
                {
                    foreach(JsonData eventName in data["conditionEvents"])
                    {
                        me.conditionEvents.Add((string)eventName);
                    }
                }


                // 属性
                if (data.Contains("conditionStatus")) 
                {
                    foreach (KeyValuePair<string, JsonData> kv in data["conditionStatus"])
                    {
                        int min = kv.Value.Contains("min") ? (int)kv.Value["min"] :  Constants.STATUS_MIN;
                        int max = kv.Value.Contains("max") ? (int)kv.Value["max"] :  Constants.STATUS_MAX;
                        Range range = new Range(min, max);
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

        public string GetEventDebugStr()
        {
            string str = "";


            str += "EventTable:\n";
            foreach(KeyValuePair<string, List<MapEvent>> kv in eventTable){
                str += ("  " + kv.Key + "\n");
                foreach(MapEvent e in kv.Value)
                {
                    str += ("    " + e.ToString() + "\n");
                }

            }

            return str;
        }
    }
}