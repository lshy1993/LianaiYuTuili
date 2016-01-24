using UnityEngine;
using System.Collections;

public class UserData{

    //玩家数据（进度）
    public int month, day, week, date, money;
    public int rank, status;
    //玩家数据（养成）
    public int wen, li, yi, ti, zhai;
    //玩家数据（推理）
    public int leng, kou, si, guan;
    public int hp, mp;
    public int[] evidenceList;
    //玩家数据（恋爱）
    public int[] girlsPoint;
    //系统数据（游戏）
    public int gamenode;
    public int volumeBGM, volumeSE, volumeVoice;
    public string currentBGM, currentSE, currentVoice;
    public string currentScenario;
    public int ScenarioLine;

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
        hp = 8;
        mp = 4;
    }

}
