using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Assets.Script.GameStruct.Model
{
    /// <summary>
    /// 数值增减类
    /// </summary>
    public class EduStatistic
    {
        /// <summary>
        /// 上下限
        /// </summary>
        public int max, min;

        public EduStatistic(JsonData data)
        {
            max = (int)data["最大"];
            min = (int)data["最小"];
        }
    }

    /// <summary>
    /// 养成事件类
    /// </summary>
    public class EduEvent
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string name;

        /// <summary>
        /// （未启用）熟练等级
        /// </summary>
        public int level;

        /// <summary>
        /// 数值增减类
        /// </summary>
        public Dictionary<string, EduStatistic> statistic;

        /// <summary>
        /// 消耗活力
        /// </summary>
        public int ap;

        public EduEvent(JsonData data)
        {
            name = (string)data["课程"];
            level = (int)data["等级"];

            statistic = new Dictionary<string, EduStatistic>();
            JsonData jd = data["属性区间"];
            statistic.Add("文科", jd.Contains("文科") ? new EduStatistic(jd["文科"]) : null);
            statistic.Add("理科", jd.Contains("理科") ? new EduStatistic(jd["理科"]) : null);
            statistic.Add("艺术", jd.Contains("艺术") ? new EduStatistic(jd["艺术"]) : null);
            statistic.Add("体育", jd.Contains("体育") ? new EduStatistic(jd["体育"]) : null);
            statistic.Add("宅力", jd.Contains("宅力") ? new EduStatistic(jd["宅力"]) : null);

            ap = (int)data["体力"];

        }

    }
}
