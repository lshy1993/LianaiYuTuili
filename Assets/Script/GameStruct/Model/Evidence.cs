using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;
using UnityEngine;

namespace Assets.Script.GameStruct.Model
{
    public class Evidence
    {
        public string UID;
        public string name;
        public string introduction;

        public string iconPath, imagePath;
        private static readonly string ICON_PATH = "Icon/";

        //public Sprite icon, image;

        public Evidence(JsonData data)
        {
            UID = (string)data["UID"];
            name = (string)data["证据名"];
            iconPath = ICON_PATH + (string)data["图标"];

            //icon = Resources.Load( + iconPath) as Sprite;
            if (data.Contains("大图"))
            {
                imagePath = ICON_PATH + (string)data["大图"];
                //image = Resources.Load(+ imagePath) as Sprite;
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
