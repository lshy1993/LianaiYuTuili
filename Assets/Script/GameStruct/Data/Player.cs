using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    /// <summary>
    /// 用户数据模型
    /// </summary>
    public class Player
    {

        /// <summary>
        /// 养成属性
        /// </summary>
        public int[] basicStatus;
        private Dictionary<string, int> basicName = new Dictionary<string, int>
        {
            { "文科", 0 }, { "理科", 1 },
            { "艺术" , 2 }, { "体育", 3 },
            { "宅力" , 4 }, { "体力", 5 },
            { "排名", 6 }, { "金钱" ,7 }
        };

        /// <summary>
        /// 好感度
        /// </summary>
        public int[] girls;
        private Dictionary<string, int> girlsName = new Dictionary<string, int>
        {
            { "苏梦忆", 0 }, { "西门吹", 1 },
            { "欧阳晓芸", 2 }, { "车小曼", 3 },
            { "陈瑜", 4 }
        };

        /// <summary>
        /// 逻辑属性
        /// </summary>
        public int[] logicStatus;
        private Dictionary<string, int> logicName = new Dictionary<string, int>
        {
            { "冷静", 0 }, { "口才", 1 },
            { "思维", 2 }, { "观察" , 3 },
            { "生命上限", 4 }
        };

        public int LimitHP
        {
            get { return logicStatus[logicName["生命上限"]]; }
        }

        /// <summary>
        /// 体力
        /// </summary>
        public int energyPoint
        {
            set
            {
                if (value > Constants.MOVE_MAX)
                {
                    basicStatus[basicName["体力"]] = Constants.MOVE_MAX;
                }
                else if(value < Constants.MOVE_MIN)
                {
                    basicStatus[basicName["体力"]] = Constants.MOVE_MIN;
                    //Debug.Log("体力超过上下限");
                }
                else
                {
                    basicStatus[basicName["体力"]] = value;
                }
            }
            get { return basicStatus[basicName["体力"]]; }
        }

        public void ResetEnergyPoint()
        {
            energyPoint = Constants.MOVE_MAX;
        }

        public void AddEnergy(int i)
        {
            basicStatus[basicName["体力"]] += i;
        }


        public Player()
        {
            basicStatus = new int[8] { 50, 50, 50, 50, 50, 100, 150000, 100 };
            girls = new int[5] { 0, 0, 0, 0, 0 };
            logicStatus = new int[5] { 3, 4, 4, 3, 5 };
            /*
            basicStatus = new Dictionary<string, int>();
            girls = new Dictionary<string, int>();
            logicStatus = new Dictionary<string, int>();

            basicStatus["文科"] = 50;
            basicStatus["理科"] = 50;
            basicStatus["艺术"] = 50;
            basicStatus["体育"] = 50;
            basicStatus["宅力"] = 50;
            basicStatus["体力"] = 100;

            basicStatus["排名"] = 150000;
            basicStatus["金钱"] = 100;

            girls["苏梦忆"] = 0;
            girls["西门吹"] = 0;
            girls["欧阳晓芸"] = 0;
            girls["车小曼"] = 0;
            girls["陈瑜"] = 0;

            logicStatus["冷静"] = 3;
            logicStatus["口才"] = 4;
            logicStatus["思维"] = 4;
            logicStatus["观察"] = 3;
            logicStatus["生命上限"] = 5;
            */
        }

        /*
        public Player(string json)
        {
            JsonData data = JsonMapper.ToObject(json);

            //foreach(KeyValuePair<string, JsonData> kv in data)
            //{
            //    Debug.Log(kv.Key + ":" + kv.Value);
            //}

            basicStatus = new Dictionary<string, int>();
            girls = new Dictionary<string, int>();
            logicStatus = new Dictionary<string, int>();

            basicStatus["文科"] = (int)data[Regex.Escape("基本属性")][Regex.Escape("文科")];
            basicStatus["理科"] = (int)data[Regex.Escape("基本属性")][Regex.Escape("理科")];
            basicStatus["艺术"] = (int)data[Regex.Escape("基本属性")][Regex.Escape("艺术")];
            basicStatus["体育"] = (int)data[Regex.Escape("基本属性")][Regex.Escape("体育")];
            basicStatus["宅力"] = (int)data[Regex.Escape("基本属性")][Regex.Escape("宅力")];
            basicStatus["体力"] = (int)data[Regex.Escape("基本属性")][Regex.Escape("体力")];
            basicStatus["排名"] = (int)data[Regex.Escape("基本属性")][Regex.Escape("排名")];
            basicStatus["金钱"] = (int)data[Regex.Escape("基本属性")][Regex.Escape("金钱")];

            girls["苏梦忆"] = (int)data[Regex.Escape("好感度")][Regex.Escape("苏梦忆")];
            girls["西门吹"] = (int)data[Regex.Escape("好感度")][Regex.Escape("西门吹")];
            girls["欧阳晓芸"] = (int)data[Regex.Escape("好感度")][Regex.Escape("欧阳晓芸")];
            girls["车小曼"] = (int)data[Regex.Escape("好感度")][Regex.Escape("车小曼")];
            girls["陈瑜"] = (int)data[Regex.Escape("好感度")][Regex.Escape("陈瑜")];

            logicStatus["冷静"] = (int)data[Regex.Escape("逻辑属性")][Regex.Escape("冷静")];
            logicStatus["口才"] = (int)data[Regex.Escape("逻辑属性")][Regex.Escape("口才")];
            logicStatus["思维"] = (int)data[Regex.Escape("逻辑属性")][Regex.Escape("思维")];
            logicStatus["观察"] = (int)data[Regex.Escape("逻辑属性")][Regex.Escape("观察")];
            logicStatus["生命上限"] = (int)data[Regex.Escape("逻辑属性")][Regex.Escape("生命上限")];
        }

        public override string ToString()
        {
            return Regex.Unescape(JsonMapper.Serialize(ToDictionary()));
        }

        public Dictionary<string, Dictionary<string, int>> ToDictionary()
        {
            //JsonData data = new JsonData();
            Dictionary<string, Dictionary<string, int>> player = new Dictionary<string, Dictionary<string, int>>();
            player.Add("基本属性", basicStatus);
            player.Add("好感度", girls);
            player.Add("逻辑属性", logicStatus);

            return player;
        }
        */


        //用于模拟计算排名
        //假定750为第一名，幂函数关系 初始四维 200 为150000名
        //权重关系：文理 35 35 艺体 15 15
        public void SetRank()
        {
            int peopleall = 300000;
            int maxscore = 750;
            int wen = basicStatus[basicName["文科"]];
            int li = basicStatus[basicName["理科"]];
            int yi = basicStatus[basicName["艺术"]];
            int ti = basicStatus[basicName["体育"]];
            double score = wen * 0.35 + li * 0.35 + yi * 0.15 + ti * 0.15;
            if (Math.Sqrt(score / maxscore * 2) == 1)
            {
                basicStatus[basicName["排名"]] = 1;
            }
            else
            {
                double rank = peopleall * (1 - Math.Sqrt(score / maxscore * 2));
                basicStatus[basicName["排名"]] = (int)rank;
            }
        }

        /// <summary>
        /// 获取基本属性
        /// </summary>
        /// <param name="s">文科，理科，艺术，体育，宅力，排名，金钱</param>
        public int GetBasicStatus(string s)
        {
            return basicStatus[basicName[s]];
        }


        /// <summary>
        /// 基本属性
        /// 文科，理科，艺术，体育，宅力，排名，金钱
        /// </summary>
        /// <param name="s">文科，理科，艺术，体育，宅力，排名，金钱</param>
        /// <returns></returns>
        public void AddBasicStatus(string s, int i)
        {
            basicStatus[basicName[s]] += i;
            int MAX = Constants.BASIC_MAX;
            int MIN = Constants.BASIC_MIN;
            if (s.Equals("排名"))
            {
                MAX = Constants.RANK_MAX;
                MIN = Constants.RANK_MIN;
            }
            else if (s.Equals("金钱"))
            {
                MAX = Constants.MONEY_MAX;
                MIN = Constants.MONEY_MIN;
            }
            if (basicStatus[basicName[s]] > MAX) basicStatus[basicName[s]] = MAX;
            if (basicStatus[basicName[s]] < MIN) basicStatus[basicName[s]] = MIN;
        }


        /// <summary>
        /// 好感度
        /// </summary>
        /// <param name="s">苏梦忆,西门吹,欧阳晓芸,车小曼,陈瑜</param>
        public int GetGirlPoint(string s)
        {
            return girls[girlsName[s]];
        }

        /// 好感度
        /// 苏梦忆,西门吹,欧阳晓芸,车小曼,陈瑜
        /// </summary>
        /// <param name="s">苏梦忆,西门吹,欧阳晓芸,车小曼,陈瑜</param>
        /// <returns></returns>
        public void AddGirlPoint(string s, int i)
        {
            girls[girlsName[s]] += i;
            if (girls[girlsName[s]] < Constants.GIRLS_MIN) girls[girlsName[s]] = Constants.GIRLS_MIN;
            if (girls[girlsName[s]] > Constants.GIRLS_MAX) girls[girlsName[s]] = Constants.GIRLS_MAX;
        }


        /// <summary>
        /// 获取逻辑属性
        /// 冷静，口才，思维，观察，生命上限
        /// </summary>
        /// <param name="s">冷静，口才，思维，观察，生命上限</param>
        /// <returns></returns>
        public int GetLogicStatus(string s)
        {
            Debug.Log(s);
            return logicStatus[logicName[s]];
        }

        /// <summary>
        /// 获取逻辑属性
        /// </summary>
        /// <param name="s">冷静，口才，思维，观察，生命上限</param>
        public void AddLogicStatus(string s, int i)
        {
            logicStatus[logicName[s]] += i;
            if (logicStatus[logicName[s]] < Constants.LOGIC_MIN) logicStatus[logicName[s]] = Constants.LOGIC_MIN;
            if (logicStatus[logicName[s]] > Constants.LOGIC_MAX) logicStatus[logicName[s]] = Constants.LOGIC_MAX;
        }

    }
}
