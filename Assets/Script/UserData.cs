using UnityEngine;
using System.Collections;

public class UserData{

    public int month { set; get; }
    public int day { set; get; }
    public int week { set; get; }
    public int date { set; get; }
    public int money { set; get; }
    public int wen { set; get; }
    public int li { set; get; }
    public int yi { set; get; }
    public int ti { set; get; }
    public int zhai { set; get; }
    public int rank { set; get; }
    public int status { set; get; }
    public int leng { set; get; }
    public int kou { set; get; }
    public int si { set; get; }
    public int guan { set; get; }

    public UserData()
    {
        month = 9;
        day = 1;
        week = 1;
        date = 1;
        money = 1000;
        wen = 70;
        li = 160;
        yi = 110;
        ti = 30;
        zhai = 0;
        status = 1;
        leng = 3;
        kou = 4;
        si = 4;
        guan = 3;
    }

}
