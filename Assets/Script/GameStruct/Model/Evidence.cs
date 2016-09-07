using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    public class Evidence
    {
        public string name;
        public string iconPath;
        public string imagePath;
        public string introduction;

        public Evidence(JsonData data)
        {
            name = (string)data["证据名"];
            iconPath = (string)data["图标"];
            if (data.Contains("大图")) imagePath = (string)data["大图"];
            introduction = (string)data["简介"];
        }
    }
}
