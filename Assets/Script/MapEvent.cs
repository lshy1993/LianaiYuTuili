using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        /// 前置事件名
        /// </summary>
        public List<string> conditionEvents;

        /// <summary>
        /// 条件回合
        /// </summary>
        public Range conditionTurn;

        /// <summary>
        /// 条件状态
        /// </summary>
        public Dictionary<string, Range> conditionStatus;


        /// <summary>
        /// 可能女主
        /// </summary>
        public List<string> girls;


        /// <summary>
        /// 是否结束
        /// </summary>
        public bool finished;

        public MapEvent(string name, string position, string entryNode)
        {
            this.name = name;
            this.position = position;
            this.entryNode = entryNode;
            conditionEvents = new List<string>();
            conditionStatus = new Dictionary<string, Range>();
            conditionTurn = new Range(Constants.TURN_MIN, Constants.TURN_MAX);
            girls = new List<string>();
            finished = false;
        }
        
        public override string ToString()
        {
            string str = base.ToString();
            str += ("name : " + name + "\n");

            str += ("position: " + position + "\n");

            str += ("conditionEvents : " + "\n");

            foreach(string s in conditionEvents)
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

            str += ("girls:\n");
            foreach(string g in girls)
            {
                str += (g + "\n");
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
