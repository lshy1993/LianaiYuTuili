using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    /// <summary>
    /// 微信消息类
    /// </summary>
    public class ChatMessage
    {
        public int UID;

        /// <summary>
        /// 是否为主角回信
        /// </summary>
        public bool isReply;

        /// <summary>
        /// 消息来源角色
        /// </summary>
        public string chara;

        /// <summary>
        /// 正文
        /// </summary>
        public string content;

        /// <summary>
        /// 后继消息类编号
        /// </summary>
        public List<int> nextNum;

        public ChatMessage(JsonData data)
        {
            UID = (int)data["编号"];
            isReply = (bool)data["是否回复"];
            if (!isReply)
            {
                chara = (string)data["角色"];
            }
            content = (string)data["文字"];

            nextNum = new List<int>();
            if (data.Contains("next"))
            {
                
                foreach (JsonData i in data["next"])
                {
                    nextNum.Add((int)i);
                }
            }
            
        }

        public string ToString(bool isEng)
        {
            string str = string.Empty;
            str += "UID : " + UID + "\n";
            str += (isEng ? "isReply" : "是否为回信") + " : " + isReply + "\n";
            if (!isReply) str += (isEng ? "chara" : "来源角色") + " : " + chara + "\n";
            str += (isEng ? "content" : "正文") + " : " + content + "\n";
            str += (isEng ? "nextNum" : "下个信息") + " : ";
            foreach (int i in nextNum)
            {
                str +=i + "  ";
            }
            str += "\n";

            return str;
        }

    }
}
