using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.GameStruct
{
    public static class Constants
    {
        public static readonly bool DEBUG = true;

        public static readonly int TURN_MIN = 0;
        public static readonly int TURN_MAX = 180;

        public static readonly int MOVE_MIN = 0;
        public static readonly int MOVE_MAX = 100;

        public static readonly int BASIC_MIN = 0;
        public static readonly int BASIC_MAX = 250;

        public static readonly int RANK_MAX = 300000;
        public static readonly int RANK_MIN = 1;

        internal static readonly int MONEY_MIN = 0;
        internal static readonly int MONEY_MAX = int.MaxValue;

        internal static readonly int GIRLS_MIN = 0;
        internal static readonly int GIRLS_MAX = 200;

        internal static readonly int LOGIC_MIN = 0;
        internal static readonly int LOGIC_MAX = 100;

        public static readonly string[] WEEK_DAYS = new string[] { " 星期日 ", " 星期一 ", " 星期二 ", " 星期三 ", " 星期四 ", " 星期五 ", " 星期六 " };

        public enum DETECT_STATUS
        {
            FREE, DIALOG, INVEST, MOVE
        }


    }

}
