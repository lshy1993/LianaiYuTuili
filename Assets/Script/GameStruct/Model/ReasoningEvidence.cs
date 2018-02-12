using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    /// <summary>
    /// 自我推理证物选项类
    /// </summary>
    public class ReasoningEvidence
    {
        /// <summary>
        /// 正确/错误 进入脚本名
        /// </summary>
        public string curretEntry, wrongEntry;
        
        /// <summary>
        /// 需要选择的证据
        /// </summary>
        public List<string> evi;

        public ReasoningEvidence(JsonData data)
        {
            //name = (string)data["证物"];
            evi = new List<string>();
            foreach(JsonData item in data["证物"])
            {
                evi.Add((string)item);
            }
            curretEntry = (string)data["事件入口"];
            wrongEntry = (string)data["错误入口"];
        }

        public string ToString(bool isEng)
        {
            string str = string.Empty;
            str += "    " + (isEng ? "evi" : "需要证据") + " : ";
            foreach(string item in evi)
            {
                str += evi + "  ";
            }
            str += "\n";
            str += "    " + (isEng ? "curretEntry" : "正确进入脚本") + " : " + curretEntry + "\n";
            str += "    " + (isEng ? "wrongEntry" : "错误进入脚本") + " : " + wrongEntry + "\n";
            return str;
        }
    }
}
