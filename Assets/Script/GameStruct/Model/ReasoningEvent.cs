using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    public class ReasoningEvent
    {
        public string id;
        public string question;
        public List<ReasoningChoice> choice;
        public ReasoningEvidence evidence;

        public ReasoningEvent(JsonData data)
        {
            id = (string)data["ID"];
            question = (string)data["问题"];

            choice = new List<ReasoningChoice>();
            if (data.Contains("选项") && data["选项"] != null)
            {
                foreach(JsonData jd in data["选项"])
                {
                    choice.Add(new ReasoningChoice(jd));
                }
            }

            if (data.Contains("证据") && data["证据"] != null)
            {
                evidence = new ReasoningEvidence(data["证据"]);
            }
        }
    }
}
