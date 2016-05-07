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
    public class EventManager : LoadSaveInterface
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

        public MapEvent lastEvent;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            random = new UnityEngine.Random();
            eventPool = EventPool.GetInstance();
            eventPool.Init();
        }

        public void Init(Dictionary<string, List<MapEvent>> eventTable,
            Dictionary<string, List<MapEvent>> currentEvents, Dictionary<string, int> eventPointers)
        {
            eventPool = EventPool.GetInstance();
            eventPool.Init(eventTable, currentEvents, eventPointers);
        }


        /// <summary>
        /// 获取当前地点的事件，没有返回null
        /// </summary>
        /// <param name="pos">地点</param>
        /// <returns>当前地点的事件，没有事件则为null</returns>
        public MapEvent GetCurrentEventAt(string pos)
        {
            eventPool.UpdateEvents();
            return eventPool.currentEvents.ContainsKey(pos) ?
                eventPool.currentEvents[pos][UnityEngine.Random.Range(0, eventPool.currentEvents[pos].Count)] : null;
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
            Player player = (Player)GameManager.GetGlobalVars()["玩家数据"];
            if (player == null) return false;

            // 不满足前置日期
            if (value.conditionTurn.GetMax() < player.GetTime("回合") &&
                value.conditionTurn.GetMin() > player.GetTime("回合"))
            {
                return false;
            }

            // 不满足前置属性
            if (value.conditionStatus != null &&
                value.conditionStatus.Count > 0)
            {
                foreach (KeyValuePair<string, Range> kv in value.conditionStatus)
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
                    if (player.ContainsBasicStatus(kv.Key) &&
                        (kv.Value.GetMin() > player.GetGirlPoint(kv.Key) || player.GetGirlPoint(kv.Key) > kv.Value.GetMax()))
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
            MapEvent e = GetCurrentEventAt(place);

            nextNode = NodeFactory.GetInstance().FindTextScript(e.entryNode);

            MovePointer(e);
            // TODO: 保存一份全局可调用的当前事件副本，在事件结尾脚本手动控制事件结束
            //e.finished = true;
            return nextNode;
        }

        public void Load(GameDataSet data)
        {
            //            throw new NotImplementedException();
        }

        public void Save(GameDataSet data)
        {
            //            throw new NotImplementedException();
        }
    }
}