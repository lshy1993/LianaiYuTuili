using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    /// <summary>
    /// 自我推理文字选项类
    /// </summary>
    public class ReasoningChoice
    {
        /// <summary>
        /// 选项名
        /// </summary>
        public string text;
        
        /// <summary>
        /// 是否正解
        /// </summary>
        public bool correct;
        
        /// <summary>
        /// 入口脚本名
        /// </summary>
        public string entry;

        public ReasoningChoice(JsonData data)
        {
            text = (string)data["显示"];
            correct = false;
            if (data.Contains("正解")) correct = (bool)data["正解"];
            entry = (string)data["事件入口"];
        }

        public string ToString(bool isEng)
        {
            string str = string.Empty;
            str += "    " + (isEng ? "text" : "选项") + " : " + text + "\n";
            str += "    " + (isEng ? "correct" : "是否为正确答案") + " : " + correct + "\n";
            str += "    " + (isEng ? "entry" : "选项进入脚本") + " : " + entry + "\n";
            return str;
        }

    }
}
