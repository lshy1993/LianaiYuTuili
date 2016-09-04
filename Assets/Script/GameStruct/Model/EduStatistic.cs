using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    public class EduStatistic
    {
        public int max, min;

        public EduStatistic(JsonData data)
        {
            max = (int)data["最大"];
            min = (int)data["最小"];
        }
    }
}
