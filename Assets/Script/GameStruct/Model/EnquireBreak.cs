using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    public class EnquireBreak
    {
        public List<int> id;
        public List<string> evidence;
        public string outEvent;
        public List<int> conditions;

        public EnquireBreak(JsonData data)
        {
            id = new List<int>();
            evidence = new List<string>();
            conditions = new List<int>();

            outEvent = (string)data["出口"];

            if (data.Contains("全威慑") && data["全威慑"] != null)
            {
                foreach (JsonData d in data["全威慑"]) conditions.Add((int)d);
            }
            else
            {
                foreach (JsonData d in data["目标编号"]) id.Add((int)d);
                foreach (JsonData d in data["所需证据"]) evidence.Add((string)d);
            }
        }

        public string ToString(bool isEng)
        {
            string str = string.Empty;
            if (conditions.Count != 0)
            {
                str += "    " + (isEng ? "conditions" : "需要威慑的证词") + " : " ;
                foreach(int item in conditions)
                {
                    str += item + "  ";
                }
                str += "\n";
            }

            if (id.Count != 0)
            {
                str += "    " + (isEng ? "id" : "指证证词编号") + " : ";
                foreach (int item in id) str += item + "  ";
                str += "\n";
            }

            if (evidence.Count != 0)
            { 
                str += "    " + (isEng ? "evidence" : "所需证据") + " : ";
                foreach (string item in evidence) str += item + "  ";
                str += "\n";
            }
           
            return str;
        }

    }
}
