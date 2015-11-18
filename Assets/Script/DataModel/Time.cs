using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.DataModel
{
    public class Time
    {
        public int month, week, date, dayInWeek;
        public Time()
        {
            month = 9;
            date = 1;
            week = 1;
            dayInWeek = 1;
        }
    }
}
