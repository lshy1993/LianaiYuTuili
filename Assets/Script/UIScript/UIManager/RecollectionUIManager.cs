using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.GameStruct;

public class RecollectionUIManager : MonoBehaviour
{
    private List<bool> caseTable;

    private void OnEnable()
    {
        caseTable = (List<bool>)DataPool.GetInstance().GetSystemVar("案件表");
        SetRecollection();
    }

    private void SetRecollection()
    {
        //编辑器内 先设计好所有按钮位置 只需要开启即可
        GameObject grid = transform.Find("Case_Container").gameObject;
        for (int i = 0; i < caseTable.Count; i++)
        {
            grid.transform.Find("Case" + i + "_Button").gameObject.SetActive(caseTable[i]);
        }
    }

    public void ClickCase()
    {
        //按下case按钮
    }

}
