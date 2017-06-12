using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    public class EnquireEvent
    {
        //private static int ID = 0;

        public List<EnquireTestimony> testimony;
        public EnquireBreak enquireBreak;
        public string id, music;

        public string loopExit, wrongExit;

        public EnquireEvent(JsonData data)
        {
            id = (string)data["编号"];
            music = (string)data["音乐"];
            loopExit = (string)data["循环"];
            wrongExit = (string)data["错误"];

            // 处理内容
            JsonData contents = data["内容"];
            testimony = new List<EnquireTestimony>();
            foreach (JsonData content in contents)
            {
                testimony.Add(new EnquireTestimony(content));
            }

            // 处理跳出条件
            JsonData condition = data["跳出"];
            enquireBreak = new EnquireBreak(condition);
        }
    }
}
