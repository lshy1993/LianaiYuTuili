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
    }
}
