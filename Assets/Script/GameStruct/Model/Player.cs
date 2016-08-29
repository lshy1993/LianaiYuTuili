using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct.Model
{

    /// <summary>
    /// Class User
    /// 用户数据模型
    /// </summary>
    public class Player
    {

        /// <summary>
        /// 养成属性
        /// </summary>
        private Dictionary<string, int> basicStatus;

        /// <summary>
        /// 好感度
        /// </summary>
        private Dictionary<string, int> girls;

        /// <summary>
        /// 逻辑属性
        /// </summary>
        private Dictionary<string, int> logicStatus;

        /// <summary>
        /// 体力
        /// </summary>
        private int energyPoint;

        public int EnergyPoint
        {
            set{
                if(value > Constants.MOVE_MAX || value < Constants.MOVE_MIN)
                {
                    Debug.Log("体力超过上下限");
                }
                else
                {
                    this.energyPoint = value;
                }
            }

            get { return energyPoint; }
        }

        public void ResetEnergyPoint()
        {
            energyPoint = Constants.MOVE_MAX;
        }

        public void AddEnergy(int i)
        {
            //EnergyPoint = EnergyPoint + i;
            basicStatus["体力"] += i;
        }

        private static Player instance = null;

        public static Player GetInstance()
        {
            if (instance == null) instance = new Player();
            return instance;
        }

        private Player()
        {
            Init();
        }

        private void Init()
        {
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
        }


        /// <summary>
        /// 获取时间
        /// </summary>
        /// <param name="s">日， 月， 星期，回合</param>
        /// <returns></returns>
        //public int GetTime(string s)
        //{
        //    return progress[s];
        //}
        /// <summary>
        /// 获取基本属性
        /// 文科，理科，艺术，体育，宅力，排名，金钱
        /// </summary>
        /// <param name="s">文科，理科，艺术，体育，宅力，排名，金钱</param>
        /// <returns></returns>
        public int GetBasicStatus(string s)
        {
            return basicStatus[s];
        }
        /// <summary>
        /// 检测基本属性
        /// 文科，理科，艺术，体育，宅力，排名，金钱
        /// </summary>
        /// <param name="s">文科，理科，艺术，体育，宅力，排名，金钱</param>
        /// <returns></returns>
        public bool ContainsBasicStatus(string s)
        {
            return basicStatus.ContainsKey(s);
        }
        /// <summary>
        /// 基本属性
        /// 文科，理科，艺术，体育，宅力，排名，金钱
        /// </summary>
        /// <param name="s">文科，理科，艺术，体育，宅力，排名，金钱</param>
        /// <returns></returns>
        public void AddBasicStatus(string s, int i)
        {
            if (basicStatus.ContainsKey(s))
            {
                basicStatus[s] += i;
                if (s.Equals("排名"))
                {
                    if (basicStatus[s] > Constants.RANK_MAX) basicStatus[s] = Constants.RANK_MAX;
                    if (basicStatus[s] < Constants.RANK_MIN) basicStatus[s] = Constants.RANK_MIN;
                }
                else if (s.Equals("金钱"))
                {
                    if (basicStatus[s] > Constants.MONEY_MAX) basicStatus[s] = Constants.MONEY_MAX;
                    if (basicStatus[s] < Constants.MONEY_MIN) basicStatus[s] = Constants.MONEY_MIN;

                }
                else
                {
                    if (basicStatus[s] > Constants.BASIC_MAX) basicStatus[s] = Constants.BASIC_MAX;
                    if (basicStatus[s] < Constants.BASIC_MIN) basicStatus[s] = Constants.BASIC_MIN;
                }

            }
        }


        /// <summary>
        /// 好感度
        /// 苏梦忆,西门吹,欧阳晓芸,车小曼,陈瑜
        /// </summary>
        /// <param name="s">苏梦忆,西门吹,欧阳晓芸,车小曼,陈瑜</param>
        /// <returns></returns>
        public int GetGirlPoint(string s)
        {
            return girls[s];
        }

        /// 好感度
        /// 苏梦忆,西门吹,欧阳晓芸,车小曼,陈瑜
        /// </summary>
        /// <param name="s">苏梦忆,西门吹,欧阳晓芸,车小曼,陈瑜</param>
        /// <returns></returns>

        public void AddGirlPoint(string s, int i)
        {
            if (girls.ContainsKey(s))
            {
                girls[s] += i;
                if (girls[s] < Constants.GIRLS_MIN) girls[s] = Constants.GIRLS_MIN;
                if (girls[s] > Constants.GIRLS_MAX) girls[s] = Constants.GIRLS_MAX;
            }
        }




        /// <summary>
        /// 获取逻辑属性
        /// 冷静，口才，思维，观察，生命上限
        /// </summary>
        /// <param name="s">冷静，口才，思维，观察，生命上限</param>
        /// <returns></returns>
        public int GetLogicStatus(string s)
        {
            return logicStatus[s];
        }

        /// <summary>
        /// 获取逻辑属性
        /// 冷静，口才，思维，观察，生命上限
        /// </summary>
        /// <param name="s">冷静，口才，思维，观察，生命上限</param>
        /// <returns></returns>
        public bool ContainsLogicStatus(string s)
        {
            return logicStatus.ContainsKey(s);
        }


        /// <summary>
        /// 获取逻辑属性
        /// 冷静，口才，思维，观察，生命上限
        /// </summary>
        /// <param name="s">冷静，口才，思维，观察，生命上限</param>
        /// <returns></returns>
        public void AddLogicStatus(string s, int i)
        {
            if (logicStatus.ContainsKey(s))
            {
                logicStatus[s] += i;
                if (logicStatus[s] < Constants.LOGIC_MIN) logicStatus[s] = Constants.LOGIC_MIN;
                if (logicStatus[s] > Constants.LOGIC_MAX) logicStatus[s] = Constants.LOGIC_MAX;
            }

        }



        public override string ToString()
        {
            string str = base.ToString();
            str += "玩家数据Player:\n";
            str += "体力： " + energyPoint;

            //str += "\nprogress: \n";
            //foreach (var item in progress)
            //{
            //    str += item.Key.ToString() + ":" + item.Value.ToString() + "\n";
            //}

            str += "\ngirls: \n";
            foreach (var item in girls)
            {
                str += item.Key.ToString() + ":" + item.Value.ToString() + "\n";
            }

            str += "\nbasic status: \n";
            foreach (var item in basicStatus)
            {
                str += item.Key.ToString() + ":" + item.Value.ToString() + "\n";
            }

            str += "\nlogic status: \n";
            foreach (var item in logicStatus)
            {
                str += item.Key.ToString() + ":" + item.Value.ToString() + "\n";
            }
            return str;
        }

    }
}
