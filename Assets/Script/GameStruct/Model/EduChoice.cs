using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.GameStruct.Model
{
    /// <summary>
    /// EduChoice
    /// 模拟选择内容
    /// </summary>
    public class EduChoice
    {
        //public string hint { set; get; }
        //public string[] type { set; get; }

        public string name { set; get; }
        public string description { set; get; }
        public Dictionary<string, int[]> data;

        public EduChoice()
        {
            data = new Dictionary<string, int[]>();
        }
        //public string baseimg { set; get; }
        //public string hoverimg { set; get; }
        //public string 

        public override string ToString()
        {
            return "Hint = " + name + "\n"
                +  "type = " + name.ToString() + "\n"
                +  "data = " + data.ToString();
        }



    }
}
