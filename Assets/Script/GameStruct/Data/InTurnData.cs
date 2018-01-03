using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.GameStruct
{
    /// <summary>
    /// 回合内游戏数据
    /// </summary>
    public class InTurnData
    {
        /// <summary>
        /// 当前持有的证据表 UID索引
        /// </summary>
        public List<string> holdEvidences;

        /// <summary>
        /// 侦探事件已知信息 信息名索引
        /// </summary>
        public List<string> detectKnown;

        /// <summary>
        /// 当前侦探模式
        /// </summary>
        public Constants.DETECT_STATUS detectMode;

        /// <summary>
        /// 当前侦探事件
        /// </summary>
        public string currentDetectEvent;

        /// <summary>
        /// 当前侦探位置
        /// </summary>
        public string currentDetectPos;

        /// <summary>
        /// 侦探事件位置状态表 事件名索引
        /// </summary>
        public Dictionary<string, bool> detectEventTable;

        /// <summary>
        /// 已威慑证词表 序号索引
        /// </summary>
        public List<int> pressedTestimony;

        /// <summary>
        /// 当前所处询问名
        /// </summary>
        public string currentEnquire;

        /// <summary>
        /// 当前所处证词序号
        /// </summary>
        public int currentTestimonyNum;

        /// <summary>
        /// 当前血量
        /// </summary>
        public int currentHP;


    }
}
