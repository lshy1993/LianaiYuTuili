using UnityEngine;
using System.Collections;
using System;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;
using System.Collections.Generic;

public class BacklogUIManager : MonoBehaviour {

    private GameObject table;

	void Awake ()
    {
        table = this.transform.Find("Scroll View/BacklogText_Table").gameObject;
    }

    void OnEnable()
    {
        //刷新位置
        table.GetComponent<UITable>().Reposition();
        transform.Find("Scroll View").GetComponent<UIScrollView>().ResetPosition();
        transform.Find("Scroll View").GetComponent<UIScrollView>().UpdatePosition();
    }

}
