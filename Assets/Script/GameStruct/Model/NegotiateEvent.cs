using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    /// <summary>
    /// 对峙事件类
    /// </summary>
    public class NegotiateEvent
    {
        /// <summary>
        /// 唯一编号
        /// </summary>
        public string id;

        /// <summary>
        /// 主目标
        /// </summary>
        public string mainGoal;

        /// <summary>
        /// 副目标
        /// </summary>
        public string subGoal;

        /// <summary>
        /// 入口对峙文本UID
        /// </summary>
        public int entry;

        /// <summary>
        /// 结束出口脚本名
        /// </summary>
        public string exit;

        public NegotiateEvent(JsonData data)
        {
            id = (string)data["编号"];
            mainGoal = (string)data["主目标"];
            subGoal = (string)data["副目标"];
            entry = (int)data["入口"];
            exit = (string)data["出口"];

        }

    }
}
