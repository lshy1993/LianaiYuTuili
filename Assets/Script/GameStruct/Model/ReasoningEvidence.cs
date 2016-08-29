using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    public class ReasoningEvidence
    {
        public string name, curretEntry, wrongEntry;

        public ReasoningEvidence(JsonData data)
        {
            name = (string)data["证物"];
            curretEntry = (string)data["事件入口"];
            wrongEntry = (string)data["错误入口"];
        }
    }
}
