using Assets.Script.GameStruct.Model;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

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
        private EventPool eventPool;
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
        /// 初始化
        /// </summary>
        public void Init()
        {
            random = new Random();
            eventPool = EventPool.GetInstance();
            eventPool.Init();
            //LoadEvents();
            //initPointer();
            //updateEvents();
            //Debug.Log(GetEventDebugStr());
        }


        /// <summary>
        /// 获取当前地点的事件，没有返回null
        /// </summary>
        /// <param name="pos">地点</param>
        /// <returns>当前地点的事件，没有事件则为null</returns>
        public MapEvent GetCurrentEvent(string pos)
        {
            eventPool.UpdateEvents();
            return eventPool.currentEvents.ContainsKey(pos) ?
                eventPool.currentEvents[pos][Random.Range(0, eventPool.currentEvents[pos].Count)] : null;
        }

        private void MovePointer(MapEvent e)
        {
            string eventLink = eventPool.GetEventLinkName(e);
            if (eventLink != null)
            {
                eventPool.eventPointers[eventLink]++;
            }
            else
            {
                Debug.LogError("找不到事件对应的事件链: " + e.name);
            }
        }

        public MapEvent GetEventByName(string name)
        {
            foreach (List<MapEvent> eventLink in eventPool.eventTable.Values)
            {
                foreach (MapEvent e in eventLink)
                {
                    if (e.name.Equals(name))
                    {
                        return e;
                    }
                }
            }

            return null;
        }

        private bool IsAvailableEvent(MapEvent value)
        {
            Player user = (Player)GameManager.GetGlobalVars()["用户数据"];
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
                    if (user.ContainsBasicStatus(kv.Key) &&
                        (kv.Value.GetMin() > user.GetBasicStatus(kv.Key) || user.GetBasicStatus(kv.Key) > kv.Value.GetMax()))
                        return false;

                    if (user.ContainsLogicStatus(kv.Key) &&
                        (kv.Value.GetMin() > user.GetLogicStatus(kv.Key) || user.GetLogicStatus(kv.Key) > kv.Value.GetMax()))
                        return false;
                }
            }

            // 不满足前置事件
            if (value.conditionEvents != null && value.conditionEvents.Count > 0)
            {
                foreach (string eventName in value.conditionEvents)
                {
                    if (!GetEventByName(eventName).finished)
                    {
                        return false;
                    }
                }
            }

            // 好感度
            if (value.conditionGirls != null && value.conditionGirls.Count > 0)
            {
                foreach (KeyValuePair<string, Range> kv in value.conditionGirls)
                {
                    if (user.ContainsBasicStatus(kv.Key) &&
                        (kv.Value.GetMin() > user.GetGirlPoint(kv.Key) || user.GetGirlPoint(kv.Key) > kv.Value.GetMax()))
                        return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 运行在某个地点的事件，使之可以
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        public GameNode RunEvent(string place)
        {
            GameNode nextNode = null;
            MapEvent e = GetCurrentEvent(place);

            nextNode = NodeFactory.GetInstance().FindTextScript(e.entryNode);

            MovePointer(e);
            // TODO: 保存一份全局可调用的当前事件副本，在事件结尾脚本手动控制事件结束
            //e.finished = true;
            return nextNode;
        }

    }
}