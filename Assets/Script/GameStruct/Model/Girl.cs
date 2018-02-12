using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    public class Girl
    {
        public string name, cla, club, horoscope;
        public List<string> like, dislike, intro;
        public int height, weight, dayOfBirth, monthOfBirth;

        public Girl(JsonData data)
        {
            name = (string)data["姓名"];
            cla = (string)data["班级"];
            club = (string)data["社团"];
            height = (int)data["身高"];
            weight = (int)data["体重"];
            monthOfBirth = (int)data["生日"][0];
            dayOfBirth = (int)data["生日"][1];
            horoscope = (string)data["星座"];

            like = new List<string>();
            foreach(JsonData da in data["喜欢"])
            {
                like.Add((string)da);
            }

            dislike = new List<string>();
            foreach (JsonData da in data["讨厌"])
            {
                dislike.Add((string)da);
            }

            intro = new List<string>();
            foreach (JsonData da in data["简介"])
            {
                intro.Add((string)da);
            }

        }

        /// <summary>
        /// 格式化输出
        /// </summary>
        /// <param name="isEng">是否显示变量名</param>
        public string ToString(bool isEng)
        {
            string str = string.Empty;

            str += (isEng ? "name" : "姓名") + " : " + name + "\n";
            str += (isEng ? "cla" : "班级") + " : " + cla + "\n";
            str += (isEng ? "club" : "社团") + " : " + club + "\n";
            str += (isEng ? "height" : "身高") + " : " + height + "\n";

            str += (isEng ? "weight" : "体重") + " : " + weight + "\n";

            str += (isEng ? "monthOfBirth" : "生日月份") + " : " + monthOfBirth + "\n";

            str += (isEng ? "dayOfBirth" : "生日天数") + " : " + dayOfBirth + "\n";

            str += (isEng ? "horoscope" : "星座") + " : " + horoscope + "\n";

            str += (isEng ? "like" : "喜欢") + " : ";
            foreach (string i in like)
            {
                str += i + "  ";
            }
            str += "\n";

            str += (isEng ? "dislike" : "讨厌") + " : ";
            foreach (string i in dislike)
            {
                str += i + "  ";
            }
            str += "\n";

            str += (isEng ? "intro" : "简介") + " : \n";
            foreach (string i in intro)
            {
                str += i + "\n";
            }
            str += "\n";

            return str;
        }

    }
}
