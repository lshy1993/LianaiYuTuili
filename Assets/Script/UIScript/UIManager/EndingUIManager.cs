using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.GameStruct;

public class EndingUIManager : MonoBehaviour
{
    private Dictionary<int, string> endingInfoTable;
    private List<bool> endingTable;

    public UILabel info;

    private void OnEnable()
    {
        endingTable = (List<bool>)DataPool.GetInstance().GetSystemVar("结局表");
        SetEnding();
    }

    private void SetEnding()
    {
        //编辑器内 先设计好所有的结局图标
        //需要设置图片 true的显示相应 false的为默认
        GameObject grid = transform.Find("Achieve_Grid").gameObject;
        for (int i = 0; i < endingTable.Count; i++)
        {
            GameObject go = grid.transform.GetChild(i).gameObject;
            UIButton btn = go.GetComponent<UIButton>();
            if (endingTable[i])
            {
                btn.normalSprite2D = Resources.Load<Sprite>("AchieveIcon" + i);
            }
            else
            {
                btn.normalSprite2D = Resources.Load<Sprite>("star");
            }
        }
    }
    public void ClickAchieveAt(string str)
    {
        //int x = System.Convert.ToInt32(str);
        info.text = "这是第" + str + "个成就！";
    }

}
