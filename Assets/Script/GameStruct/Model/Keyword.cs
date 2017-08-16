using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    public class Keyword
    {
        public int id;
        public string name;
        public string backFile;
        public string intro;

        public Keyword(JsonData data)
        {
            id = (int)data["编号"];
            name = (string)data["词条名"];
            backFile = (string)data["配图"];
            intro = (string)data["解释"];
        }

    }
}
