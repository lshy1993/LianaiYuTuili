using Assets.Script.GameStruct.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LitJson;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public class EventManager
    {
        private static EventManager instance;
        private readonly string PATH = "MapEvents/";
        public static EventManager GetInstance()
        {
            if(instance == null) instance = new EventManager();
            return instance;
        }

        private EventManager()
        {
            LoadEvents();
        }


        private Dictionary<string, Dictionary<string, MapEvent>> eventTable;

        /// <summary>
        /// 获取当前可以进行的事件列表
        /// </summary>
        /// <param name="pos">位置</param>
        /// <returns>事件列表</returns>
        public List<MapEvent> GetAvailableEvents(string pos)
        {
            if (!eventTable.ContainsKey(pos)) throw new KeyNotFoundException();
            List<MapEvent> temp = new List<MapEvent>();

            foreach(KeyValuePair<string, MapEvent> kv in eventTable[pos])
            {
                if (isAvailableEvent(kv.Value)) temp.Add(kv.Value);
            }

            return temp;
        }

        private bool isAvailableEvent(MapEvent value)
        {
            User user = (User)GameManager.GetGlobalVars()["用户数据"];
            if (user == null) return false;

            // 不满足前置日期
            if (value.conditionTurn > user.GetTime("回合")) return false;

            // 不满足前置属性
            if (value.conditionStatus != null &&
                value.conditionStatus.Count > 0)
            {
                foreach (KeyValuePair<string, int[]> kv in value.conditionStatus)
                {
                    if (user.ContainsClass(kv.Key) &&
                        (kv.Value[0] > user.GetClass(kv.Key) || user.GetClass(kv.Key) > kv.Value[1]))
                        return false;

                    if (user.ContainsStatus(kv.Key) &&
                        (kv.Value[0] > user.GetStatus(kv.Key) || user.GetStatus(kv.Key) > kv.Value[1]))
                        return false;
                }
            }

            // 不满足前置事件
            if (value.conditionEvents != null && value.conditionEvents.Count > 0)
            {
                foreach (string name in value.conditionEvents)
                {
                    foreach (Dictionary<string, MapEvent> d in eventTable.Values)
                    {
                        if (d.ContainsKey(name) && !d[name].overFlag) return false;
                    }
                }
            }

            return true;
        }

        public void LoadEvents()
        {
            if (Directory.GetFiles(PATH).Length < 1)
            {
                Debug.Log("事件路径无效");
            }

            foreach(string file in Directory.GetFiles(PATH))
            {
                if (Path.GetExtension(file) == ".json")
                {
                    string jsonContent = File.ReadAllText(file);

                    Debug.Log("读取：" + Path.GetFileName(file));

                    eventTable.Add(Path.GetFileName(file), ParseJson(jsonContent));
                }

            }
        }

        private Dictionary<string, MapEvent> ParseJson(string jsonContent)
        {
            Dictionary<string, MapEvent> dict = new Dictionary<string, MapEvent>();

            JsonData alldata = JsonMapper.ToObject(jsonContent)["data"];

            foreach(JsonData data in alldata)
            {
                MapEvent me = new MapEvent();

                string name = (string)data["test1"];

                me.name = name;

                if (data.Contains("conditionEvents"))
                {
                    JsonData conditionEvents = data["conditionEvents"];
                    for(int i = 0; i < conditionEvents.Count; i++)
                    {
                        me.conditionEvents.Add((string) conditionEvents[i]);
                    }

                }

                if (data.Contains("conditionStatus"))
                {
                    foreach(KeyValuePair<string, JsonData> kv in data["conditionStatus"])
                    {
                        int[] arr = new int[2];
                        arr[0] = (int)kv.Value[0];
                        arr[1] = (int)kv.Value[1];
                        me.conditionStatus.Add(kv.Key, arr);
                    }

                }

                if(data.Contains("conditionTurn"))
                me.conditionTurn = (int)data["conditionTurn"];

                dict.Add(name, me);

            }

            return dict;
        }
    }
}
