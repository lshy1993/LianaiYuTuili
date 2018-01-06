using Assets.Script.GameStruct.Model;
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
        public List<bool> musicTable;

        /// <summary>
        /// CG浏览表
        /// </summary>
        public List<bool> cgTable;

        /// <summary>
        /// 结局开启表
        /// </summary>
        public List<bool> endingTable;

        /// <summary>
        /// 案件回顾开启表
        /// </summary>
        public List<bool> caseTable;

        public MultiData()
        {
            musicTable = new List<bool>();
            cgTable = new List<bool>();
            endingTable = new List<bool>();
            caseTable = new List<bool>();
        }
    }
}
