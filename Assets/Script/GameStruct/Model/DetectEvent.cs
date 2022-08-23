using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    /// <summary>
    /// 侦探事件类
    /// </summary>
    public class DetectEvent
    {
        private static int ID = 0;
        public int id;

        /// <summary>
        /// 子场景节点
        /// </summary>
        public Dictionary<string, DetectPlaceSection> sections;

        /// <summary>
        /// 前置开启条件
        /// </summary>
        public List<string> conditions;

        /// <summary>
        /// 达成后出口脚本名
        /// </summary>
        public string eventExit;

        public DetectEvent(JsonData data)
        {
            JsonData contents = data["内容"],
                     exit = data["出口"];

            sections = new Dictionary<string, DetectPlaceSection>();
            // 处理内容
            foreach (JsonData content in contents)
            {
                DetectPlaceSection section = new DetectPlaceSection(content);
                sections.Add(section.place, section);
            }

            // 处理出口
            eventExit = (string)exit["事件"];

            conditions = new List<string>();
            foreach(JsonData condition in exit["条件"])
            {
                conditions.Add((string)condition);
            }

            id = ID;
            ID++;
        }

        /// <summary>
        /// 格式化输出
        /// </summary>
        /// <param name="isEng">是否显示变量名</param>
        public string ToString(bool isEng)
        {
            string str = string.Empty;
            str += (isEng ? "id" : "编号") + " : " + id + "\n";

            if (sections.Count != 0)
            {
                str += (isEng ? "sections" : "子节点") + " : " + "\n";
                foreach (KeyValuePair<string, DetectPlaceSection> kv in sections)
                {
                    str += kv.Value.ToString(isEng);
                }
            }

            str += (isEng ? "eventExit" : "出口脚本文件") + " : " + eventExit + "\n";

            if (conditions.Count != 0)
            {
                str += (isEng ? "conditions" : "结束条件") + " : ";
                foreach (string item in conditions)
                {
                    str += item + "  ";
                }
                str += "\n";
            }

            return str;
        }

    }
}
