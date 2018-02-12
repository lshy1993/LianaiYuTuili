using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct.Model
{
    /// <summary>
    /// 现场调查类
    /// </summary>
    public class DetectInvest
    {
        /// <summary>
        /// 调查点名称
        /// </summary>
        public string info;

        /// <summary>
        /// 坐标
        /// </summary>
        public Vector3 coordinate;

        /// <summary>
        /// 按钮正常/悬停图片名
        /// </summary>
        public string normal, hover;

        /// <summary>
        /// 入口脚本名
        /// </summary>
        public string entry;

        /// <summary>
        /// 出现前置条件
        /// </summary>
        public List<string> condition;

        public DetectInvest(JsonData data)
        {
            //position = (string)data["名称"];
            info = (string)data["名称"];
            coordinate = new Vector3((int)data["坐标"][0], (int)data["坐标"][1]);

            normal = (string)data["正常图片"];
            hover = (string)data["悬停图片"];

            entry = (string)data["事件"];
            condition = new List<string>();
            if (data.Contains("前置") && data["前置"] != null)
            {
                foreach (JsonData d in data["前置"]) condition.Add((string)d);
            }
        }

        public string ToString(bool isEng)
        {
            string str = string.Empty;
            str += "        " + (isEng ? "info" : "名称") + " : " + info + "\n";
            str += "        " + (isEng ? "coordinate" : "坐标") + " : " + ((Vector2)coordinate).ToString() + "\n";
            if (normal != "")
            {
                str += "        " + (isEng ? "normal" : "按钮图标文件") + " : " + normal + "\n";
            }
            if (hover != "")
            {

                str += "        " + (isEng ? "hover" : "悬停图标文件") + " : " + hover + "\n";
            }
            str += "        " + (isEng ? "entry" : "入口脚本文件") + " : " + entry + "\n";

            if (condition.Count != 0)
            {
                str += "        " + (isEng ? "condition" : "前置条件") + " : ";
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
