using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    /// <summary>
    /// 单条证词类
    /// </summary>
    public class EnquireTestimony
    {
        /// <summary>
        /// 位置编号
        /// </summary>
        public int id;

        /// <summary>
        /// 正文
        /// </summary>
        public string text;

        /// <summary>
        /// 威慑脚本名
        /// </summary>
        public string pressOut;

        /// <summary>
        /// 出现前置条件（威慑）
        /// </summary>
        public List<int> condition;

        public EnquireTestimony(JsonData content)
        {
            id = (int)content["位置"];
            text = (string)content["证词"];
            pressOut = (string)content["威慑出口"];

            condition = new List<int>();
            if(content.Contains("前置") && content["前置"] != null)
            {
                foreach (JsonData d in content["前置"]) condition.Add((int)d);
            }
        }

        public string ToString(bool isEng)
        {
            string str = string.Empty;
            str += "    " + (isEng ? "id" : "位置") + " : " + id + "\n";
            str += "    " + (isEng ? "text" : "证词") + " : " + text.Replace("\n", "") + "\n";
            str += "    " + (isEng ? "pressOut" : "威慑 进入脚本") + " : " + pressOut + "\n";
            if (condition.Count != 0)
            {
                str += "    " + (isEng ? "condition" : "出现条件") + " : ";
                foreach(int item in condition)
                {
                    str += item + "  ";
                }
                str += "\n";
            }
            return str;
        }

    }
}
