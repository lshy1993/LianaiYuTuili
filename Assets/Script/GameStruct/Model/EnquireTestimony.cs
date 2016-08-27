using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    public class EnquireTestimony
    {
        public int id;
        public string text;
        public string pressOut;
        public List<int> condition;

        public EnquireTestimony(JsonData content)
        {
            id = (int)content["位置"];
            text = (string)content["证词"];
            pressOut = (string)content["威慑出口"];

            condition = new List<int>();
            if(content.Contains("前置") && content["前置"] != null)
            {
                foreach (JsonData d in content["前置"]) condition.Add((int)d);
            }
        }
    }
}
