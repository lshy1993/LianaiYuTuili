using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    /// <summary>
    /// 成就结局类
    /// </summary>
    public class AchieveEnding
    {
        public int UID;

        /// <summary>
        /// 成就名
        /// </summary>
        public string achieveName;

        /// <summary>
        /// 成就提示
        /// </summary>
        public string achieveHint;

        public AchieveEnding(JsonData data)
        {
            UID = (int)data["编号"];
            achieveName = (string)data["成就名"];
            achieveHint = (string)data["成就提示"];

        }

        public string ToString(bool isEng)
        {
            string str = string.Empty;
            str += "UID : " + UID + "\n";
            str += (isEng ? "achieveName" : "成就名") + " : " + achieveName + "\n";
            str += (isEng ? "achieveHint" : "正文") + " : " + achieveHint + "\n";
            return str;
        }

    }

}
