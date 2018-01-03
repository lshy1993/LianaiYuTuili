using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    public class DetectDialog
    {
        public string dialog;
        public string info;
        public string entry;
        public List<string> condition;

        public DetectDialog(JsonData data)
        {
            dialog = (string)data["描述"];
            info = dialog;
            entry = (string)data["事件"];

            condition = new List<string>();
            if(data.Contains("前置") && data["前置"] != null)
            {
                foreach (JsonData d in data["前置"])
                    condition.Add((string)d);
            }
        }

        public string ToString(bool isEng)
        {
            string str = string.Empty;
            str += "        " + (isEng ? "dialog" : "描述") + " : " + dialog + "\n";
            str += "        " + (isEng ? "entry" : "事件入口") + " : " + entry + "\n";

            if (condition.Count != 0)
            {
                str += "        " + (isEng ? "condition" : "开启条件") + " : " ;
                foreach(string item in condition)
                {
                    str += item + "  ";
                }
                str += "\n";
            }

            return str;
        }
    }
}
