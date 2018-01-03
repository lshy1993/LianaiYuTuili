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
        public string exit;
        public List<ReasoningChoice> choice;
        public ReasoningEvidence answerEvi;

        public ReasoningEvent(JsonData data)
        {
            id = (string)data["ID"];
            question = (string)data["问题"];

            exit = data.Contains("出口") ? (string)data["出口"] : "";

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
                answerEvi = new ReasoningEvidence(data["证据"]);
            }
        }

        /// <summary>
        /// 格式化输出
        /// </summary>
        /// <param name="isEng">是否显示变量名</param>
        public string ToString(bool isEng)
        {
            string str = string.Empty;
            str += (isEng ? "id" : "编号") + " : " + id + "\n";
            str += (isEng ? "question" : "问题") + " : " + question + "\n";
            str += (isEng ? "exit" : "出口") + " : " + exit + "\n";

            if (choice.Count != 0)
            {
                str += (isEng ? "choice" : "文本选项") + " : " + "\n";
                foreach(ReasoningChoice item in choice)
                {
                    str += item.ToString(isEng);
                }
            }
            else
            {
                str += (isEng ? "evidence" : "正确证据") + " : " + "\n";
                str += answerEvi.ToString(isEng);
            }

            return str;
        }


    }
}
