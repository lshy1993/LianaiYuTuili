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
    public class User
    {
        /// <summary>
        /// 进度数据
        /// </summary>
        private Dictionary<string, int> time;

        /// <summary>
        /// 养成数据
        /// </summary>
        private Dictionary<string, int> classes;

        /// <summary>
        /// 好感度
        /// </summary>
        private Dictionary<string, int> girls;

        /// <summary>
        /// 状态
        /// </summary>
        private Dictionary<string, int> status;

        private static User instance = null;
        public static User GetInstance()
        {
            if (instance == null) instance = new User();
            return instance;
        }

        private User()
        {
            init();
        }

        private void init()
        {
            time = new Dictionary<string, int>();
            classes = new Dictionary<string, int>();
            girls = new Dictionary<string, int>();
            status = new Dictionary<string, int>();

            time["月"] = 9;
            time["日"] = 1;
            time["周数"] = 1;
            time["周日期"] = 1;
            time["回合"] = 1;

            classes["文科"] = 70;
            classes["理科"] = 160;
            classes["艺术"] = 110;
            classes["体育"] = 30;
            classes["宅度"] = 0;

            //girls[""] = 0;
            // TODO: 初始化好感度

            status["排名"] = 0;
            status["冷静"] = 3;
            status["口才"] = 4;
            status["思维"] = 4;
            status["观察"] = 3;
        }


        /// <summary>
        /// 完成一回合，更新日期
        /// </summary>
        public void WalkDate()
        {
            time["回合"]++;
        }

        public int GetTime(string s)
        {
            return time[s];
        }

        public void SetTime(string s, int i)
        {
            if(time.ContainsKey(s)) time[s] = i;
        }

        public void SetTimeDelta(string s, int i)
        {
            if(time.ContainsKey(s)) time[s] += i;
        }


        public int GetClass(string s)
        {
            return classes[s];
        }

        public bool ContainsClass(string s)
        {
            return classes.ContainsKey(s);
        }

        public void SetClass(string s, int i)
        {
            if(classes.ContainsKey(s)) classes[s] = i;
        }

        public void SetClassDelta(string s, int i)
        {
            if(classes.ContainsKey(s))classes[s] += i;
        }


        public int GetGirlPoint(string s)
        {
            return girls[s];
        }

        public void SetGirlPoint(string s, int i)
        {
            if(girls.ContainsKey(s)) girls[s] = i;
        }

        public void SetGirlPointDelta(string s, int i)
        {
            if(girls.ContainsKey(s)) girls[s] += i;
        }



        public int GetStatus(string s)
        {
            return status[s];
        }
        public bool ContainsStatus(string s)
        {
            return status.ContainsKey(s);
        }

        public void SetStatus(string s, int i)
        {
           if(status.ContainsKey(s)) status[s] = i;
        }

        public void SetStatusDelta(string s, int i)
        {
            if(status.ContainsKey(s)) status[s] += i;
        }

        public override string ToString()
        {
            string str = base.ToString();

            str += "\ntime: \n";
            foreach (var item in time)
            {
                str += item.Key.ToString() + ":" + item.Value.ToString() + "\n";
            }

            str += "\ngirls: \n";
            foreach (var item in girls)
            {
                str += item.Key.ToString() + ":" + item.Value.ToString() + "\n";
            }

            str += "\nclasses: \n";
            foreach (var item in classes)
            {
                str += item.Key.ToString() + ":" + item.Value.ToString() + "\n";
            }

            str += "\nstatus: \n";
            foreach (var item in status)
            {
                str += item.Key.ToString() + ":" + item.Value.ToString() + "\n";
            }
            return str;
        }


    }
}
