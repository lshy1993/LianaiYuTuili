using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    /// <summary>
    /// MapEvent 
    /// 大地图事件
    /// </summary>
    public class MapEvent
    {
        /// <summary>
        /// 事件名
        /// </summary>
        public string name { set; get; }


        /// <summary>
        /// 事件发生地点
        /// </summary>
        public string position { set; get; }


        /// <summary>
        /// 事件入口文件名
        /// </summary>
        public string entryNode { set; get; }

        /// <summary>
        /// 前置与事件
        /// </summary>
        public List<string> conditionAndEvents;

        /// <summary>
        /// 前置或事件
        /// </summary>
        public List<string> conditionOrEvents;

        /// <summary>
        /// 条件回合
        /// </summary>
        public Range conditionTurn;

        /// <summary>
        /// 条件状态
        /// </summary>
        public Dictionary<string, Range> conditionStatus;


        /// <summary>
        /// 好感度条件
        /// </summary>
        public Dictionary<string, Range> conditionGirls;


        /// <summary>
        /// 是否结束
        /// </summary>

        public MapEvent(string name, string position, string entryNode)
        {
            this.name = name;
            this.position = position;
            this.entryNode = entryNode;
            conditionAndEvents = new List<string>();
            conditionOrEvents = new List<string>();
            conditionStatus = new Dictionary<string, Range>();
            conditionGirls = new Dictionary<string, Range>();
            conditionTurn = new Range(Constants.TURN_MIN, Constants.TURN_MAX);
       }

        /// <summary>
        /// 根据事件定义文件来创建一个事件
        /// 事件定义见tower
        /// </summary>
        public MapEvent(JsonData data)
        {
            name = (string)data["事件"];
            entryNode = (string)data["入口"];
            conditionAndEvents = new List<string>();
            conditionOrEvents = new List<string>();
            conditionStatus = new Dictionary<string, Range>();
            conditionGirls = new Dictionary<string, Range>();
            conditionTurn = new Range(Constants.TURN_MIN, Constants.TURN_MAX);


            if (data.Contains("地点"))
            {
                position = (string)data["地点"];
            }
            else
            {
                // 强制事件没有地点
                position = null;
            }

            // 属性
            if (data.Contains("属性条件"))
            {
                foreach (KeyValuePair<string, JsonData> kv in data["属性条件"])
                {
                    int min = kv.Value.Contains("最小") ? (int)kv.Value["最小"] : Constants.BASIC_MIN;
                    int max = kv.Value.Contains("最大") ? (int)kv.Value["最大"] : Constants.BASIC_MAX;
                    Range range = new Range(min, max);
                    conditionStatus.Add(kv.Key, range);
                }
            }

            // 回合
            if (data.Contains("回合条件"))
            {
                JsonData turn = data["回合条件"];

                if (turn.Contains("最小"))
                    conditionTurn.SetMin((int)turn["最小"]);

                if (turn.Contains("最大"))
                    conditionTurn.SetMax((int)turn["最大"]);
            }

            if (data.Contains("好感度条件"))
            {
                foreach (KeyValuePair<string, JsonData> kv in data["好感度条件"])
                {
                    int min = kv.Value.Contains("最小") ? (int)kv.Value["最小"] : Constants.GIRLS_MIN;
                    int max = kv.Value.Contains("最大") ? (int)kv.Value["最大"] : Constants.GIRLS_MAX;
                    Range range = new Range(min, max);
                    conditionGirls.Add(kv.Key, range);
                }
            }
        }
        
        public override string ToString()
        {
            string str = base.ToString();
            str += ("name : " + name + "\n");

            str += ("position: " + position + "\n");

            str += ("conditionAndEvents : " + "\n");

            foreach(string s in conditionAndEvents)
            {
                str += (s + " ");
            }

            str += ("conditionOrEvents : " + "\n");

            foreach(string s in conditionOrEvents)
            {
                str += (s + " ");
            }


            str += "\n";

            str += ("conditionTurn : " + conditionTurn + "\n");

            str += ("conditionStatus :" + "\n");

            foreach (KeyValuePair<string, Range> kv in conditionStatus)
            {
                str += (kv.Key + ": " );

                str += ("min: " + kv.Value.GetMin() + " max: " + kv.Value.GetMax() + "\n");

            }

            foreach (KeyValuePair<string, Range> kv in conditionGirls)
            {
                str += (kv.Key + ": " );

                str += ("min: " + kv.Value.GetMin() + " max: " + kv.Value.GetMax() + "\n");

            }


            return str;
        }

    }


    public class Range
    {
        private int min;
        private int max;
        public Range(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        public void SetMin(int min)
        {
            this.min = min;
        }

        public void SetMax(int max)
        {
            this.max = max;
        }

        public int GetMin()
        {
            return min;
        }

        public int GetMax()
        {
            return max;
        }
    }
}
