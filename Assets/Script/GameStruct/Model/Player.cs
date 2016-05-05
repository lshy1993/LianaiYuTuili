using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.GameStruct.Model
{
    /// <summary>
    /// Class User
    /// 用户数据模型
    /// </summary>
    public class Player
    {
        /// <summary>
        /// 进度
        /// </summary>
        private Dictionary<string, int> progress;

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

        private static Player instance = null;
        public static Player GetInstance()
        {
            if (instance == null) instance = new Player();
            return instance;
        }

        private Player()
        {
            init();
        }

        private void init()
        {
            progress = new Dictionary<string, int>();
            basicStatus = new Dictionary<string, int>();
            girls = new Dictionary<string, int>();
            logicStatus = new Dictionary<string, int>();

            progress["月"] = 9;
            progress["日"] = 1;
            progress["星期"] = 1;
            progress["回合"] = 1;
            

            basicStatus["文科"] = 50;
            basicStatus["理科"] = 50;
            basicStatus["艺术"] = 50;
            basicStatus["体育"] = 50;
            basicStatus["宅力"] = 50;
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


        public void moveOneTurn()
        {
            int t = progress["回合"] + 1;
            if(0 < t && t <= 30)
            {
                progress["月"] = 9;
                progress["日"] = t;
            }
            else if (t <= 61)
            {
                progress["月"] = 10;
                progress["日"] = t - 30;
            }
            else if(t <= 91)
            {
                progress["月"] = 11;
                progress["日"] = t - 61;
            }
            else if(t <= 122)
            {
                progress["月"] = 12;
                progress["日"] = t - 91;
            }
            else if(t <= 153)
            {
                progress["月"] = 1;
                progress["日"] = t - 122;
            }
            else if(t <= 180)
            {
                progress["月"] = 2;
                progress["日"] = t - 153;               
            }
            else
            {
                // 不做改变
                return;
            }

            progress["星期"] = t % 7;
            progress["回合"] = t;

        }

        public int GetTime(string s)
        {
            return progress[s];
        }

        public int GetBasicStatus(string s)
        {
            return basicStatus[s];
        }

        public bool ContainsBasicStatus(string s)
        {
            return basicStatus.ContainsKey(s);
        }

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


        public int GetGirlPoint(string s)
        {
            return girls[s];
        }

        public void AddGirlPoint(string s, int i)
        {
            if (girls.ContainsKey(s))
            {
                girls[s] += i;
                if (girls[s] < Constants.GIRLS_MIN) girls[s] = Constants.GIRLS_MIN;
                if (girls[s] > Constants.GIRLS_MAX) girls[s] = Constants.GIRLS_MAX;
            }
        }




        public int GetLogicStatus(string s)
        {
            return logicStatus[s];
        }
        public bool ContainsLogicStatus(string s)
        {
            return logicStatus.ContainsKey(s);
        }
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

            str += "\nprogress: \n";
            foreach (var item in progress)
            {
                str += item.Key.ToString() + ":" + item.Value.ToString() + "\n";
            }

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
