using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    /// <summary>
    /// 帮助词条类
    /// </summary>
    public class Keyword
    {
        public int UID;

        /// <summary>
        /// 词条名
        /// </summary>
        public string name;

        /// <summary>
        /// 介绍用图片路径
        /// </summary>
        public string backFile;

        /// <summary>
        /// 词条解释
        /// </summary>
        public string intro;

        public Keyword(JsonData data)
        {
            UID = (int)data["编号"];
            name = (string)data["词条名"];
            backFile = (string)data["配图"];
            intro = (string)data["解释"];
        }

        /// <summary>
        /// 格式化输出
        /// </summary>
        /// <param name="isEng">是否显示变量名</param>
        public string ToString(bool isEng)
        {
            string str = string.Empty;
            str += "UID : " + UID + "\n";

            str += (isEng ? "name" : "词条名") + " : " + name + "\n";
            str += (isEng ? "backFile" : "配图") + " : " + backFile + "\n";
            str += (isEng ? "intro" : "解释") + " : \n" + intro + "\n";

            return str;
        }
    }
}
