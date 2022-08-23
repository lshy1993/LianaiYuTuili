using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    /// <summary>
    /// 侦探对话类
    /// </summary>
    public class DetectDialog
    {
        /// <summary>
        /// 对话名
        /// </summary>
        public string title;

        /// <summary>
        /// 入口脚本名
        /// </summary>
        public string entry;

        /// <summary>
        /// 前置条件（对话名）
        /// </summary>
        public List<string> condition;

        public DetectDialog(JsonData data)
        {
            title = (string)data["描述"];
            entry = (string)data["事件"];

            condition = new List<string>();
            if(data.Contains("前置") && data["前置"] != null)
            {
                foreach (JsonData d in data["前置"])
                    condition.Add((string)d);
            }
        }

        public string ToString(bool isEng)
        {
            string str = string.Empty;
            str += "        " + (isEng ? "title" : "对话名") + " : " + title + "\n";
            str += "        " + (isEng ? "entry" : "事件入口") + " : " + entry + "\n";

            if (condition.Count != 0)
            {
                str += "        " + (isEng ? "condition" : "开启条件") + " : " ;
                foreach(string item in condition)
                {
                    str += item + "  ";
                }
                str += "\n";
            }

            return str;
        }
    }
}
