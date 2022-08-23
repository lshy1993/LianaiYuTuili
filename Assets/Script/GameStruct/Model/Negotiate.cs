using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    /// <summary>
    /// 对峙选项类
    /// </summary>
    public class NegotiateSelection
    {
        /// <summary>
        /// 选项文字
        /// </summary>
        public string selectName;

        /// <summary>
        /// 对应语句
        /// </summary>
        public int negotiateNum;

        /// <summary>
        /// 是否隐藏（需要话题列表才显示）
        /// </summary>
        public bool isHide;

        public NegotiateSelection(JsonData data)
        {
            selectName = (string)data["名字"];

            negotiateNum = (int)data["后继"];

            isHide = data.Contains("隐藏");
        }
    }

    /// <summary>
    /// 对峙类
    /// </summary>
    public class Negotiate
    {
        public int UID;

        /// <summary>
        /// 是否为主角发言
        /// </summary>
        public bool isReply;

        /// <summary>
        /// 正文
        /// </summary>
        public string content;

        /// <summary>
        /// 背景移动量
        /// </summary>
        public int move;

        /// <summary>
        /// 自动后继对话编号
        /// </summary>
        public int nextNum;

        /// <summary>
        /// 后继选项
        /// </summary>
        public List<NegotiateSelection> nextSelect;


        public Negotiate(JsonData data)
        {
            UID = (int)data["UID"];

            isReply = false;
            if (data.Contains("主角发言")) isReply = (bool)data["主角发言"];

            content = (string)data["文字"];

            if (data.Contains("移动")) move = (int)data["移动"];

            if (data.Contains("后继")) nextNum = (int)data["后继"];

            nextSelect = new List<NegotiateSelection>();
            if (data.Contains("选项"))
            {
                foreach (JsonData i in data["选项"])
                {
                    nextSelect.Add(new NegotiateSelection(i));
                }
            }

        }

        public string ToString(bool isEng)
        {
            string str = string.Empty;
            str += "UID : " + UID + "\n";
            str += (isEng ? "isReply" : "是否为主角发言") + " : " + isReply + "\n";
            str += (isEng ? "content" : "文字") + " : " + content + "\n";
            str += (isEng ? "nextNum" : "自动后继句") + " : ";
            str += "\n";

            return str;
        }

    }
}
