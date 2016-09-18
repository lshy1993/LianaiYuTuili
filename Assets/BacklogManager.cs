using UnityEngine;
using System.Collections;
using System;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;
using System.Collections.Generic;

public class BacklogManager : MonoBehaviour {

    private GameObject table;
    private List<BacklogText> backlist;

	void Awake () {
        table = this.transform.Find("Scroll View/BacklogText_Table").gameObject;
    }

    void OnEnable()
    {
        backlist = (List<BacklogText>)DataPool.GetInstance().GetGameVar("文字记录");
        table.transform.DestroyChildren();
        foreach (BacklogText bt in backlist)
        {
            GameObject go = (GameObject)Resources.Load("Prefab/Backlog");
            NGUITools.AddChild(table, go);

            go.transform.Find("Name_Label").GetComponent<UILabel>().text = bt.charaName;
            go.transform.Find("Content_Label").GetComponent<UILabel>().text = bt.mainContent;
            go.transform.Find("Voice_Button").GetComponent<BacklogVoiceButton>().path = bt.voicePath;

            go.GetComponent<UITable>().Reposition();
        }
        table.GetComponent<UITable>().Reposition();
        this.transform.Find("Scroll View").GetComponent<UIScrollView>().ResetPosition();
        this.transform.Find("Scroll View").GetComponent<UIScrollView>().UpdatePosition();
    }

}
