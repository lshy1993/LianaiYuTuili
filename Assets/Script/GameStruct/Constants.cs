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
        public static readonly int MOVE_MAX = 250;

        public static readonly int BASIC_MIN = 0;
        public static readonly int BASIC_MAX = 250;

        public static readonly int RANK_MAX = 300000;
        public static readonly int RANK_MIN = 1;

        public static readonly string ROOT_PATH = "JSON/";

        public static readonly string DEFAULT_PATH = ROOT_PATH + "MapEvents/";
        public static readonly string LOCATION_PATH = ROOT_PATH + "ButtonConfig/";
        public static readonly string DETECT_PATH = ROOT_PATH + "Detect/";
        public static readonly string EVIDENCE_PATH = ROOT_PATH + "EvidenceConfig/";
        public static readonly string EDU_PATH = ROOT_PATH + "EduConfig/";
        public static readonly string REASONING_PATH = ROOT_PATH + "ReasoningConfig/";
        public static readonly string ENQUIRE_PATH = ROOT_PATH + "EnquireConfig/";

        //DEBUG paths
        public static readonly string DEBUG_PATH = ROOT_PATH + "TestEvents/";
        public static readonly string DETECT_DEBUG_PATH = ROOT_PATH + "DetectDebug/";
        public static readonly string EVIDENCE_DEBUG_PATH = ROOT_PATH + "EvidenceDebug/";
        public static readonly string EDU_DEBUG_PATH = ROOT_PATH + "EduDebug/";
        public static readonly string REASONING_DEBUG_PATH = ROOT_PATH + "ReasoningDebug/";
        public static readonly string ENQUIRE_DEBUG_PATH = ROOT_PATH + "EnquireDebug/";

        internal static readonly int MONEY_MIN = 0;
        internal static readonly int MONEY_MAX = int.MaxValue;

        internal static readonly int GIRLS_MIN = 0;
        internal static readonly int GIRLS_MAX = 200;

        internal static readonly int LOGIC_MIN = 0;
        internal static readonly int LOGIC_MAX = 100;

        public static readonly string[] WEEK_DAYS = new string[]
        {
            " 星期日 ", " 星期一 ", " 星期二 ", " 星期三 ", " 星期四 ", " 星期五 ", " 星期六 "
        };

        public enum DETECT_STATUS
        {
            FREE, DIALOG, INVEST, MOVE
        }

        public enum ENQUIRE_STATUS
        {
            LOOP, PRESS, WRONG, CORRECT, GAMEOVER
        }

        public enum TITLE_STATUS
        {
            TITLE, EXTRA, MUSIC, GALLERY, RECOLL, ENDING
        }

        public enum APP_STATUS
        {
            TOP, CALENDAR, TOUR, HELP
        }

        public enum NOTE_STATUS
        {
            INDEX, SELF, LOVE, EVIDENCE, APP
        }

        public enum Setting_Mode
        {
            Graphic, Sound, Text, Operate
        }

        public enum WarningMode
        {
            Title, Quit, Save, Load, Delete
        }

    }

}
