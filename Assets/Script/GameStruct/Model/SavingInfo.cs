using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.GameStruct.Model
{
    public class SavingInfo
    {
        public string saveTime;
        public string gameMode;
        public string saveText;
        public string picPath;

        public SavingInfo(string mode,string time,string content,string pic)
        {
            this.gameMode = mode;
            this.saveTime = time;
            this.saveText = content;
            this.picPath = pic;
        }

        public SavingInfo() { }
    }
}
