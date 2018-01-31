﻿using Assets.Script.GameStruct.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.GameStruct
{
    /// <summary>
    /// 多周目全局数据 单独存档
    /// </summary>
    public class MultiData
    {
        /// <summary>
        /// 音乐浏览开启表
        /// </summary>
        public Dictionary<int, bool> musicTable;

        /// <summary>
        /// CG浏览表
        /// </summary>
        public Dictionary<int, bool> cgTable;

        /// <summary>
        /// 结局开启表
        /// </summary>
        public Dictionary<int, bool> endingTable;

        /// <summary>
        /// 案件回顾开启表
        /// </summary>
        public Dictionary<int, bool> caseTable;

        public MultiData()
        {
            musicTable = new Dictionary<int, bool>();
            cgTable = new Dictionary<int, bool>();
            endingTable = new Dictionary<int, bool>();
            caseTable = new Dictionary<int, bool>();
        }
    }
}
