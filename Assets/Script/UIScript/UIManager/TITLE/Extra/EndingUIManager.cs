using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingUIManager : MonoBehaviour
{
    /// <summary>
    /// 成就静态表
    /// </summary>
    private Dictionary<int, AchieveEnding> endingDic
    {
        get { return DataManager.GetInstance().staticData.endingInfo; }
    }

    /// <summary>
    /// 成就结局开启表格
    /// </summary>
    private Dictionary<int, bool> endingTable
    {
        get { return DataManager.GetInstance().multiData.endingTable; }
    }

    private static string path = "Icon/";

    public UILabel achiNameLabel, achiHintLabel;
    public GameObject mainGrid, hintCon;

    private void OnEnable()
    {
        SetEnding();
    }

    private void SetEnding()
    {
        //编辑器内 先放好所有的结局图标
        //需要设置图片 true的显示相应 false的为灰色图标
        for (int i = 0; i < endingTable.Count; i++)
        {
            GameObject go = mainGrid.transform.GetChild(i).gameObject;
            UIButton btn = go.GetComponent<UIButton>();
            string fileName = path + "Star";
            if(i < 3)
            {
                if (endingTable[i])
                {
                    fileName = path + "AchieveIcon" + i;
                }
                else
                {
                    fileName = path + "AchieveIcon" + i + "_lock";
                }
            }
            btn.normalSprite2D = Resources.Load<Sprite>(fileName);
        }
        hintCon.SetActive(false);
    }

    /// <summary>
    /// 显示成就详细
    /// </summary>
    /// <param name="x">成就编号</param>
    public void ClickAchieveAt(int x)
    {
        if (endingDic.ContainsKey(x))
        {
            achiNameLabel.text = endingDic[x].achieveName;
            achiHintLabel.text = endingDic[x].achieveHint;
        }
        else
        {
            achiNameLabel.text = string.Empty;
            achiHintLabel.text = "该成就未开启";
        }
        hintCon.SetActive(true);
    }

}
