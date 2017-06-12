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
        public string name;
        public string introduction;

        public string iconPath, imagePath;
        private static readonly string ICON_PATH = "Icon/";

        //public Sprite icon, image;

        public Evidence(JsonData data)
        {
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
    }
}
