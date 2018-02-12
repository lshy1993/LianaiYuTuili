using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.GameStruct.Model;

namespace Assets.Script.GameStruct
{
    /// <summary>
    /// 综合游戏数据类
    /// </summary>
    public class GameData
    {
        /// <summary>
        /// 当前游戏回合数
        /// </summary>
        public int gameTurn;

        /// <summary>
        /// 玩家设定主角姓
        /// </summary>
        public string heroXing;

        /// <summary>
        /// 玩家设定主角名
        /// </summary>
        public string heroMing;

        public int morningSchedule;
        public int afternoonSchedule;
        public float morningRate;
        public float afternoonRate;

        /// <summary>
        /// 游戏主角信息类
        /// </summary>
        public Player player;

        /// <summary>
        /// 精力MP上限
        /// </summary>
        public int All_MP;

        /// <summary>
        /// 当前游戏模式
        /// </summary>
        public string MODE;

        /// <summary>
        /// 当前事件名
        /// </summary>
        public string currentEvent;

        /// <summary>
        /// 当前脚本名
        /// </summary>
        public string currentScript;

        /// <summary>
        /// 当前文字位置
        /// </summary>
        public int currentTextPos;

        /// <summary>
        /// 背景图片名
        /// </summary>
        public string bgSprite;

        /// <summary>
        /// 立绘信息
        /// </summary>
        public Dictionary<int, SpriteState> fgSprites;

        /// <summary>
        /// 全地图事件状态表
        /// </summary>
        public Dictionary<string, int> eventStatus;

        /// <summary>
        /// 当前播放的BGM曲名
        /// </summary>
        public string BGM;

        /// <summary>
        /// 当前播放的SE曲名
        /// </summary>
        public string SE;

        /// <summary>
        /// 当前播放的语音文件名
        /// </summary>
        public string Voice;

        /// <summary>
        /// 储存发来消息的人数
        /// </summary>
        public List<string> messageNameList;

        /// <summary>
        /// 储存每个角色收到的信息
        /// </summary>
        public Dictionary<string, List<int>> charaMessages;

        /// <summary>
        /// 至当前时间点的所有朋友圈
        /// </summary>
        public List<Moment> momentList;
    }
}
