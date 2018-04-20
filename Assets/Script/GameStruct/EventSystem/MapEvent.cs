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
        /// 【唯一】事件名
        /// </summary>
        public string name { set; get; }

        /// <summary>
        /// 事件触发【地点名】
        /// </summary>
        public string position { set; get; }

        /// <summary>
        /// 事件入口文件名
        /// </summary>
        public string entryNode { set; get; }

        /// <summary>
        /// 前置【非】事件列表
        /// </summary>
        public List<string> conditionNotEvents;

        /// <summary>
        /// 前置【与】事件列表
        /// </summary>
        public List<string> conditionAndEvents;

        /// <summary>
        /// 前置【或】事件列表
        /// </summary>
        public List<string> conditionOrEvents;
        
        /// <summary>
        /// 回合数限制
        /// </summary>
        public Range conditionTurn;

        /// <summary>
        /// 其他属性限制【多项】
        /// </summary>
        public Dictionary<string, Range> conditionStatus;

        /// <summary>
        /// 角色好感度限制【多项】
        /// </summary>
        public Dictionary<string, Range> conditionGirls;

        /// <summary>
        /// 是否【默认/可重复】事件
        /// </summary>
        public bool isdefault;

        // <summary>
        // 是否结束
        // </summary>

        public MapEvent(string name, string position, string entryNode)
        {
            this.name = name;
            this.position = position;
            this.entryNode = entryNode;
            conditionAndEvents = new List<string>();
            conditionOrEvents = new List<string>();
            conditionNotEvents = new List<string>();
            conditionStatus = new Dictionary<string, Range>();
            conditionGirls = new Dictionary<string, Range>();
            conditionTurn = new Range(Constants.TURN_MIN, Constants.TURN_MAX);
            isdefault = false;
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
            conditionNotEvents = new List<string>();
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

            // 可重复事件：例如 蹲点失败，加属性事件
            if (data.Contains("默认"))
            {
                isdefault = true;
                //isdefault = (bool)data[""];
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

        /// <summary>
        /// 格式化输出
        /// </summary>
        /// <param name="isEng">是否显示变量名</param>
        public string ToString(bool isEng)
        {
            string str = string.Empty;

            str += (isEng ? "name" : "事件名") + " : " + name + "\n";
            str += (isEng ? "position" : "触发地点") + " : " + position + "\n";
            str += (isEng ? "entryNode" : "入口文件") + " : " + entryNode + "\n";
            str += (isEng ? "conditionTurn" : "回合限制") + " : " + conditionTurn.ToString() + "\n";
            //前置【与】事件
            if (conditionAndEvents.Count() != 0)
            {
                str += (isEng ? "conditionAndEvents" : "前置【与】事件") + " : " + "\n";
                str += "    ";
                foreach (string s in conditionAndEvents)
                {
                    str += (s + " ");
                }
                str += "\n";
            }
            //前置【或】事件
            if (conditionOrEvents.Count() != 0)
            {
                str += (isEng ? "conditionOrEvents" : "前置【或】事件") + " : " + "\n";
                str += "    ";
                foreach (string s in conditionOrEvents)
                {
                    str += (s + " ");
                }
                str += "\n";
            }
            //属性限制
            if(conditionStatus.Count() != 0)
            {
                str += (isEng ? "conditionStatus" : "属性限制") + " : " + "\n";
                foreach (KeyValuePair<string, Range> kv in conditionStatus)
                {
                    str += "    " + kv.Key + " : ";
                    str += kv.Value.ToString() + "\n";
                }
            }
            //好感度限制
            if (conditionGirls.Count() != 0)
            {
                str += (isEng ? "conditionGirls" : "好感度限制") + " : " + "\n";
                foreach (KeyValuePair<string, Range> kv in conditionGirls)
                {
                    str += "    " + kv.Key + " : ";
                    str += kv.Value.ToString() + "\n";
                    //str += ("min: " + kv.Value.GetMin() + " max: " + kv.Value.GetMax() + "\n");
                }
            }

            return str;
        }

    }

    /// <summary>
    /// 自定义范围类
    /// </summary>
    public class Range
    {
        private int min;
        private int max;

        public Range(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        public void SetMin(int value)
        {
            min = value;
        }

        public void SetMax(int value)
        {
            max = value;
        }

        /// <summary>
        /// 返回范围下限
        /// </summary>
        /// <returns></returns>
        public int GetMin()
        {
            return min;
        }

        /// <summary>
        /// 返回范围上限
        /// </summary>
        /// <returns></returns>
        public int GetMax()
        {
            return max;
        }

        public override string ToString()
        {
            //return base.ToString();
            if (min == max) return min.ToString();
            return min + " -- " + max;
        }

    }
}
