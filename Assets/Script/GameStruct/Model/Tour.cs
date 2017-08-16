using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    public class Tour
    {
        public int id;
        public string backFile;
        public string name;
        public string intro;

        public Tour(JsonData data)
        {
            id = (int)data["编号"];
            backFile = (string)data["背景"];
            name = (string)data["地点"];
            intro = (string)data["介绍"];
        }
    }
}
