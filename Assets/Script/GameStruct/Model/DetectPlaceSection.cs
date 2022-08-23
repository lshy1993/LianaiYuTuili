using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.GameStruct.Model
{
    /// <summary>
    /// 单一场景 侦探事件类
    /// </summary>
    public class DetectPlaceSection
    {
        /// <summary>
        /// 地点名
        /// </summary>
        public string place;

        /// <summary>
        /// 首次进入地点触发脚本名
        /// </summary>
        public string entry;

        /// <summary>
        /// 场景背景图片
        /// </summary>
        public string imagename;

        /// <summary>
        /// 默认角色图片
        /// </summary>
        public string charaimage;

        /// <summary>
        /// 对话类
        /// </summary>
        public List<DetectInvest> invests;

        /// <summary>
        /// 现场调查类
        /// </summary>
        public List<DetectDialog> dialogs;

        /// <summary>
        /// 移动目的地
        /// </summary>
        public List<string> moves;

        public DetectPlaceSection(JsonData data)
        {
            place = (string)data["地点"];
            imagename = (string)data["背景图片"];

            if (data.Contains("角色图片")) charaimage = (string)data["角色图片"];
            
            if (data.Contains("进入事件")) entry = (string)data["进入事件"];
            
            dialogs = new List<DetectDialog>();
            invests = new List<DetectInvest>();
            moves = new List<string>();

            if (data.Contains("调查")
                && data["调查"] != null
                && data["调查"].Count > 0)
            {
                foreach (JsonData inv in data["调查"]) invests.Add(new DetectInvest(inv));
                //invests.Add(new DetectInvest(data["调查"]));
            }

            if (data.Contains("对话")
                && data["对话"] != null
                && data["对话"].Count > 0)
            {
                foreach (JsonData dia in data["对话"]) dialogs.Add(new DetectDialog(dia));
                //dialogs.Add(new DetectDialog(data["对话"]));
            }

            if (data.Contains("移动")
                && data["移动"] != null
                && data["移动"].Count > 0)
            {
                foreach (JsonData mov in data["移动"]) moves.Add((string)mov);
                //moves.Add((string)data["移动"]);
            }
        }

        public string ToString(bool isEng)
        {
            string str = string.Empty;
            str += "    " + (isEng ? "place" : "地点") + " : " + place + "\n";
            str += "    " + (isEng ? "imagename" : "背景图片名") + " : " + imagename + "\n";
            if (entry != "")
            {
                str += "    " + (isEng ? "entry" : "初次进入脚本文件") + " : " + entry + "\n";
            }

            if (dialogs.Count != 0)
            {
                str += "    " + (isEng ? "dialogs" : "对话项") + " : ";
                foreach(DetectDialog item in dialogs)
                {
                    str += "\n" + item.ToString(isEng);
                }
            }

            if (invests.Count != 0)
            {
                str += "    " + (isEng ? "invests" : "调查项") + " : ";
                foreach (DetectInvest item in invests)
                {
                    str += "\n" + item.ToString(isEng);
                }
            }

            if (moves.Count != 0)
            {
                str += "    " + (isEng ? "moves" : "移动项") + " : ";
                foreach (string item in moves)
                {
                    str += item + "  ";
                }
                str += "\n";
            }
            str += "\n";
            return str;
        }

    }
}
