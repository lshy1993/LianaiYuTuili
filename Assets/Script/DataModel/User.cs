using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.DataModel
{
    public class User
    {
        public int liberal, science, art, sport, otaku;
        public int calm, speech, logic, observe;
        public int money, rank, status;

        public User()
        {
            liberal = 70;
            science = 160;
            art = 110;
            sport = 30;
            otaku = 0;
            calm = 3;
            speech = 4;
            logic = 4;
            observe = 3;
            money = 1000;
            status = 1;
        }

    }
}
