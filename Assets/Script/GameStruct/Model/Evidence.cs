using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;
using UnityEngine;

namespace Assets.Script.GameStruct.Model
{
    /// <summary>
    /// 证据类
    /// </summary>
    public class Evidence
    {
        public string UID;

        /// <summary>
        /// 证据名
        /// </summary>
        public string name;

        /// <summary>
        /// 证据简介
        /// </summary>
        public string introduction;

        /// <summary>
        /// 证据 图标/大图 路径
        /// </summary>
        public string iconPath, imagePath;
        private static readonly string ICON_PATH = "Icon/";

        public Evidence(JsonData data)
        {
            UID = (string)data["UID"];
            name = (string)data["证据名"];
            iconPath = ICON_PATH + (string)data["图标"];

            if (data.Contains("大图"))
            {
                imagePath = ICON_PATH + (string)data["大图"];
            }
            
            introduction = (string)data["简介"];
        }

        /// <summary>
        /// 格式化输出
        /// </summary>
        /// <param name="isEng">是否显示变量名</param>
        public string ToString(bool isEng)
        {
            string str = string.Empty;
            str += "UID : " + UID + "\n";
            str += (isEng ? "name" : "证据名") + " : " + name + "\n";
            str += (isEng ? "introduction" : "简介") + " : " + introduction + "\n";

            str += (isEng ? "iconPath" : "图标文件") + " : " + iconPath + "\n";
            str += (isEng ? "imagePath" : "详细图片文件") + " : " + imagePath + "\n";

            return str;
        }
    }
}
