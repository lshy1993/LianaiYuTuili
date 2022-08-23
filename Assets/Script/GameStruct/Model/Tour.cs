using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    public class Tour
    {
        public int UID;
        public string backFile;
        public string name;
        public string intro;

        public Tour(JsonData data)
        {
            UID = (int)data["编号"];
            backFile = (string)data["背景"];
            name = (string)data["地点"];
            intro = (string)data["介绍"];
        }

        /// <summary>
        /// 格式化输出
        /// </summary>
        /// <param name="isEng">是否显示变量名</param>
        public string ToString(bool isEng)
        {
            string str = string.Empty;
            str += "UID : " + UID + "\n";

            str += (isEng ? "backFile" : "背景文件名") + " : " + backFile + "\n";
            str += (isEng ? "name" : "地点名") + " : " + name + "\n";
            str += (isEng ? "intro" : "介绍") + " : \n" + intro + "\n";

            return str;
        }
    }
}
