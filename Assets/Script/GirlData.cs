using UnityEngine;
using System.Collections;

public class GirlData {
    
    public string name { set; get; }
    public string cla { set; get; }
    public string club { set; get; }
    public int height { set; get; }
    public int weight { set; get; }
    public string birth { set; get; }
    public string star { set; get; }
    public int graderank { set; get; }
    public int provencerank { set; get; }
    public string like { set; get; }
    public string dislike { set; get; }
    public string info { set; get; }

    public GirlData() { }

    public GirlData(int x)
    {
        name = "两姓两名";
        cla = "高" + x + x + "班";
        club = x + "社";
        height = x * 111;
        weight = x * 111;
        birth = x + "月" + x + "日";
        star = x + "座";
        graderank = x * 111;
        provencerank = x * 11111;
        like = "这里写她喜欢干什么，最多可放十八个字";
        dislike = "这里写她讨厌干什么，最多能放十八个字";
        info = "第一女主角，主人公的同班班长，是个坦率并且受大家喜欢的人，和西门吹的关系非常复杂。无论做什么都非常努力。在同年级里面拥有很高的人气。";
    }
}
