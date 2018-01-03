using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.GameStruct.Model;

namespace Assets.Script.GameStruct
{
    /// <summary>
    /// 游戏系统数据
    /// </summary>
    public class SystemData
    {
        /// <summary>
        /// 当前系统设置模式
        /// </summary>
        public Constants.Setting_Mode settingMode;

        /// <summary>
        /// 是否全屏
        /// </summary>
        public bool fullScreen;

        /// <summary>
        /// 是否开启转换特效
        /// </summary>
        public bool fadingSwitch;

        /// <summary>
        /// 是否开启动画特效
        /// </summary>
        public bool animateSwitch;

        /// <summary>
        /// 是否开启头像
        /// </summary>
        public bool avatarSwitch;

        /// <summary>
        /// BGM曲名显示时长
        /// </summary>
        public int BGMTime;

        /// <summary>
        /// 章节名显示时长
        /// </summary>
        public int chapterTime;

        /// <summary>
        /// 打字机速度
        /// </summary>
        public float textSpeed;

        /// <summary>
        /// 自动模式等待时长
        /// </summary>
        public float waitTime;

        /// <summary>
        /// 对话框透明度
        /// </summary>
        public int diaboxAlpha;

        /// <summary>
        /// 角色语音默认显示项
        /// </summary>
        public int defaultCharaNum;

        /// <summary>
        /// 角色语音音量表
        /// </summary>
        public float[] charaVoiceVolume;

        /// <summary>
        /// 角色语音开启表
        /// </summary>
        public bool[] charaVoice;

        /// <summary>
        /// 音乐表
        /// </summary>
        public List<bool> musicTable;

        /// <summary>
        /// CG表
        /// </summary>
        public List<bool> cgTable;

        /// <summary>
        /// 结局表
        /// </summary>
        public List<bool> endingTable;

        /// <summary>
        /// 案件表
        /// </summary>
        public List<bool> caseTable;

        /// <summary>
        /// 存档信息
        /// </summary>
        public Dictionary<int, SavingInfo> saveInfo;

        public SystemData()
        {
            settingMode = Constants.Setting_Mode.Graphic;
            fullScreen = false;
            fadingSwitch = true;
            animateSwitch = true;
            avatarSwitch = true;
            BGMTime = 3;
            chapterTime = 3;
            textSpeed = 60f;
            waitTime = 1.5f;
            diaboxAlpha = 75;
            defaultCharaNum = 0;
            charaVoiceVolume = new float[] { 1, 1, 1, 1, 1, 1 };
            charaVoice = new bool[] { true, true, true, true, true, true };

        }

    }
}
