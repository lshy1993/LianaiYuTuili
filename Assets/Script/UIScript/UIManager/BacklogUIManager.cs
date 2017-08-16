using UnityEngine;
using System.Collections;
using System;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;
using System.Collections.Generic;

public class BacklogUIManager : MonoBehaviour {

    private GameObject table;
    private UIScrollBar bar;

	void Awake ()
    {
        table = this.transform.Find("Scroll View/BacklogText_Table").gameObject;
        bar = this.transform.Find("ScrollBar").GetComponent<UIScrollBar>();
    }

    void OnEnable()
    {
        //刷新位置
        table.GetComponent<UITable>().Reposition();
        transform.Find("Scroll View").GetComponent<UIScrollView>().ResetPosition();
        if (table.transform.childCount > 6) bar.value = 1;
    }

    public bool IsEnoughRow()
    {
        return table.transform.childCount > 6;
    }
}
