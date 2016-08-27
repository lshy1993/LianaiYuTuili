using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    public class EnquireBreak
    {
        public int id;
        public string evidence;
        public string outEvent;
        public List<int> conditions;

        public EnquireBreak(JsonData data)
        {
            outEvent = (string)data["出口"];

            conditions = new List<int>();
            if (data.Contains("全威慑") && data["全威慑"] != null)
            {
                foreach (JsonData d in data["全威慑"]) conditions.Add((int)d);
            }
            else
            {
                id = (int)data["目标编号"];
                evidence = (string)data["所需证据"];
            }
        }
    }
}
