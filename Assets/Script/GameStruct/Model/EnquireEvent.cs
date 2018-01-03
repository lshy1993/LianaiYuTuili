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

        /// <summary>
        /// 格式化输出
        /// </summary>
        /// <param name="isEng">是否显示变量名</param>
        public string ToString(bool isEng)
        {
            string str = string.Empty;
            str += (isEng ? "id" : "编号") + " : " + id + "\n";
            str += (isEng ? "music" : "音乐") + " : " + music + "\n";
            if (testimony.Count != 0)
            {
                str += (isEng ? "testimony" : "证词列表") + " : " + "\n";
                foreach (EnquireTestimony item in testimony)
                {
                    str += item.ToString(isEng);
                }
            }
            str += (isEng ? "enquireBreak" : "结束询问的条件") + " : " + "\n";
            str += (isEng ? "loopExit" : "自循环 进入脚本") + " : " + loopExit + "\n";
            str += (isEng ? "wrongExit" : "指证错误后 进入脚本") + " : " + wrongExit + "\n";
            str += enquireBreak.ToString(isEng);
            return str;
        }

    }
}
