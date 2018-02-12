using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    /// <summary>
    /// 考试题库类
    /// </summary>
    public class Question
    {
        public int UID;

        /// <summary>
        /// 难度等级
        /// </summary>
        public int hard;

        /// <summary>
        /// 科目
        /// </summary>
        public string subject;

        /// <summary>
        /// 题型
        /// </summary>
        public bool isTorF;

        /// <summary>
        /// 题干
        /// </summary>
        public string content;

        /// <summary>
        /// 选项文字
        /// </summary>
        public List<string> choice;

        /// <summary>
        /// 正确答案编号
        /// </summary>
        public int answer;

        public Question(JsonData data)
        {
            UID = (int)data["编号"];
            hard = (int)data["难度"];
            subject = (string)data["科目"];
            isTorF = (bool)data["题型"];
            content = (string)data["题干"];

            choice = new List<string>();
            foreach(JsonData d in data["选项"])
            {
                choice.Add((string)d);
            }

            answer = (int)data["答案"];
        }

        public string ToString(bool isEng)
        {
            string str = string.Empty;
            str += "UID : " + UID + "\n";
            str += (isEng ? "hard" : "难度") + " : " + hard + "\n";
            str += (isEng ? "subject" : "科目") + " : " + subject + "\n";
            str += (isEng ? "isTorF" : "是否为正误题") + " : " + isTorF + "\n";
            str += (isEng ? "content" : "题干") + " : " + content + "\n";
            str += (isEng ? "choice" : "选项") + " : ";

            foreach (string x in choice)
            {
                str += x + "  ";
            }
            str += "\n";

            str += (isEng ? "answer" : "正确答案") + " : " + answer + "\n";
            return str;
        }
    }
}
