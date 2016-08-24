using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    public class DetectEvent
    {
        private static int ID = 0;

        public Dictionary<string, DetectPlaceSection> sections;
        public List<string> conditions;
        public int id;
        
        public string eventExit;

        public DetectEvent(JsonData data)
        {
            JsonData contents = data["内容"],
                     exit = data["出口"];

            sections = new Dictionary<string, DetectPlaceSection>();
            // 处理内容
            foreach (JsonData content in contents)
            {
                DetectPlaceSection section = new DetectPlaceSection(content);
                sections.Add(section.place, section);
            }

            // 处理出口
            eventExit = (string)exit["事件"];

            conditions = new List<string>();
            foreach(JsonData condition in exit["条件"])
            {
                conditions.Add((string)condition);
            }

            id = ID;
            ID++;
        }
    }
}
