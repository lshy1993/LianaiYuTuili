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
        /// 前置事件名
        /// </summary>
        public List<string> conditionEvents;


        /// <summary>
        /// 条件回合
        /// </summary>
        public int conditionTurn;

        /// <summary>
        /// 条件状态
        /// </summary>
        public Dictionary<string, int[]> conditionStatus;


        /// <summary>
        /// 是否结束
        /// </summary>
        public bool overFlag;

        public MapEvent()
        {
            conditionEvents = new List<string>();
            conditionStatus = new Dictionary<string, int[]>();
            conditionTurn = 0;
            overFlag = false;
        }



        public override string ToString()
        {
            string str = base.ToString();
            str += ("name : " + name + "\n");

            str += ("conditionEvents : " + "\n");

            foreach(string s in conditionEvents)
            {
                str += (s + " ");
            }

            str += "\n";
            str += ("conditionTurn : " + conditionTurn + "\n");

            str += ("conditionStatus :" + "\n");

            foreach (KeyValuePair<string, int[]> kv in conditionStatus)
            {
                str += (kv.Key + ":" );

                foreach(int i in kv.Value)
                {
                    str += (i + " ");
                }
                str += "\n";
            }
            

            return str;
        }

    }
}
