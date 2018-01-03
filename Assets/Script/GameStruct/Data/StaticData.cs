using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.GameStruct.Model;

namespace Assets.Script.GameStruct
{
    /// <summary>
    /// 游戏内静态的数据
    /// </summary>
    public class StaticData
    {
        /// <summary>
        /// 事件总表
        /// </summary>
        public Dictionary<string, MapEvent> eventTable;

        /// <summary>
        /// 侦探事件表
        /// </summary>
        public Dictionary<string, DetectEvent> detectEvents;

        /// <summary>
        /// 询问总表
        /// </summary>
        public Dictionary<string, EnquireEvent> enquireEvents;

        /// <summary>
        /// 自我推理总表
        /// </summary>
        public Dictionary<string, ReasoningEvent> reasonEvents;

        /// <summary>
        /// 证据总列表
        /// </summary>
        public Dictionary<string, Evidence> evidenceDic;

        /// <summary>
        /// 女主角资料表
        /// </summary>
        public Dictionary<string, Girl> girls;

        /// <summary>
        /// 旅游资讯表
        /// </summary>
        public Dictionary<string, Tour> tours;

        /// <summary>
        /// 帮助词条表
        /// </summary>
        public Dictionary<string, Keyword> keywords;

        /// <summary>
        /// 日程表
        /// </summary>
        public Dictionary<int, Routine> routines;

        public Dictionary<int, string> cgInfo;

        public Dictionary<string, Character> characters;

        public List<EduEvent> eduEvents;
    }
}
