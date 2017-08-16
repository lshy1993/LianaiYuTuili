using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    public class Routine
    {
        public int round;
        public List<string> routines;
        public bool isHoliday;

        public Routine(JsonData data)
        {
            round = (int)data["回合"];
            routines = new List<string>();
            isHoliday = data.Contains("假期");
            foreach(JsonData da in data["日程"])
            {
                string str = (string)da;
                routines.Add(str);
            }
        }

        public override string ToString()
        {
            string result = "";
            foreach(string str in routines)
            {
                result += str;
            }
            return result;
        }
    }
}
