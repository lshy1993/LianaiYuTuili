using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    public class ReasoningChoice
    {
        public string text, entry;
        public bool correct;

        public ReasoningChoice(JsonData data)
        {
            text = (string)data["显示"];
            correct = false;
            if (data.Contains("正解")) correct = (bool)data["正解"];
            entry = (string)data["事件入口"];
        }
    }
}
