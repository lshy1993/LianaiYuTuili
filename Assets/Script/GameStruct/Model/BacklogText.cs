using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.GameStruct.Model
{
    /// <summary>
    /// 文字履历块类
    /// </summary>
    public class BacklogText
    {
        /// <summary>
        /// 角色名
        /// </summary>
        public string charaName;
        
        /// <summary>
        /// 文本
        /// </summary>
        public string mainContent;

        /// <summary>
        /// 语音路径
        /// </summary>
        public string voicePath;

        public BacklogText(string cName, string mContent, string vFile = "")
        {
            this.charaName = cName;
            this.mainContent = mContent;
            this.voicePath = vFile;
        }

        public override string ToString()
        {
            string str = string.Empty;
            str += "Name: " + charaName+"\n";
            str += "Content: " + mainContent + "\n";
            str += "VoicePath: " + voicePath + "\n";
            return str;
        }
    }
}
